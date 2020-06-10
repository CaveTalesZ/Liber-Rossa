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
        Debug.Log("number of towers in range: " + towersInRange.Count);

        if (towersInRange.Count > 0 && cooldownTimer <= 0)
        {
            // look for the closes tower
            GameObject closestTower = towersInRange[0];
            float closestDistance = Vector3.Distance(closestTower.transform.position, gameObject.transform.position);
            foreach (var tower in towersInRange)
            {
                float new_distance = Vector3.Distance(tower.transform.position, gameObject.transform.position);
                if (new_distance < closestDistance)
                {
                    closestDistance = new_distance;
                    closestTower = tower;
                }
            }

            //Debug.Log("about to shoot");
            fireAt(closestTower);
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Tower"))
        {
            towersInRange.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Tower"))
        {
            towersInRange.Remove(other.gameObject);
        }
    }

}


