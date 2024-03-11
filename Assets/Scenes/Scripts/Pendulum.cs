using UnityEngine;

public class Pendulum : MonoBehaviour
{
    //private HingeJoint2D hj;
    //public float motorSpeed;
    //public float motorSpeedNearLimits;
    //public float maxAngle;
    //public float minAngle;
    //private int direction = 1;
    //public float angleTolerance;


    public Rigidbody2D pendulumRB;
    public float rightRange;
    public float leftRange;
    public float limitVelocity;

    void Start()
    {
        pendulumRB = GetComponent<Rigidbody2D>();
        pendulumRB.angularVelocity = limitVelocity;
    }

    void Update()
    {
        Impulse();
    }

    public void Impulse()
    {
        if (transform.rotation.z > 0 && transform.rotation.z < rightRange && (pendulumRB.angularVelocity > 0) && pendulumRB.angularVelocity < limitVelocity)
        {
            pendulumRB.angularVelocity = limitVelocity;
        }
        else if (transform.rotation.z < 0 && transform.rotation.z > rightRange && (pendulumRB.angularVelocity < 0) && pendulumRB.angularVelocity > limitVelocity * -1)
        {
            pendulumRB.angularVelocity = limitVelocity;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMove>().Hit(false);
        }
    }

    //void Start()
    //{
    //    // Obtém a referência para o componente HingeJoint2D
    //    hj = GetComponent<HingeJoint2D>();

    //    // Configura o motor
    //    JointMotor2D motor = hj.motor;
    //    motor.motorSpeed = motorSpeed * direction; // Multiplica pela direção
    //    motor.maxMotorTorque = 1000; // Torque do motor (pode ajustar conforme necessário)
    //    hj.motor = motor;

    //    // Configura os limites de ângulo
    //    JointAngleLimits2D limits = hj.limits;
    //    limits.min = minAngle;
    //    limits.max = maxAngle;
    //    hj.limits = limits;

    //    // Habilita o motor e os limites
    //    hj.useMotor = true;
    //    hj.useLimits = true;
    //}

    //void Update()
    //{
    //    print(hj.jointAngle + " " + minAngle + " ");
    //    float speed = (Mathf.Abs(hj.jointAngle - maxAngle) < angleTolerance || Mathf.Abs(hj.jointAngle - minAngle) < angleTolerance) ? motorSpeedNearLimits : motorSpeed;
    //    if (Mathf.Abs(hj.jointAngle - maxAngle) < angleTolerance || Mathf.Abs(hj.jointAngle - minAngle) < angleTolerance)
    //    {
    //        // Inverte a direção do movimento
    //        direction *= -1;
    //        // Atualiza a velocidade do motor com a nova direção
    //        JointMotor2D motor = hj.motor;
    //        motor.motorSpeed = speed * direction;
    //        hj.motor = motor;
    //    }
    //}
}
