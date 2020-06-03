using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAI : MonoBehaviour
{
    public GameObject tower;
    public TowerAIType type;
    //public bool homing;
    //public bool splash;
    //public bool line;
    public int bulletDamage = 1;
    public float bulletSpeed = 20.0f;
    public float rotspeed = 100f;
    public SpriteRenderer spirt;
    //line attack stuff
    public GameObject startline;
    public GameObject endline;
    private LineRenderer linerend;
    private MeshCollider linecol;
    public float finalcountdown = 60f;
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
        type = tower.GetComponent<TowerAI>().type;
        if (type == TowerAIType.Splash)
        {
            Debug.LogWarning("We don't have splash functionality yet!");
        }
        else
        {
            // Sets the target location to that of the target GameObject if it exists
            if (type == TowerAIType.Line)
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
                if(finalcountdown == 0)
                {
                    Destroy(endline);
                    Destroy(gameObject);
                    finalcountdown = 60f;
                }
                if(anywaycountdown == 0)
                {
                    Destroy(gameObject);
                }
            }
            if (type == TowerAIType.Homing && targetObject)
            {
                bulletSpeed = 60.0f;
                spirt.enabled = true;
                linerend.enabled = false;
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
