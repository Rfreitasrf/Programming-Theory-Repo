using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float torqueForce = 1000;
    [SerializeField] private float motorSpeed = 1000;
    
    private HingeJoint flipperHinge;
    private GameManager gameManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PaddleConfig();
        gameManager = FindAnyObjectByType<GameManager>().GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameManager.isGameOver)
            FlipperControl();
    }


    // ABSTRACTION (All methods down below)

    //Configura as paletas ao iniciar a cena
    public void PaddleConfig()
    {
        flipperHinge = GetComponent<HingeJoint>();

        JointMotor motor = flipperHinge.motor;
        motor.force = torqueForce;
        motor.targetVelocity = 0;
        motor.freeSpin = false;

        flipperHinge.motor = motor;
        flipperHinge.useMotor = false;
    }


    //Controla ambas as paletas
    public void FlipperControl()
    {
        //Controla paleta direita
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (gameObject.CompareTag("FlipperR"))
            {
                JointMotor motor = flipperHinge.motor;
                motor.targetVelocity = motorSpeed;

                flipperHinge.motor = motor;
                flipperHinge.useMotor = true;
            }
        }

        //Controla paleta esquerda
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (gameObject.CompareTag("FlipperL"))
            {
                JointMotor motor = flipperHinge.motor;
                motor.targetVelocity = motorSpeed;
                
                flipperHinge.motor = motor;
                flipperHinge.useMotor = true;
            }
        }

        //Peleta volta á posicao inicial
        else
        {
            flipperHinge.useMotor = false;
        }
    }
}
