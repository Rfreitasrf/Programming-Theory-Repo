using UnityEngine;

public class PullSpring : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float springForce;
    [SerializeField] private bool isCharged;
    private float startPos;
    private float currentPos;
    private float zLimitPos;
    private Rigidbody springRb;

    private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpringConfig();

        gameManager = FindAnyObjectByType<GameManager>().GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isGameOver)
            SpringBehaviour();
    }


    // ABSTRACTION (All methods down below)

    //Configura a mola ao iniciar a cena
    public void SpringConfig()
    {
        startPos = transform.position.z;
        isCharged = false;
        zLimitPos = startPos - 2.0f;
        springRb = GetComponent<Rigidbody>();
    }

    //Define todo o comportamento da mola
    public void SpringBehaviour()
    {
        //Recebe o input do player que controla a mola
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(verticalInput * speed * Time.deltaTime * Vector3.forward);

        //Monitora posicao de z da mola
        currentPos = transform.position.z;

        //Estabelece limites da mola
        if (currentPos < zLimitPos)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zLimitPos);
        }

        if (currentPos > startPos)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, startPos);

            //A mola sempre volta um pouco acima da pos inicial
            //Codigo esta aqui por falta de lugar melhor
            springRb.isKinematic = true;
        }

        //Rebilita a fisica para poder atuar na mola
        if (currentPos < startPos && Input.GetKey(KeyCode.DownArrow))
        {
            isCharged = true;
            springRb.isKinematic = false;
        }

        //Aplica uma forca na mola quando solta pelo player
        if (isCharged && Input.GetKeyUp(KeyCode.DownArrow))
        {
            isCharged = false;
            springRb.AddForce(Vector3.forward * springForce, ForceMode.Impulse);
        }
    }
}
