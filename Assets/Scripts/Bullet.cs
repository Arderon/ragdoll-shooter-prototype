using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitVFX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Destroy the bullet on collision
        GameObject vfx = Instantiate(hitVFX);
        vfx.transform.position = collision.contacts[0].point;
        vfx.transform.rotation = Quaternion.LookRotation(-transform.forward);
        Destroy(gameObject);
    }
}
