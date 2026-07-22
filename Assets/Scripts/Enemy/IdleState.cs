namespace EnemyAI
{
    public class IdleState : EnemyStateBase
    {
        private readonly EnemyStateMachine machine;
        private float timer;
        private const float IdleDuration = 1.5f;

        public IdleState(EnemyController enemy, EnemyStateMachine machine) : base(enemy)
        {
            this.machine = machine;
        }

        public override void Enter()
        {
            timer = 0f;
            Enemy.Agent.isStopped = true;
        }

        public override void Tick()
        {
            if (Enemy.Target != null)
            {
                machine.ChangeState(Enemy.Chase);
                return;
            }

            timer += UnityEngine.Time.deltaTime;
            if (timer >= IdleDuration && Enemy.PatrolPoints != null && Enemy.PatrolPoints.Length > 0)
            {
                machine.ChangeState(Enemy.Patrol);
            }
        }

        public override void Exit()
        {
            Enemy.Agent.isStopped = false;
        }
    }
}