using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private float _speed;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {          
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
    }
}
