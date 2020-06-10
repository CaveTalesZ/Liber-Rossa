using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splashExplosion : MonoBehaviour
{
    public int bulletDamage = 1;
    public GameObject[] linebullets;
    public float explosionTime = 0.25f;
   
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
        DestroyLine();
        Destroy(gameObject);
    }
    public void DestroyLine()
    {
        linebullets = GameObject.FindGameObjectsWithTag("linebullet");
        for (int i = 0; i < linebullets.Length; i++)
        {
            Destroy(linebullets[i]);
        }
    }
}
