using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyrange : MonoBehaviour
{
    // Assigns a bullet to be shot by this turret, which determines how its attacks function
    public GameObject bulletenemy;

    // GameObject to show range the tower can reach
    public GameObject range;

    // variable to determine size of rangeCollider
    public float enemyRadius = 1.0f;

    // List of towers the turret can hit right now
    public List<GameObject> towersInRange;

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
        collideboye = GetComponentInChildren<CircleCollider2D>();
        rend = gameObject.GetComponent<SpriteRenderer>();
        range.transform.localScale = new Vector2(enemyRadius * 1f, enemyRadius * 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        towersInRange = findtowers(enemyRadius);
        if (towersInRange.Count >= 0 && cooldownTimer <= 0)
        {
            //Debug.Log("about to shoot");
            fireAt(towersInRange[0]);
            cooldownTimer = cooldown;
        }
    }

    // Function to shoot at a target GameObject
    public void fireAt(GameObject target)
    {
        GameObject attack1 = Instantiate(bulletenemy, transform.position, transform.rotation);
        enemyshoot bulletScript1 = attack1.GetComponent<enemyshoot>();
        bulletScript1.targetLocation = target.transform.position;
        bulletScript1.targetObject = target;
        bulletScript1.targettorotate = target.transform;
    }

    // Returns a list of all towers within radius, sorted by proximity, closest first
    List<GameObject> findtowers(float radius)
    {
        List<GameObject> towers = new List<GameObject>();
        foreach (GameObject tower in GameObject.FindGameObjectsWithTag("Tower"))
        {
            float distance = Vector2.Distance(tower.transform.position, gameObject.transform.position);
            // Checks if the distance to the enemy is within the specified radius
            if (distance <= radius * gameObject.transform.lossyScale.x)
            {
                Debug.Log("tower in sight: " + gameObject.name);

                // If there are no known towers within range, add this one as the closest
                if (towers.Count == 0)
                {
                    towers.Add(tower);
                }
                else
                {
                    // Loops through the list of known towers within range
                    int i = 0;
                    while (i < towers.Count)
                    {
                        // If this enemy is closer than another enemy in the radius, put it earlier in the list
                        if (distance < Vector2.Distance(towers[i].transform.position, gameObject.transform.position))
                        {
                            towers.Insert(i, tower);
                            break;
                        }
                        else
                        {
                            i++;
                            // If this enemy was not closer than any other enemy, add it as the furthest
                            if (i == towers.Count)
                            {
                                towers.Add(tower);
                                break;
                            }
                        }
                    }
                }

            }


        }
        return towers;

    }

}


