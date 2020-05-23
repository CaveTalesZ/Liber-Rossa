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

    // Start is called before the first frame update
    void Start()
    {
        spirt = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (splash)
        {
            Debug.LogWarning("We don't have splash functionality yet!");
        }
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

            other.gameObject.GetComponent<EnemyAI>().hitPoints -= bulletDamage;
            Destroy(gameObject);
        }
    }

}


