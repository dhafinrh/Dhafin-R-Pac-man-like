using UnityEngine;

public class GameManagers : MonoBehaviour
{
    [SerializeField] private SceneManagers sceneManagers;
    [SerializeField] private PlayerHealth player;

    private void Awake()
    {
        if (player != null)
        {
            player.OnPlayerDeath += HandlePlayerDeath;
            Debug.Log("Sudah Subs OnPlayerDeath");
        }
    }

    private void OnDestroy()
    {
        if (player != null) player.OnPlayerDeath -= HandlePlayerDeath;
    }

    private void HandlePlayerDeath()
    {
        Debug.Log("Player Mati, memanggil SceneManagers untuk load LoseScreen");
        if (sceneManagers != null)
            sceneManagers.LoadScene("LoseScreen");
        else
            Debug.LogWarning("SceneManagers tidak ditemukan!");
    }
}