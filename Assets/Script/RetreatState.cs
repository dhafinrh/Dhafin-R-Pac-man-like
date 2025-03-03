using UnityEngine;

public class RetreatState : IBaseState
{
    public void EnterState(Enemy enemy)
    {
    }

    public void UpdateState(Enemy enemy)
    {
        if (enemy.Player != null)
        {
            Debug.Log("Kabur Cuy");
            
            enemy.enemyAgent.destination = enemy.transform.position - enemy.Player.transform.position;;
        }
    }

    public void ExitState(Enemy enemy)
    {
    }
}