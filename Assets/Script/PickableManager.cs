using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickableManager : MonoBehaviour
{
    [SerializeField] private List<Pickable> pickableList = new();
    [SerializeField] private Player player;
    [SerializeField] private ScoreManager scoreManager;

    private void Start()
    {
        InitPickableList();
    }

    private void InitPickableList()
    {
        var pickableObjects = FindObjectsOfType<Pickable>();
        for (var i = 0; i < pickableObjects.Length; i++)
        {
            pickableList.Add(pickableObjects[i]);
            pickableObjects[i].OnPicked += OnPickablePicked;
        }

        scoreManager.SetMaxScore(pickableList.Count);
        Debug.Log(pickableList.Count);
    }

    private void OnPickablePicked(Pickable pickable)
    {
        pickableList.Remove(pickable);

        if (pickable.PickableType == PickableTypes.PowerUp)
        {
            player.PickPowerUp();
            Debug.Log("PowerUp PickedUp!");
        }

        if (scoreManager != null) scoreManager.AddScore(1);

        if (pickableList.Count <= 0) SceneManager.LoadScene("WinScreen");
    }
}