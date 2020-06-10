using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class winlosecond : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] getcount;
    public float timerr = 2.0f;
    public GameObject losemenu;
    public GameObject endobject;
    public GameObject winmenu;
    public int counter;
    public bool waveended;

    // Start is called before the first frame update
    void Start()
    {
        //endobject = GameObject.Find("End");
        waveended = false;
    }

    // Update is called once per frame
    void Update()
    {
        endobject = GameObject.Find("End(Clone)");
        timerr = timerr - 1;
       getcount = GameObject.FindGameObjectsWithTag("Enemy");
       counter = getcount.Length; 
       if(counter == 0 && timerr <= 0 && endobject.GetComponent<losegame>().health > 0)
        {
            timerr = 0;
            winmenu.SetActive(true);
            winmenu.GetComponent<winscreen>().enabled = true;
            
        }
       if(endobject.GetComponent<losegame>().health <= 0)
        {
            DestroyEnemies();
            losemenu.SetActive(true);
            Destroy(endobject);
        }
    }
     public void DestroyEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }
    }

}
