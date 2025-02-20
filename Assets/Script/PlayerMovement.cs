using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private float speed;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {          
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, 0, vertical);
        rb.velocity = movement.normalized * speed * Time.deltaTime;
    }
}
