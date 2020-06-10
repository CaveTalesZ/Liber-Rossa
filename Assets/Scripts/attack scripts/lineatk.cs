using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineatk : MonoBehaviour
{
    public GameObject tower;
    public bool line = true;
    public int bulletDamage = 3;
    public float bulletSpeed = 20.0f;
    public float rotspeed = 100f;
    public SpriteRenderer spirt;
    //line attack stuff
    public GameObject startline;
    public GameObject endline;
    private LineRenderer linerend;
    private MeshCollider linecol;
    public float finalcountdown = 30f;
    public float anywaycountdown = 60f;




    // Position used for aimed attacks
    public Vector3 targetLocation;
    public Transform targettorotate;

    // Position used for locked attacks
    public GameObject targetObject;

    // Start is called before the first frame update
    void Start()
    {
        spirt = GetComponent<SpriteRenderer>();
        linerend = GetComponent<LineRenderer>();
        linecol = GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Sets the target location to that of the target GameObject if it exists
        if (line)
        {
            finalcountdown = finalcountdown - 1;
            anywaycountdown = anywaycountdown - 1;
            bulletSpeed = 0;
            spirt.enabled = false;
            RaycastHit2D ray = Physics2D.Raycast(startline.transform.position, transform.right);
            Debug.DrawLine(transform.position, ray.point);
            transform.position = ray.point;
            //Start and end positions for the line renderer
            linerend.SetPosition(0, startline.transform.position);
            linerend.SetPosition(1, endline.transform.position);
            if (finalcountdown <= 0)
            {
                Destroy(gameObject);
                endline.gameObject.GetComponent<EnemyAI>().hitPoints -= bulletDamage;
                finalcountdown = 30f;
            }
            if (anywaycountdown <= 0)
            {
                Destroy(gameObject);
            }
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


