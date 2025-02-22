using Unity.VisualScripting;
using UnityEngine;

public class SpecialBall : Ball // INHERITANCE
{
    private Rigidbody rB;
    private int pullForceMin = 30;
    private int pullForceMax = 51;
    
    protected override void Start()
    {
        base.Start();
        BallInGame();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (transform.position.z < zBound)
        {
            DestroyBall();
            //gameManager.ResetPowerUp();
        }
    }

    //Coloca a bola automaticamente em jogo
    private void BallInGame()  // ABSTRACTION
    {
        rB = GetComponent<Rigidbody>();
        int pullForce = Random.Range(pullForceMin, pullForceMax);
        rB.AddForce(Vector3.forward * pullForce, ForceMode.Impulse);
    }

    public override void DestroyBall() //POLYMORPHISM (override)
    {
        base.DestroyBall();
        gameManager.powerUpInfoText.SetActive(false);
        gameManager.timer = 0.0f;
        gameManager.hasPowerUp = false;
    }

}
