using UnityEngine;

public class ChaseState : IBaseState
{
    public void EnterState(Enemy enemy, Animator animator)
    {
        animator.SetBool("isRunning", true);
        enemy.alertAudioSource.Play();
    }

    public void UpdateState(Enemy enemy)
    {
        if (enemy.Player != null)
        {
            Debug.Log("Kejar Cuy");

            enemy.enemyAgent.speed = 4;
            enemy.enemyAgent.destination = enemy.Player.transform.position;
            if (Vector3.Distance(enemy.transform.position, enemy.Player.transform.position) >
                enemy.ChaseDistance) enemy.SwitchState(enemy.patrolState);
        }
    }

    public void ExitState(Enemy enemy)
    {
    }
}