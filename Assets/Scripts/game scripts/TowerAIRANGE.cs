using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerAIRANGE : MonoBehaviour
{
    public GameObject tower;
    public GameObject bulletsplash;
    public GameObject bulletline;
    public GameObject bullethoming;
    // GameObject to show range the tower can reach
    public GameObject range;

    // variable to determine size of rangeCollider
    public float towerRadius = 1.0f;

    // List of enemies the turret can hit right now
    public List<GameObject> enemiesInRange = new List<GameObject>();

    // This determines how long it takes before the turret can fire again
    public float cooldown = 60.0f;

    // This is used
    // it is indeed, whoa
    public float cooldownTimer = 0.0f;
    //sprite renderer for the tower
    private SpriteRenderer rend;
    //range's circle collider
    public Collider2D collideboye;
    // Start is called before the first frame update
    void Start()
    {
        range.transform.localScale = new Vector2(towerRadius * 2f, towerRadius * 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        enemiesInRange = enemiesInRange.Where(enemy => enemy != null).ToList();

        if (enemiesInRange.Count > 0 && cooldownTimer <= 0)
        {
            //Debug.Log("about to shoot");
            fireAt(enemiesInRange[0]);
            cooldownTimer = cooldown;
        }
    }
    public void fireAt(GameObject target)
    {
        FindObjectOfType<AudioManager>().Play("Fire");


        if (tower.GetComponent<TowerAI>().type == TowerAIType.Homing)
        {
            GameObject attack1 = Instantiate(bullethoming, transform.position, transform.rotation);
            homingatk bulletScript1 = attack1.GetComponent<homingatk>();
            bulletScript1.targetLocation = target.transform.position;
            bulletScript1.targetObject = target;
            bulletScript1.targettorotate = target.transform;
            cooldown = 1f;
        }
        if (tower.GetComponent<TowerAI>().type == TowerAIType.Splash)
        {
            GameObject attack2 = Instantiate(bulletsplash, transform.position, transform.rotation);
            splashatk bulletScript2 = attack2.GetComponent<splashatk>();
            bulletScript2.targetLocation = target.transform.position;
            bulletScript2.targetObject = target;
            bulletScript2.targettorotate = target.transform;
            cooldown = 1.5f;
        }
        if (tower.GetComponent<TowerAI>().type == TowerAIType.Line)
        {

            GameObject attack3 = Instantiate(bulletline, transform.position, transform.rotation);
            lineatk bulletScript3 = attack3.GetComponent<lineatk>();
            bulletScript3.targetLocation = target.transform.position;
            bulletScript3.targetObject = target;
            bulletScript3.targettorotate = target.transform;
            bulletScript3.startline = gameObject;
            bulletScript3.endline = target;
            cooldown = 0.8f;
        }  
        
    }
    List<GameObject> findEnemies(float radius)
    {
        List<GameObject> enemies = new List<GameObject>();
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            float distance = Vector2.Distance(enemy.transform.position, gameObject.transform.position);
            // Checks if the distance to the enemy is within the specified radius
            if (distance <= radius * gameObject.transform.lossyScale.x)
            {
                Debug.Log("enemy in sight: " + gameObject.name);

                // If there are no known enemies within range, add this one as the closest
                if (enemies.Count == 0)
                {
                    enemies.Add(enemy);
                }
                else
                {
                    // Loops through the list of known enemies within range
                    int i = 0;
                    while (i < enemies.Count)
                    {
                        // If this enemy is closer than another enemy in the radius, put it earlier in the list
                        if (distance < Vector2.Distance(enemies[i].transform.position, gameObject.transform.position))
                        {
                            enemies.Insert(i, enemy);
                            break;
                        }
                        else
                        {
                            i++;
                            // If this enemy was not closer than any other enemy, add it as the furthest
                            if (i == enemies.Count)
                            {
                                enemies.Add(enemy);
                                break;
                            }
                        }
                    }
                }

            }


        }
        return enemies;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemiesInRange.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(collision.gameObject);
        }

    }
}
