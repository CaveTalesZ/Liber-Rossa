using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAI : MonoBehaviour
{

    // Assigns a bullet to be shot by this turret, which determines how its attacks function
    public GameObject bullet;

    // GameObject to detect enemies within the space of a cylindrical collider;
    private GameObject rangeCollider;

    // variable to determine size of rangeCollider
    public float towerRadius = 5.0f;

    // The collider of the rangeCollider GameObject
    private CapsuleCollider effectiveArea;

    // List of enemies the turret can hit right now
    private List<GameObject> enemiesInRange;

    // This determines how long it takes before the turret can fire again
    public float cooldown = 60.0f;

    // This is used
    public float cooldownTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rangeCollider = findCollider();
        rangeCollider.transform.localScale = new Vector3(towerRadius * 0.4f, towerRadius * 0.4f, towerRadius * 0.4f);
        effectiveArea = rangeCollider.GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        enemiesInRange = findEnemies();
        if (enemiesInRange.Count > 0 && cooldownTimer <= 0)
        {
            fireAt(enemiesInRange[0]);
            cooldownTimer = cooldown;
        }
        
    }

    // Function to shoot aimed attack at a target
    public void fireAt(Vector3 target)
    {

    }

    // Function to shoot locked attack at a target
    public void fireAt(GameObject target)
    {
        GameObject attack = Instantiate(bullet);
        bullet.transform.position = transform.position;
        BulletAI bulletScript = attack.GetComponent<BulletAI>();
        bulletScript.targetObject = target;
    }

    List<GameObject> findEnemies()
    {
        List<GameObject> enemies = new List<GameObject>();
        Collider[] collisions = Physics.OverlapCapsule(effectiveArea.center + new Vector3(0, effectiveArea.height / 2, 0) + rangeCollider.transform.position,
                                                       effectiveArea.center - new Vector3(0, effectiveArea.height / 2, 0) + rangeCollider.transform.position,
                                                       towerRadius);
        foreach(Collider collision in collisions)
        {
            if(collision.gameObject.CompareTag("Enemy"))
            {
                enemies.Add(collision.gameObject);
            }
        }
        return enemies;

    }

    GameObject findCollider()
    {
        for(var i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            if (child.name == "Collider")
            {
                return child;
            }
        }
        Debug.LogError("Found no GameObject named \"Collider\"");
        return null;
    }
}
