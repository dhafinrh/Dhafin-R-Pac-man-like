using System.Collections.Generic;
using UnityEngine;

public class PickableManager : MonoBehaviour
    {
       [SerializeField] private List<Pickable> pickableList = new List<Pickable>();
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
            Debug.Log("Pickable List: "+pickableList.Count);
        }
        private void OnPickablePicked(Pickable pickable)
        {
            pickableList.Remove(pickable);
                Debug.Log("Removing" + pickable.name);
                
            if (pickableList.Count <= 0)
            {
                Debug.Log("Win");
            }

        }
    }
