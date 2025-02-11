using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int score;
    private GameObject[] balls;
    private int quantBalls;

    [SerializeField] private GameObject ball;
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private GameObject pauseScreen;
    private bool paused;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Verifica quantas bolas há em jogo no comeco do jogo
        balls = GameObject.FindGameObjectsWithTag("Ball");
        quantBalls = balls.Length;
      
        scoreText.text = "Score: " + score;

        //Previne o jogo de comecar em pausa
        Time.timeScale = 1;
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

        //Check if the user has pressed the P key
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                ChangePaused();
            }
        
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

    private void ChangePaused()
    {
        if (!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }

    
    }
}
