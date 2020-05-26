using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splashatk : MonoBehaviour
{
    public GameObject tower;
    public bool splash = true;
    public int bulletDamage = 1;
    public float bulletSpeed = 20.0f;
    public float rotspeed = 100f;
    public SpriteRenderer spirt;
    // Position used for aimed attacks
    public Vector3 targetLocation;
    public Transform targettorotate;

    // Position used for locked attacks
    public GameObject targetObject;

    public float splashRange = 100;

    // Start is called before the first frame update
    void Start()
    {

        spirt = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (splash && targetObject)
        {
            bulletSpeed = 60.0f;
            spirt.enabled = true;
            targetLocation = targetObject.transform.position;
        }

        transform.position = Vector2.MoveTowards(transform.position, targetLocation, bulletSpeed * Time.deltaTime);

        Vector2 direction = targetLocation - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotspeed * Time.deltaTime);

        if ((Vector2)transform.position == (Vector2)targetLocation)
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



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {

            Collider2D[] splashCollider = new Collider2D[10];
            ContactFilter2D splashFilter = new ContactFilter2D();
            splashFilter.useTriggers = true;
            int withinSplash = Physics2D.OverlapCircle(gameObject.transform.position, 100f, splashFilter , splashCollider);
            Debug.Log("num"+withinSplash);
            if (withinSplash != 0)
            {
                Debug.Log("Trying to hit within splash");
                foreach (Collider2D enemyHit in splashCollider)
                {
                        Debug.Log("Final hit try");
                        enemyHit.gameObject.GetComponent<EnemyAI>().hitPoints -= bulletDamage;
                        Destroy(gameObject);
                }
            }
        }
    }

}


