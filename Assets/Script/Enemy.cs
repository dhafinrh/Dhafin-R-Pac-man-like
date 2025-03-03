using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Enemy : MonoBehaviour
{
    [SerializeField] private List<Transform> waypoints = new List<Transform>();
    [SerializeField] private float chaseDistance;
    [SerializeField] private Player player;
    [SerializeField] private TMP_Text stateText; 

    private IBaseState currentState;

    [HideInInspector] public PatrolState patrolState = new PatrolState();
    [HideInInspector] public ChaseState chaseState = new ChaseState();
    [HideInInspector] public RetreatState retreatState = new RetreatState();

    public NavMeshAgent enemyAgent;
    public List<Transform> Waypoints { get => waypoints; }
    public Player Player { get => player; }
    public float ChaseDistance { get => chaseDistance; }

    public void Awake()
    {
        currentState = patrolState;
        currentState.EnterState(this);
        enemyAgent = GetComponent<NavMeshAgent>();
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
        if (currentState != null)
        {
            currentState.UpdateState(this);
        }
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
        if (stateText != null)
        {
            stateText.text = "Current State: " + currentState.GetType().Name;
        }
    }
}