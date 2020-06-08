using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class range : MonoBehaviour
{
    public GameObject tower;
    public List<GameObject> enemiesInRange;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        enemiesInRange = tower.GetComponent<TowerAI>().enemiesInRange;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            tower.GetComponent<TowerAI>().fireAt(enemiesInRange[0]);
            tower.GetComponent<TowerAI>().cooldownTimer = tower.GetComponent<TowerAI>().cooldown;
        }
    }
}
