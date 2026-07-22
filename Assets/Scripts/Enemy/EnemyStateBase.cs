namespace EnemyAI
{
    /// <summary>
    /// Base class for enemy states, providing a structure for entering, ticking, and exiting states.
    /// </summary>
    public abstract class EnemyStateBase : IState
    {
        protected readonly EnemyController Enemy;

        protected EnemyStateBase(EnemyController enemy)
        {
            Enemy = enemy;
        }

        public virtual void Enter() { }
        public virtual void Tick() { }
        public virtual void FixedTick() { }
        public virtual void Exit() { }
    }
}
