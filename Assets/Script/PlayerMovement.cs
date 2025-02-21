using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed = 100;
    [SerializeField] private Camera _camera; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed *= 2;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed /= 2;
        }
        

        Vector3 inputDirection = new Vector3(horizontal, 0, vertical);
        
        Quaternion rotation = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0);
        
        Vector3 moveDirection = rotation * inputDirection;

        rb.velocity = moveDirection * (speed * Time.fixedDeltaTime);
    }
}