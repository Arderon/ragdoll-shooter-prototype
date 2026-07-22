using UnityEngine;
using UnityEngine.AI;

namespace EnemyAI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform[] patrolPoints;
        [SerializeField] private LayerMask playerLayer;

        [Header("Movement")]
        [SerializeField] private float patrolSpeed = 2f;
        [SerializeField] private float chaseSpeed = 4f;
        [SerializeField] private float patrolWaitTime = 2f;

        public NavMeshAgent Agent { get; private set; }
        public Transform[] PatrolPoints => patrolPoints;
        public float PatrolSpeed => patrolSpeed;
        public float ChaseSpeed => chaseSpeed;
        public float PatrolWaitTime => patrolWaitTime;

        private bool isDead = false;

        private EnemyStateMachine stateMachine;
        private EnemyVision vision;
        private RagdollEnabler ragdollEnabler;

        // Готові інстанси станів (щоб не створювати нові об'єкти щокадру)
        public IdleState Idle { get; private set; }
        public PatrolState Patrol { get; private set; }
        public ChaseState Chase { get; private set; }
        public AttackState Attack { get; private set; }
        public DeadState Dead { get; private set; }
        public Transform Target { get; private set; }

        private void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
            ragdollEnabler = GetComponent<RagdollEnabler>();
            vision = GetComponent<EnemyVision>();

            stateMachine = new EnemyStateMachine();

            Idle = new IdleState(this, stateMachine);
            Patrol = new PatrolState(this, stateMachine);
            Chase = new ChaseState(this, stateMachine);
            Attack = new AttackState(this, stateMachine);
            Dead = new DeadState(this, stateMachine);
        }

        private void Start()
        {
            vision.SetPlayerLayer(playerLayer);
            stateMachine.ChangeState(Idle);
        }

        private void Update()
        {
            stateMachine.Tick();
        }

        private void FixedUpdate()
        {
            stateMachine.FixedTick();
        }

        private void TryFindTarget()
        {
            if (Target != null || isDead) return;

            
            Target = vision.GetPlayerInSight();
        }

        public void TakeDamage(int damage)
        {
            if (isDead) return;
            stateMachine.ChangeState(Dead);
            //ragdollEnabler.EnableRagdoll();
            //isDead = true;
        }
    }
}