using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    [Header("Player atributes")]
    public float speed;
    public float jumpForce;
    public int life;
    [SerializeField] private int maxLife = 10;
    public int strawberry = 0;
    public GameObject Player;

    [Header("Components")]
    public Rigidbody2D rig;
    public Animator anim;
    public SpriteRenderer sprite;

    [Header("UI")]
    public TextMeshProUGUI strawberryText;
    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI levelNumberText;
    public int level;
    public GameObject gameOver;

    [Header("Extras")]
    [SerializeField] private Vector2 direction;
    public bool canJump = true;
    private bool recovery;
    public static PlayerMove playerInstance;
    public bool stopCoroutine;
    [SerializeField] private ParticleSystem fireParticle;


    private void Awake()
    {
        if (playerInstance == null)
        {
            playerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    void Start()
    {
        lifeText.text = life.ToString();
        levelNumberText.text = level.ToString();
        gameOver.SetActive(false);
        Time.timeScale = 1;
        playerInstance = this;
        fireParticle.enableEmission = false;

        //DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));

        Jump();
        PlayAnimations();

        if (rig != null && fireParticle != null)
        {
            Vector2 playerVelocity = rig.velocity;

            // Aplicar a velocidade à partícula
            ParticleSystem.VelocityOverLifetimeModule velocityModule = fireParticle.velocityOverLifetime;
            velocityModule.xMultiplier = playerVelocity.x > 0 ? playerVelocity.x * -1 : playerVelocity.x;
            velocityModule.yMultiplier = playerVelocity.y;

        }
    }

    void FixedUpdate()
    {
        Movement();     
    }
    // movement
    void Movement()
    {
        rig.velocity = new Vector2(direction.x*speed, rig.velocity.y);
    }
    // jump
    void Jump()
    {
        if(Input.GetKeyDown("w") && canJump == true)
        {
            rig.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            anim.SetInteger("transition",2);
            canJump = false;
        }
    }
    // collision
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == 6)
        {
            canJump = true;
        }
    }
    // death
    void Death()
    {
        if(life <= 0)
        {
            //StopCoroutine(coroutine);
            gameOver.SetActive(true);
            Time.timeScale = 0;
            stopCoroutine = true;
        }
    }

    // damage
    public void Hit(bool permadeath)
    {
        if (permadeath)
        {
            life = life - life;
        }
        if(recovery == false)
        {
            StartCoroutine(Flick());
        }
    }

    IEnumerator Flick()
    {
        recovery = true;
        life -= 1;
        Death();
        lifeText.text = life.ToString();

        for (int i = 0; i < 2; i++) 
        {

            if (stopCoroutine == false)
            {
                sprite.color = new Color(1, 1, 1, 0);
                yield return new WaitForSeconds(0.2f);
                sprite.color = new Color(1, 1, 1, 1);
                yield return new WaitForSeconds(0.2f);
            }
            else
            {
                stopCoroutine = false;
                break;
            }
        }
        recovery = false;
    }

    //fire damage
    public void FireHit()
    {
        if (recovery == false)
        {
            StartCoroutine(FireFlick());
        }
    }

    IEnumerator FireFlick()
    {
        recovery = true;
        fireParticle.enableEmission = true;
        for (int i = 0; i < 5; i++)
        {
            life -= 1;
            Death();
            lifeText.text = life.ToString();
            if (stopCoroutine == false)
            {
                sprite.color = new Color(1.0f, 0.64f, 0.0f, 1);
                yield return new WaitForSeconds(0.2f);
                sprite.color = new Color(1, 1, 1, 1);
                yield return new WaitForSeconds(0.2f);
            }
            else
            {
                stopCoroutine = false;
                break;
            }
        }
        recovery = false;
        fireParticle.enableEmission = false;
    }

    // animations
    void PlayAnimations()
    {
        if(direction.x > 0 && canJump == true)
        {
            anim.SetInteger("transition",1);
            transform.eulerAngles = new Vector2(0,0);
        }
        else if(direction.x < 0 && canJump == true)
        {
            anim.SetInteger("transition",1);
            transform.eulerAngles = new Vector2(0,180);
        }
        else if(canJump == true)
        {
            anim.SetInteger("transition",0);
        }
    }

    public void IncreaseScore()
    {
        strawberry += 1;
        strawberryText.text = strawberry.ToString();
        FinalPoint.finalPointInstance.currentStrawberry += 1;
    }

    public void RestartGame()
    {
        //StopAllCoroutines();
        Scene scene = SceneManager.GetActiveScene();
        gameOver.SetActive(false);
        Time.timeScale = 1;
        life = maxLife;
        lifeText.text = life.ToString();
        strawberry = scene.buildIndex == 1 ? 0 : (strawberry - FinalPoint.finalPointInstance.currentStrawberry);
        strawberryText.text = strawberry.ToString();
        FinalPoint.finalPointInstance.currentStrawberry = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}