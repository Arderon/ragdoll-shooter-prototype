using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    private EnemyVision vision;
    private RagdollEnabler ragdollEnabler;
    private bool isDead = false;

    private void Awake()
    {
        ragdollEnabler = GetComponent<RagdollEnabler>();
        vision = GetComponent<EnemyVision>();
    }


    public void TakeDamage(int damage)
    {
        if (isDead) return;
        ragdollEnabler.EnableRagdoll();
        isDead = true;
    }

    private void Update()
    {
        if(vision.CanSeePlayer())
        {
            Debug.Log("Player is in sight!");
        }
    }
}
