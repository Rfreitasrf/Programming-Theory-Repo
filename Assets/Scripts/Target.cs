using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] int pointValue; //valor de cada target
    private GameManager gameManager;
  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameManager.UpdateScore(pointValue);
    }
}
