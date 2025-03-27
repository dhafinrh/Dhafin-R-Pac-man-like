using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagers : MonoBehaviour
{
    [SerializeField] private bool unlockCursor;

    private void Awake()
    {
        VisibilityCursor(unlockCursor);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        LoadScene("Main Menu");
    }

    public void LoadGameplay()
    {
        LoadScene("Gameplay");
    }

    public void ExitGame()
    {
        Debug.Log("Game Exit");
        Application.Quit();
    }

    private void VisibilityCursor(bool value)
    {
        Cursor.visible = value;

        if (value)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }
}