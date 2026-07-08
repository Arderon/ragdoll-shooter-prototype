using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    RagdollEnabler ragdollEnabler;
    bool isDead = false;

    private void Awake()
    {
        ragdollEnabler = GetComponent<RagdollEnabler>();
    }


    public void TakeDamage(int damage)
    {
        if (isDead) return;
        ragdollEnabler.EnableRagdoll();
        isDead = true;
    }
}
