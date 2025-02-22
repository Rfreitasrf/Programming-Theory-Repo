using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private GameManager gameManager;
    private int rotationSpeed = 100;

    /*
     Esta classe lida com o comportamento dos powerup prefab
     */

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>().GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed *  Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") && !gameManager.hasPowerUp)
        {
            gameManager.hasPowerUp = true;
            Destroy(gameObject);   
        }
    }
}
