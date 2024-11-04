using UnityEngine;
using VInspector;

public abstract class EnemyBase : EntityBase
{
    public enum State
    {
        //Idle,
        Find,
        Chase,
        Attack,
        Dead,
    }

    [ReadOnly][ShowInInspector]
    protected State currState { get; private set; }

    protected virtual void ChangeState(State newState)
    {
        currState = newState;
    }
}
