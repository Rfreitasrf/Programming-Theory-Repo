using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    internal int credits; //Melhorar proteção no futuro
    internal float timer; //Melhorar proteção no futuro
    [SerializeField] internal TextMeshProUGUI creditsText; //Melhorar proteção no futuro
    [SerializeField] internal GameObject powerUpInfoText; //Melhorar proteção no futuro
    [SerializeField] internal bool hasPowerUp; //Melhorar proteção no futuro

    public bool isGameOver { get; private set; } // ENCAPSULATION

    private bool paused;
    private int score;
    private int powerUpScore;
    private int powerUpDuration = 15;
    private GameObject[] balls;
    private GameObject TempSpecialBall;
    private AudioSource gameManagerAudio;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject specialBall;
    [SerializeField] private GameObject powerUpPrefab;
    [SerializeField] private AudioClip creditSound;
    [SerializeField] private GameObject GameOverPanel;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartGameConfig();   
    }

    // Update is called once per frame
    void Update()
    { 
        CreditManager();

        //verifica se o player coletou o power up para iniciar o contador
        if (hasPowerUp)
            PowerUpTimer(); 
        
        //Verifica se o jogo esta ativo e chama o metodo que verifica se há bola em jogo
        if(!isGameOver)
            CheckQuantBalls();

        //Check if the user has pressed the P key
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangePaused();
        }  
    }


    // ABSTRACTION (All methods down below)

    //Configuracao inicial do jogo
    private void StartGameConfig()
    {
        credits = SaveLoadManager.instance.creditsAvalible;
        creditsText.text = "Credits: " + credits;

        //Acessa audio Source no Game Manager
        gameManagerAudio = GetComponent<AudioSource>();

        //Verifica quantas bolas há em jogo no comeco do jogo
        CheckQuantBalls();

        scoreText.text = "Score: " + score;

        //Previne o jogo de comecar em pausa
        Time.timeScale = 1;
    }

    //Verifica quantas bolas há em jogo atualmente
    private void CheckQuantBalls()
    {
        //Verifica quantas bolas há em jogo atualmente
        balls = GameObject.FindGameObjectsWithTag("Ball");
        
        if (balls.Length < 1)
        {
            //chama Metodo que cria uma nova bola no ponto inicial 
            NewBall();
        }
    }

    //Atualiza a pontuacao - possui sobrecarga 
    public void UpdateScore(int scoreToAdd) 
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;

        powerUpScore += scoreToAdd;

        if (powerUpScore > 500)
        {
            PowerUpInstanciate(powerUpPrefab);
            powerUpScore = 0;
        }
    }

    public void UpdateScore(int scoreToAdd, int multiply) //POLYMORPHISM (overload)
    {
        score += (scoreToAdd * multiply);
        scoreText.text = "Score: " + score;

        powerUpScore = 0;
    }

    //Cria uma nova bola no ponto inicial
    private void NewBall()
    {
        if (hasPowerUp)
        {
            //instancia da bola especial
            Vector3 ballPos = new Vector3(2.8f, 0.35f, -1.0f);
            Instantiate(specialBall, ballPos, specialBall.transform.rotation);

            powerUpInfoText.SetActive(true);
        }
        else
        {
            //instancia da bola normal
            Vector3 ballPos = new Vector3(2.8f, 0.35f, -1.0f);
            Instantiate(ball, ballPos, ball.transform.rotation);
        }
    }

    //Gerencia a pausa do jogo
    private void ChangePaused()
    {
        if (!paused)
        {
            paused = true;
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
    }

    //Reseta powerup to false e remove bola especial
    public void ResetPowerUp()
    {
        TempSpecialBall = GameObject.Find("Special Ball(Clone)");

        if (TempSpecialBall != null)
        {
            powerUpInfoText.SetActive(false);

            SpecialBall temp = TempSpecialBall.GetComponent<SpecialBall>();
            temp.DestroyBall();
            timer = 0.0f;
            hasPowerUp = false;
        }   
    }

    //Instancia um powerup prefab
    public void PowerUpInstanciate(GameObject powerUpPrefab)
    {
        float randomPosX = Random.Range(-2.5f, 1.6f);
        float randomPosZ = Random.Range(0.0f, -2.0f);
        float posY = 0.35f;

        Vector3 powerUpPos = new Vector3(randomPosX, posY,randomPosZ);
        Instantiate(powerUpPrefab, powerUpPos, powerUpPrefab.transform.rotation);
    }

    //Define um tempo de duração do PowerUp
    private void PowerUpTimer()
    {
        timer += Time.deltaTime;
        timerText.text = "Timer: " + Mathf.Round(powerUpDuration - timer);

        if (timer >= powerUpDuration)
        {
            Debug.Log("Fim do POWER UP");
            ResetPowerUp();
        }
    }

    //Gerencia o sistema de creditos na cena principal
    public void CreditManager()
    {
        //Adiciona crédito
        if (Input.GetKeyDown(KeyCode.C) & isGameOver)
        {
            credits += 1;
            creditsText.text = "Credits: " + credits;
            gameManagerAudio.PlayOneShot(creditSound);
            Debug.Log($"1 crédito foi inserido: {credits}");
        }
    }

    public void GameOver()
    {
        //Condicao para Game Over
        if (credits <= 0)
        {
            isGameOver = true;
            GameOverPanel.SetActive(true);
            Debug.Log("Game Over! - Enter para inserir créditos");
        }
    }

    //Retoma o jogo apos Game Over e player colocar credito
    public void ResumeGame()
    {
        if (credits > 0)
        {
            isGameOver = false;
            GameOverPanel.SetActive(false);
        }
        else
        {
            MenuUIHandler menuhandler = FindAnyObjectByType<MenuUIHandler>().GetComponent<MenuUIHandler>();
            StopAllCoroutines();
            StartCoroutine(menuhandler.BlinkEffect());
        }
    }
}
