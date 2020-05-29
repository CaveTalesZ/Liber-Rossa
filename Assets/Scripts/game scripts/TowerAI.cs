using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAI : MonoBehaviour
{

    // Assigns a bullet to be shot by this turret, which determines how its attacks function
    public GameObject bulletsplash;
    public GameObject bulletline;
    public GameObject bullethoming;

    //stuff
    public bool splash;
    public bool line;
    public bool homing;
    //sprites
    public Sprite spiral;
    public Sprite fire;
    public Sprite ice;

    // GameObject to show range the tower can reach
    public GameObject range;

    // variable to determine size of rangeCollider
    public float towerRadius = 5.0f;

    // List of enemies the turret can hit right now
    private List<GameObject> enemiesInRange;

    public GameObject closestEnemy;

    // This determines how long it takes before the turret can fire again
    public float cooldown = 60.0f;

    // This is used
    public float cooldownTimer = 0.0f;

    private SpriteRenderer rend;

  
    

    // Start is called before the first frame update
    void Start()
    {

        rend = gameObject.GetComponent<SpriteRenderer>();
        range.transform.localScale = new Vector2(towerRadius * 2f, towerRadius * 2f);
        FindObjectOfType<AudioManager>().Play("Construction");
    }

    // Update is called once per frame
    void Update()
    {
		if (gameObject.activeInHierarchy == false)
		{
            splash = false;
            line = false;
            homing = false;
		}
		if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        enemiesInRange = findEnemies(towerRadius);
        if (enemiesInRange.Count > 0 && cooldownTimer <= 0)
        {
            fireAt(enemiesInRange[0]);
            cooldownTimer = cooldown;
        }
        if(splash == true)
        {
            rend.sprite = fire;
        }
        if(line == true)
        {
            rend.sprite = ice;
        }
        if(homing == true)
        {
            rend.sprite = spiral;
        }
    }

    // Function to shoot at a target GameObject
    public void fireAt(GameObject target)
    {
        FindObjectOfType<AudioManager>().Play("Fire");
        if (homing == true)
        {
            GameObject attack1 = Instantiate(bullethoming, transform.position, transform.rotation);
            homingatk bulletScript1 = attack1.GetComponent<homingatk>();
            bulletScript1.targetLocation = target.transform.position;
            bulletScript1.targetObject = target;
            bulletScript1.targettorotate = target.transform;

		}
        if(splash == true)
        {
			GameObject attack2 = Instantiate(bulletsplash, transform.position, transform.rotation);
		    splashatk bulletScript2 = attack2.GetComponent<splashatk>();
			bulletScript2.targetLocation = target.transform.position;
			bulletScript2.targetObject = target;
			bulletScript2.targettorotate = target.transform;

		}
        if(line == true)
        {
			GameObject attack3 = Instantiate(bulletline, transform.position, transform.rotation);
			lineatk bulletScript3 = attack3.GetComponent<lineatk>();
			bulletScript3.targetLocation = target.transform.position;
			bulletScript3.targetObject = target;
			bulletScript3.targettorotate = target.transform;
			bulletScript3.startline = gameObject;
			bulletScript3.endline = target;
		}

     }

		// Returns a list of all enemies within radius, sorted by proximity, closest first
		List<GameObject> findEnemies(float radius)
    {
        List<GameObject> enemies = new List<GameObject>();
        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            float distance = Vector2.Distance(enemy.transform.position, gameObject.transform.position);
            // Checks if the distance to the enemy is within the specified radius
            if (distance <= radius * gameObject.transform.lossyScale.x)
            {
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
                            if(i == enemies.Count)
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

    
}