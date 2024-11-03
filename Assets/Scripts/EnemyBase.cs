using UnityEngine;

public abstract class EnemyBase : EntityBase
{
    public enum State
    {
        Idle,
        Attack,
        Run,
        Dead,
    }

    protected void ChangeState(State newState)
    {

    }
}
