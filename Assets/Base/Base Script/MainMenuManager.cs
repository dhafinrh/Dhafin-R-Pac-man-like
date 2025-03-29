using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("NewGameplay");
    }

    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}