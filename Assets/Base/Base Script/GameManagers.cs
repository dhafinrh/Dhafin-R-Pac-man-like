using UnityEngine;

public class GameManagers : MonoBehaviour
{
    [SerializeField] private SceneManagers sceneManagers;
    [SerializeField] private Player player;

    private void Awake()
    {
        if (player != null) player.onPlayerDeath += HandlePlayerDeath;
    }

    private void OnDestroy()
    {
        if (player != null) player.onPlayerDeath -= HandlePlayerDeath;
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