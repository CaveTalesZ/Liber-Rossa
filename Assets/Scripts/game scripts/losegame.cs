using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class losegame : MonoBehaviour
{
    public GameObject losescreen;
    public GameObject enemycount;
    public GameObject grid;
    public float health = 10f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(health == 0)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            health = health - 1;
        }
    }
}
