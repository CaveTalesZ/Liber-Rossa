using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAI : MonoBehaviour
{
    public bool homing;
    public bool splash;
    public int bulletDamage = 1;
    public float bulletSpeed = 10.0f;

    // Position used for aimed attacks
    public Vector3 targetLocation;

    // Position used for locked attacks
    public GameObject targetObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (splash)
        {
            Debug.LogWarning("We don't have splash functionality yet!");
        }
        else
        {
            // Sets the target location to that of the target GameObject if it exists
            if (homing && targetObject)
            {
                targetLocation = targetObject.transform.position;
            }
            // Moves towards a the target location
            transform.position = Vector3.MoveTowards(transform.position, targetLocation, bulletSpeed * Time.deltaTime);

            if(transform.position == targetLocation)
            {
                Destroy(gameObject);
            }

            // Checks for collisions
            Collider[] collisions = Physics.OverlapBox(transform.position, transform.localScale / 2);
            // Checks if the collision is an enemy
            foreach(Collider collision in collisions)
            {
                if (collision.gameObject.CompareTag("Enemy"))
                {
                    Debug.Log("Pew!");
                    collision.gameObject.GetComponent<EnemyAI>().hitPoints -= bulletDamage;
                    Destroy(gameObject);
                }
            }
        }

        
    }
}
