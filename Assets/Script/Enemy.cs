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

    private IBaseState currentState;

    [HideInInspector] public ChaseState chaseState = new();
    [HideInInspector] public PatrolState patrolState = new();
    [HideInInspector] public RetreatState retreatState = new();

    public List<Transform> Waypoints => waypoints;
    public Player Player => player;
    public float ChaseDistance => chaseDistance;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        enemyAgent = GetComponent<NavMeshAgent>();

        currentState = patrolState;
        currentState.EnterState(this);
        UpdateStateText();
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
        if (currentState != null) currentState.UpdateState(this);
    }

    public void SwitchState(IBaseState newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);

        UpdateStateText();
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
        Destroy(gameObject);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if(currentState != retreatState)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Player>().Dead();
                SwitchState(patrolState);
            }
        }
    }
}