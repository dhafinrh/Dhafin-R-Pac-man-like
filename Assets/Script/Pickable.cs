using System;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    [SerializeField] private PickableTypes pickableType;
    public Action<Pickable> OnPicked;

    public PickableTypes PickableType
    {
        get => pickableType;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(OnPicked != null)
            {
                OnPicked(this);
            }
            Destroy(gameObject);
        }
    }
}