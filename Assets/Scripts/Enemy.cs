using UnityEngine;

public class Enemy : MonoBehaviour
{
    RagdollEnabler ragdollEnabler;

    private void Awake()
    {
        ragdollEnabler = GetComponent<RagdollEnabler>();
    }


    public void OnHit()
    {
        ragdollEnabler.EnableRagdoll();
    }
}
