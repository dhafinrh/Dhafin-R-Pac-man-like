using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidBody;
    [SerializeField] private float _speed = 100;
    [SerializeField] private Camera _camera; // Tambahkan kamera yang digunakan untuk orientasi

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
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

        Vector3 inputDirection = new Vector3(horizontal, 0, vertical);
        Quaternion rotation = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0);
        Vector3 moveDirection = rotation * inputDirection;

        _rigidBody.velocity = moveDirection.normalized * _speed * Time.deltaTime;
    }
}