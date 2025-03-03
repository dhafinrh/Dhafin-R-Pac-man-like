using UnityEngine;

public class PatrolState : IBaseState
{
    private Vector3 currentDestination;
    private int index;
    private bool isMoving;

    public void EnterState(Enemy enemy)
    {
        isMoving = false;
    }

    public void UpdateState(Enemy enemy)
    {
        if (Vector3.Distance(enemy.transform.position, enemy.Player.transform.position) < enemy.ChaseDistance)
        {
            enemy.SwitchState(enemy.chaseState);
        }
        
        if (!isMoving)
        {
            isMoving = true;
            var newIndex = Random.Range(0, enemy.Waypoints.Count);
            currentDestination = enemy.Waypoints[newIndex].position;
            enemy.enemyAgent.destination = currentDestination;
        }
        else
        {
            if (Vector3.Distance(currentDestination, enemy.transform.position) <= 1f)
            {
                isMoving = false;
            }
        }
    }

    public void ExitState(Enemy enemy)
    {
    }
}