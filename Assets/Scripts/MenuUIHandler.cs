using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEditor;


#if UNITY_EDITOR
using UnityEngine;
#endif

/*
 * It handles all button navigation between scenes and game exit.
 */
public class MenuUIHandler : MonoBehaviour
{
    public void MainScene()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void BestPlayersList()
    {
        SceneManager.LoadScene(2);
    }

    public void Exit()
    {

#if UNITY_EDITOR

        EditorApplication.ExitPlaymode();
#else
        Aplication.Quit();
#endif

    }


}
