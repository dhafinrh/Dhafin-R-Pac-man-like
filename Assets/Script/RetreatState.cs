using UnityEngine;

public class RetreatState : IBaseState
{
    public void EnterState(Enemy enemy)
    {
        if (enemy != null && enemy.animator != null)
            enemy.animator.SetTrigger("RetreatState");
    }

    public void UpdateState(Enemy enemy)
    {
        if (enemy.Player != null)
        {
            Debug.Log("Kabur Cuy");

            // enemy.enemyAgent.destination = enemy.transform.position - enemy.Player.transform.position;
            enemy.enemyAgent.destination = enemy.transform.position +
                                           (enemy.transform.position - enemy.Player.transform.position).normalized;
        }
    }

    public void ExitState(Enemy enemy)
    {
    }
}