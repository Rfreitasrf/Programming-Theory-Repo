using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SaveLoadManager : MonoBehaviour
{
    internal int creditsAvalible;
    internal static SaveLoadManager instance; 
    
    [SerializeField] internal TextMeshProUGUI creditsAvalibleText;
    [SerializeField] private string path;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);

            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        path = Application.persistentDataPath + "/savefile.json";
        LoadDatas();
        creditsAvalibleText.text = creditsAvalible + " credits";

        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Updates the reference to Text whenever a new scene is loaded
        if (GameObject.Find("Credits txt"))
        {
            creditsAvalibleText = GameObject.Find("Credits txt").GetComponent<TextMeshProUGUI>();
            creditsAvalibleText.text = creditsAvalible + " credits";
        }
    }


    private void OnDestroy()
    {
        //Remove the event when destroying the object to avoid errors
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        path = Application.persistentDataPath + "/savefile.json";

        creditsAvalibleText.text = creditsAvalible + " credits";

    }


    [System.Serializable]
    class SaveData
    {
        public int credits;
    }


    //Esta sendo chamado pelo botao de sair
    //Verificar um lugar melhor para ser chamado
    public void SaveDatas(int creditsToSave) //definir parametro chamdo creditsToSave 
    {
       
        SaveData saveData = new();
        saveData.credits = creditsToSave;
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(path, json);

        Debug.Log("Dados Foram salvos");
        Debug.Log("Salvo em: " + path);
    }

    public void LoadDatas()
    {
        if (File.Exists(path))
        {
            Debug.Log("Arquivo Json encontrado");

            string json = File.ReadAllText(path);
            SaveData loadData = JsonUtility.FromJson<SaveData>(json);

            creditsAvalible = loadData.credits;
        }
        else
        {
            creditsAvalibleText.text = creditsAvalible + " credits";
            Debug.Log("Arquivo Json não encontrado");
        }
    }
    
}
