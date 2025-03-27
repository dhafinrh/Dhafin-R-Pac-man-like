using UnityEngine;

public class RetreatState : IBaseState
{
    public void EnterState(Enemy enemy, Animator animator)
    {
        animator.SetBool("isRunning", true);
    }

    public void UpdateState(Enemy enemy)
    {
        if (enemy.PlayerMovement != null)
        {
            Debug.Log("Kabur Cuy");

            // enemy.enemyAgent.destination = enemy.transform.position - enemy.Player.transform.position;
            enemy.enemyAgent.destination = enemy.transform.position +
                                           (enemy.transform.position - enemy.PlayerMovement.transform.position)
                                           .normalized;
            enemy.enemyAgent.speed = 4;
        }
    }

    public void ExitState(Enemy enemy)
    {
    }
}