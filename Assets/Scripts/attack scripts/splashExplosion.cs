using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splashExplosion : MonoBehaviour
{
    public int bulletDamage = 1;
    public float explosionTime = 1;
   
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("explosion entered");
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyAI>().hitPoints -= bulletDamage;
            StartCoroutine("Explode");
        }
    }

    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(explosionTime);
        Destroy(gameObject);
    }
}
