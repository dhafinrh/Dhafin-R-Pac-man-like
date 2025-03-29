using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private List<Transform> waypoints = new();
    [SerializeField] private float chaseDistance;
    [SerializeField] private Player player;
    [SerializeField] private TMP_Text stateText;
    [HideInInspector] public Animator animator;
    [HideInInspector] public NavMeshAgent enemyAgent;

    [HideInInspector] public ChaseState chaseState = new();

    private IBaseState currentState;
    private bool isDestroyed;
    [HideInInspector] public PatrolState patrolState = new();
    [HideInInspector] public RetreatState retreatState = new();

    [SerializeField] private AudioSource IdleAudioSource;
    [SerializeField] private Vector2 intervalRange = new(5f, 10f);
    public AudioSource alertAudioSource;
    public List<Transform> Waypoints => waypoints;
    public Player Player => player;
    public float ChaseDistance => chaseDistance;
    public static bool CanPlaySound = true;


    public void Awake()
    {
        animator = GetComponent<Animator>();
        enemyAgent = GetComponent<NavMeshAgent>();

        currentState = patrolState;
        currentState.EnterState(this, animator);
        UpdateStateText();
        StartCoroutine(PlaySoundRandomly());
    }

    private void Start()
    {
        if (player != null)
        {
            player.onPowerUpStart += StartRetreat;
            player.onPowerUpStop += StopRetreat;
        }
    }

    private void Update()
    {
        if (isDestroyed) return;

        if (currentState != null)
            currentState.UpdateState(this);

        animator.SetFloat("Velocity", enemyAgent.velocity.magnitude);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (currentState != retreatState)
            if (collision.gameObject.CompareTag("Player"))
            {
                var playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
                if (playerHealth != null) playerHealth.TakeDamage(1);
                SwitchState(patrolState);
            }
    }

    public void SwitchState(IBaseState newState)
    {
        if (this != null || gameObject.activeSelf)
        {
            currentState.ExitState(this);
            currentState = newState;
            currentState.EnterState(this, animator);

            UpdateStateText();
        }
    }

    private void StartRetreat()
    {
        SwitchState(retreatState);
    }

    private void StopRetreat()
    {
        SwitchState(patrolState);
    }

    private void UpdateStateText()
    {
        if (stateText != null) stateText.text = "Current State: " + currentState.GetType().Name;
    }

    public void Dead()
    {
        if (isDestroyed) return;

        isDestroyed = true;
        if (player != null)
        {
            player.onPowerUpStart -= StartRetreat;
            player.onPowerUpStop -= StopRetreat;
        }

        gameObject.SetActive(false);
    }
    
    private IEnumerator PlaySoundRandomly()
    {
        while (true)
        {
            float waitTime = Random.Range(intervalRange.x, intervalRange.y);
            yield return new WaitForSeconds(waitTime);

            if (CanPlaySound && IdleAudioSource != null)
            {
                IdleAudioSource.Play();
            }
        }
    }
}