using UnityEngine;
using UnityEngine.Windows;


public class PlayerController : MonoBehaviour
{
    private HingeJoint paddleHinge;
    [SerializeField] private float torqueForce = 1000;
    [SerializeField] private float motorSpeed = 1000;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        paddleHinge = GetComponent<HingeJoint>();

        JointMotor motor = paddleHinge.motor;
        motor.force = torqueForce;
        motor.targetVelocity = 0;
        motor.freeSpin = false;

        paddleHinge.motor = motor;
        paddleHinge.useMotor = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.Input.GetKey(KeyCode.RightArrow))
        {

            if (gameObject.CompareTag("FlipperR"))
            {
                JointMotor motor = paddleHinge.motor;
                motor.targetVelocity = motorSpeed;
                paddleHinge.motor = motor;
                paddleHinge.useMotor = true;
            }


        }

        else if (UnityEngine.Input.GetKey(KeyCode.LeftArrow))
        {

            if (gameObject.CompareTag("FlipperL"))
            {
                JointMotor motor = paddleHinge.motor;
                motor.targetVelocity = motorSpeed;
                paddleHinge.motor = motor;
                paddleHinge.useMotor = true;
            }
        }

        else
        {
            paddleHinge.useMotor = false;

        }


    }
}
