using System.Collections.Generic;
using UnityEngine;

public class PickableManager : MonoBehaviour
    {
       [SerializeField] private List<Pickable> pickableList = new List<Pickable>();
       [SerializeField] private Player player;
       
        private void Start()
        {
            InitPickableList();
        }

        private void InitPickableList()
        {
            Pickable[] pickableObjects = FindObjectsOfType<Pickable>();
            for (int i = 0; i < pickableObjects.Length; i++)
            {
                pickableList.Add(pickableObjects[i]);
                pickableObjects[i].OnPicked += OnPickablePicked;
            }
        }
        private void OnPickablePicked(Pickable pickable)
        {
            pickableList.Remove(pickable);

            if (pickable.PickableType == PickableTypes.PowerUp)
            {
                player.PickPowerUp();
                Debug.Log("PowerUp PickedUp!");
            }
            
            if (pickableList.Count <= 0)
            {
                Debug.Log("Win");
            }

        }
    }
