using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAI : MonoBehaviour
{
    public bool homing;
    public bool splash;
    public int bulletDamage = 1;
    public float bulletSpeed = 20.0f;
    public float rotspeed = 100f;

    // Position used for aimed attacks
    public Vector3 targetLocation;
    public Transform targettorotate;

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
            transform.position = Vector2.MoveTowards(transform.position, targetLocation, bulletSpeed * Time.deltaTime);
            //rotates towards target
            Vector2 direction = targetLocation - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotspeed * Time.deltaTime);


            if ((Vector2) transform.position == (Vector2) targetLocation)
            {
                Destroy(gameObject);
            }


        }
        // Rotate the bullet towards the target
        //gameObject.transform.eulerAngles = new Vector3(
        //    transform.rotation.x, 
        //    transform.rotation.y,
        //    Vector2.SignedAngle(0, targetLocation - transform.position)
        //);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {

            other.gameObject.GetComponent<EnemyAI>().hitPoints -= bulletDamage;
            Destroy(gameObject);
        }
    }
}
