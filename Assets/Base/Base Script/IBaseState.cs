using UnityEngine;

public interface IBaseState
{
    public void EnterState(Enemy enemy, Animator animator);
    public void UpdateState(Enemy enemy);
    public void ExitState(Enemy enemy);
}