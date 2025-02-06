using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int score;
    private GameObject[] balls;
    private int quantBalls;

    [SerializeField] private GameObject ball;
    [SerializeField] private TextMeshProUGUI scoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Verifica quantas bolas há em jogo no comeco do jogo
        balls = GameObject.FindGameObjectsWithTag("Ball");
        quantBalls = balls.Length;
        

        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        //Verifica quantas bolas há em jogo atualmente
        balls = GameObject.FindGameObjectsWithTag("Ball");
        quantBalls = balls.Length;
        

        if (quantBalls < 1)
        {
            //chama Metodo que cria uma nova bola no ponto inicial 
            NewBall();
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;

    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }

    private void NewBall()
    {
        Vector3 ballPos = new Vector3(2.8f, 0.35f, -1.0f);
        Instantiate(ball, ballPos, ball.transform.rotation);
    
    }
}
