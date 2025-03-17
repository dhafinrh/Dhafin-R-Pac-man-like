using UnityEngine;

public class ChaseState : IBaseState
{
    public void EnterState(Enemy enemy)
    {
        if (enemy != null && enemy.animator != null)
            enemy.animator.SetTrigger("ChaseState");
    }

    public void UpdateState(Enemy enemy)
    {
        if (enemy.Player != null)
        {
            Debug.Log("Kejar Cuy");

            enemy.enemyAgent.destination = enemy.Player.transform.position;
            if (Vector3.Distance(enemy.transform.position, enemy.Player.transform.position) > enemy.ChaseDistance)
                enemy.SwitchState(enemy.patrolState);
        }
    }

    public void ExitState(Enemy enemy)
    {
    }
}