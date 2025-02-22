using UnityEngine;

public class Ball : MonoBehaviour
{
    protected float zBound { get; private set; } = - 5.3f; // ENCAPSULATION
    protected GameManager gameManager { get; private set; } // ENCAPSULATION
    [SerializeField] private GameObject ballParticle;
    [SerializeField] private AudioClip explosionSound;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>().GetComponent<GameManager>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //Destroi bola em caso de powerup ou bola normal sair da area de jogo
        if (gameManager.hasPowerUp || transform.position.z < zBound)
        {
            DestroyBall();
        }
    }

    //Destroi a bola, emite efeitos sonoro e visual.
    public virtual void DestroyBall()  // ABSTRACTION 
    {
        // Cria um GameObject temporário para o som
        GameObject tempAudioSoucer = new GameObject("TempAudioSource");
        AudioSource audioSource = tempAudioSoucer.AddComponent<AudioSource>();

        // Configura o som
        audioSource.clip = explosionSound;
        audioSource.volume = 1.0f;
        audioSource.Play();

        // Destroi o GameObject temporario depois que o som termina de tocar
        Destroy(tempAudioSoucer, explosionSound.length);

        // Instancia a partícula e destrói a bola
        Instantiate(ballParticle, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);

        //Desconta 1 credito a cada bola normal perdida
        if (gameManager.hasPowerUp)
            gameManager.credits -= 0;
        else
        {
            gameManager.credits -= 1;
            gameManager.creditsText.text = "Credits: " + gameManager.credits;
            gameManager.GameOver();
        }
    }
}
