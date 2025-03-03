using System.Collections;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Action onPowerUpStart;
    public Action onPowerUpStop;
    
    [SerializeField] private float speed = 100;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float powerUpDuration = 5;

    private Rigidbody rigidBody;
    private Coroutine powerUpCoroutine;
    
    public void PickPowerUp()
    {
        if (powerUpCoroutine != null)
        {
            StopCoroutine(powerUpCoroutine);
        }
        
        powerUpCoroutine = StartCoroutine(StartPowerUp());
    }

    private IEnumerator StartPowerUp()
    {
        if (onPowerUpStart != null)
        {
            onPowerUpStart();
        }
        
        yield return new WaitForSeconds(powerUpDuration);

        if (onPowerUpStop != null)
        {
            onPowerUpStop();
        }
    }
    
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
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
        
        Quaternion rotation = Quaternion.Euler(0, playerCamera.transform.eulerAngles.y, 0);
        
        Vector3 moveDirection = rotation * inputDirection;

        rigidBody.velocity = moveDirection * speed * Time.deltaTime;
    }
}