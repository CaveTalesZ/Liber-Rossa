using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAI : MonoBehaviour
{

    // 
    public GameObject bullet;

    // GameObject to detect enemies within the space of a cylindrical collider;
    private GameObject rangeCollider;

    // variable to determine size of rangeCollider
    public float towerRadius = 5.0f;

    // The collider of the rangeCollider GameObject
    private CapsuleCollider effectiveArea;

    // List of enemies the turret can hit right now
    private List<GameObject> enemiesInRange;

    // Start is called before the first frame update
    void Start()
    {
        rangeCollider = findCollider();
        rangeCollider.transform.localScale = new Vector3(towerRadius * 2, towerRadius * 2, towerRadius * 2);
        effectiveArea = rangeCollider.GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        enemiesInRange = findEnemies();
        if(enemiesInRange.Count > 0)
        {
            fireAt(enemiesInRange[0]);
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
