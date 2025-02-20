using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;



#if UNITY_EDITOR
using UnityEngine;
#endif

/*
 * It handles all button navigation between scenes and game exit.
 */
public class MenuUIHandler : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI creditInfoText;
    private Color highlightColor = Color.red;
    private Color originalColor;
    private AudioSource menuUIHandlerAudio;
    [SerializeField] private AudioClip creditSound;


    private void Start()
    {
        if(creditInfoText != null)
            originalColor =  creditInfoText.color;

        menuUIHandlerAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        InsertCredit();
    }


    public void MainScene()
    {
        if (SaveLoadManager.instance.creditsAvalible > 0)
            SceneManager.LoadScene(1);
        else
        {
            StopAllCoroutines();
            StartCoroutine(BlinkEffect());
        }
    }

    public void MainMenu()
    {
        gameManager = FindAnyObjectByType<GameManager>().GetComponent<GameManager>();
        SaveLoadManager.instance.creditsAvalible = gameManager.credits;
        SceneManager.LoadScene(0);
    }

    public void BestPlayersList()
    {
        SceneManager.LoadScene(2);
    }

    //Persiste o dado no HD e sai do jogo
    public void Exit()
    {
        
        SaveLoadManager.instance.SaveDatas(SaveLoadManager.instance.creditsAvalible);

#if UNITY_EDITOR

        EditorApplication.ExitPlaymode();
#else
        Aplication.Quit();
#endif

    }

    //Adaptado do metodo CreditManager() da classe GameManager()
    //Refatorar esse codigo no futuro
    public void InsertCredit()
    {
        //Adiciona crédito
        if (Input.GetKeyDown(KeyCode.C))
        {
            SaveLoadManager.instance.creditsAvalible += 1;
            SaveLoadManager.instance.creditsAvalibleText.text = "Credits: " + SaveLoadManager.instance.creditsAvalible;
            
            if (creditSound != null)
            {
                menuUIHandlerAudio.PlayOneShot(creditSound);
            }
            
            Debug.Log($"1 crédito foi inserido: {SaveLoadManager.instance.creditsAvalible}");
        }
        
    }

    public IEnumerator BlinkEffect()
    {
        creditInfoText.color = highlightColor;

        for (int i = 0; i < 3; i++)
        {
            creditInfoText.enabled = false;
            yield return new WaitForSeconds(0.2f);
            creditInfoText.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }

        creditInfoText.color = originalColor;
    }


}
