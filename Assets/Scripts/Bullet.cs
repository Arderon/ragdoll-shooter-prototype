using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public GameObject hitVFX;
    private string enemyLayerName = "Enemy";
    private int enemyLayerMask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        enemyLayerMask = LayerMask.NameToLayer(enemyLayerName);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Destroy the bullet on collision
        GameObject vfx = Instantiate(hitVFX);
        vfx.transform.position = collision.contacts[0].point;
        vfx.transform.rotation = Quaternion.LookRotation(-transform.forward);
        if (collision.gameObject.layer == enemyLayerMask)
        {
            Enemy enemy = FindEnemyComponent(collision.gameObject);
            if (enemy != null)
            {
                enemy.OnHit();
            }
            StartCoroutine(WaitAndDestroy(0.1f));
        }
        else
        {
            Destroy(gameObject);
        }
        
        //Destroy(gameObject);
    }

    private Enemy FindEnemyComponent(GameObject obj)
    {
        Enemy enemy = null;
        Transform currentTransform = obj.transform;
        while (enemy == null)
        {
            Debug.Log(currentTransform.gameObject.name);
            enemy = currentTransform.GetComponent<Enemy>();
            if (enemy || currentTransform.parent == null) break;
            currentTransform = currentTransform.parent;
        }
        return enemy;
    }

    IEnumerator WaitAndDestroy(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
