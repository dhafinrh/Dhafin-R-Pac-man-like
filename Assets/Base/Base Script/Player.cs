using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 100;
    [SerializeField] private float runSpeed = 2;
    [SerializeField] private Transform _camera;
    [SerializeField] private Animator animator;
    [SerializeField] private float powerUpDuration = 5;
    [SerializeField] private AudioSource powerUpSFX;
    [SerializeField] private AudioSource deadAudioSource;


    [Header("Ground Check Settings")] 
    
    [SerializeField] private Transform leftFoot;
    [SerializeField] private Transform rightFoot;
    [SerializeField] private float groundCheckDistance = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    private readonly float rotationTime = 0.1f;
    private bool isCrouching;
    private bool isGrounded;
    private bool isPowerUpActive;

    private Coroutine powerUpCoroutine;
    private Rigidbody rigidBody;
    private float rotationVelocity;

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
        HandleMovement();
    }

    private void OnDestroy()
    {
        onPlayerDeath = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isPowerUpActive && collision.gameObject.CompareTag("Enemy"))
        {
            onVFXTriggered.Invoke();
            deadAudioSource.Play();
            collision.gameObject.GetComponent<Enemy>().Dead();
        }
    }

    private void OnDrawGizmos()
    {
        if (leftFoot && rightFoot)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(leftFoot.position, leftFoot.position + Vector3.down * groundCheckDistance);
            Gizmos.DrawLine(rightFoot.position, rightFoot.position + Vector3.down * groundCheckDistance);
        }
    }

    public event Action onVFXTriggered;
    public event Action onPowerUpStart;
    public event Action onPowerUpStop;
    public event Action onPlayerDeath;

    public void PickPowerUp()
    {
        if (powerUpCoroutine != null) StopCoroutine(powerUpCoroutine);
        powerUpCoroutine = StartCoroutine(StartPowerUp());
    }

    private IEnumerator StartPowerUp()
    {
        isPowerUpActive = true;
        powerUpSFX.Play();
        onPowerUpStart?.Invoke();
        onVFXTriggered.Invoke();
        yield return new WaitForSeconds(powerUpDuration);
        isPowerUpActive = false;
        onPowerUpStop?.Invoke();
    }

    public void Die()
    {
        Debug.Log("Player Mati");
        onPlayerDeath?.Invoke();
    }

    private void HandleMovement()
    {
        isGrounded = IsGrounded();
        animator.SetBool("isGrounded", isGrounded);
        Debug.Log("isGrounded? "+isGrounded);

        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetBool("isRunning", true);
            speed *= runSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("isRunning", false);
            speed /= runSpeed;
        }
        else if (Input.GetKeyDown(KeyCode.C) && !Input.GetKeyDown(KeyCode.LeftShift))
        {
            isCrouching = !isCrouching;
            animator.SetBool("isCrouch", isCrouching);
            speed *= isCrouching ? 0.5f : 2;
        }

        var movementDirection = new Vector3(horizontal, 0, vertical);
        if (movementDirection.magnitude >= 0.1f)
        {
            var rotationAngle = Mathf.Atan2(movementDirection.x, movementDirection.z) * Mathf.Rad2Deg +
                                _camera.eulerAngles.y;
            var smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref rotationVelocity,
                rotationTime);
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
            movementDirection = Quaternion.Euler(0f, rotationAngle, 0f) * Vector3.forward;
        }

        var targetVelocity = new Vector3(
            movementDirection.x * speed * Time.deltaTime,
            rigidBody.velocity.y,
            movementDirection.z * speed * Time.deltaTime
        );

        rigidBody.velocity = targetVelocity;
        animator.SetFloat("Velocity", rigidBody.velocity.magnitude);
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(leftFoot.position, Vector3.down, groundCheckDistance, groundLayer) ||
               Physics.Raycast(rightFoot.position, Vector3.down, groundCheckDistance, groundLayer);
    }
}