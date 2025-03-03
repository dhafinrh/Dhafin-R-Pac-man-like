using System;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    [SerializeField] private PickableType pickableType;
    public Action<Pickable> OnPicked;
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