using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 100;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float powerUpDuration = 5;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private int health;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private GameObject loseUI;


    private bool isPowerUpActive;
    public Action onPowerUpStart;
    public Action onPowerUpStop;
    private Coroutine powerUpCoroutine;

    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        UpdateUI();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift))
            speed *= 2;
        else if (Input.GetKeyUp(KeyCode.LeftShift)) speed /= 2;

        var inputDirection = new Vector3(horizontal, 0, vertical);

        var rotation = Quaternion.Euler(0, playerCamera.transform.eulerAngles.y, 0);

        var moveDirection = rotation * inputDirection;

        rigidBody.velocity = moveDirection * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isPowerUpActive)
            if (collision.gameObject.CompareTag("Enemy"))
                collision.gameObject.GetComponent<Enemy>().Dead();
    }

    private void UpdateUI()
    {
        healthText.text = "Health: " + health;
    }

    public void PickPowerUp()
    {
        if (powerUpCoroutine != null) StopCoroutine(powerUpCoroutine);

        powerUpCoroutine = StartCoroutine(StartPowerUp());
    }

    private IEnumerator StartPowerUp()
    {
        isPowerUpActive = true;
        if (onPowerUpStart != null) onPowerUpStart();

        yield return new WaitForSeconds(powerUpDuration);

        isPowerUpActive = false;
        if (onPowerUpStop != null) onPowerUpStop();
    }

    public void Dead()
    {
        Debug.Log("Kena DMG");
        health -= 1;
        if (health > 0)
        {
            transform.position = respawnPoint.position;
        }
        else
        {
            health = 0;
            Debug.Log("Lose");
            LoseGame();
        }
        UpdateUI();
    }
    
    public void LoseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        loseUI.SetActive(true);
    }
}