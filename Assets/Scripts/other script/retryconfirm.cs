using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class retryconfirm : MonoBehaviour
{
    public int wait = 10;
    public GameObject grid;
    public GameObject losescreen;
    public GameObject winlosecond;
    public GameObject endthing;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("right"))
        {
            grid.GetComponent<MapControl>().enabled = true;
            grid.GetComponent<MapControl>().ResetSelector();
            winlosecond.GetComponent<winlosecond>().timerr = 200;
            grid.GetComponent<MapControl>().enemyCount = 0;
            grid.GetComponent<MapControl>().spawnedBigGuy = false;
            Instantiate(endthing);
            gameObject.SetActive(false);
        }
        if (Input.GetKeyDown("left"))
        {
            wait = wait - 1;
            losescreen.GetComponent<losescreen>().enabled = true;
            losescreen.GetComponent<losescreen>().time1 = 90;
            losescreen.GetComponent<losescreen>().time2 = 0;
            gameObject.SetActive(false);
        }
    }
}
