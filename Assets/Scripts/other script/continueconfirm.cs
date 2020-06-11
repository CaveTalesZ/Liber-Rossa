using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class continueconfirm : MonoBehaviour
{
    public GameObject grid;
    public GameObject winscreen;
    public GameObject winlosecond;
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
            grid.GetComponent<MapControl>().wavecount += 1;
            grid.GetComponent<MapControl>().enemyCap += 2; 
            grid.GetComponent<MapControl>().spawnedBigGuy = false;
            FindObjectOfType<AudioManager>().Stop("WaveMusic");
            FindObjectOfType<AudioManager>().Play("Ambience");
            
        }
        if (Input.GetKeyDown("left"))
        {
            winscreen.GetComponent<winscreen>().waitaframe = 10;
            winscreen.GetComponent<winscreen>().enabled = true;
            winscreen.GetComponent<winscreen>().time1 = 90;
            winscreen.GetComponent<winscreen>().time2 = 0;
            gameObject.SetActive(false);
        }
    }
}
