using UnityEngine;

namespace EnemyAI
{
    public class PatrolState : EnemyStateBase
    {
        private readonly EnemyStateMachine machine;
        private int currentPointIndex;
        private float waitTimer;
        private bool isWaiting;

        public PatrolState(EnemyController enemy, EnemyStateMachine machine) : base(enemy)
        {
            this.machine = machine;
        }

        public override void Enter()
        {
            Enemy.Agent.speed = Enemy.PatrolSpeed;
            Enemy.Agent.isStopped = false;
            isWaiting = false;
            waitTimer = 0f;

            currentPointIndex = FindClosestPointIndex();
            SetDestinationToCurrentPoint();
        }

        public override void Tick()
        {
            if (Enemy.Target != null)
            {
                machine.ChangeState(Enemy.Chase);
                return;
            }

            if (isWaiting)
            {
                waitTimer += Time.deltaTime;
                if (waitTimer >= Enemy.PatrolWaitTime)
                {
                    isWaiting = false;
                    currentPointIndex = (currentPointIndex + 1) % Enemy.PatrolPoints.Length;
                    SetDestinationToCurrentPoint();
                }
                return;
            }

            if (!Enemy.Agent.pathPending && Enemy.Agent.remainingDistance <= Enemy.Agent.stoppingDistance)
            {
                isWaiting = true;
                waitTimer = 0f;
            }
        }

        private void SetDestinationToCurrentPoint()
        {
            var point = Enemy.PatrolPoints[currentPointIndex];
            Enemy.Agent.SetDestination(point.position);
        }

        private int FindClosestPointIndex()
        {
            int closest = 0;
            float minDist = float.MaxValue;
            for (int i = 0; i < Enemy.PatrolPoints.Length; i++)
            {
                float dist = Vector3.Distance(Enemy.transform.position, Enemy.PatrolPoints[i].position);
                if (dist < minDist)
                {
                    minDist = dist;
                    closest = i;
                }
            }
            return closest;
        }
    }
}