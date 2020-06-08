using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winscreen : MonoBehaviour
{
    public GameObject audioManager;
  
    public GameObject row1;
    public GameObject row2;
    public GameObject continueconfirm;
    public GameObject restartconfirm;
    public GameObject grid;
    public GameObject winlosecond;
    public int time1;
    public int time2;
    public int wavestate;
    // Start is called before the first frame update
    void Start()
    {
        row1.SetActive(true);
        row2.SetActive(false);
        continueconfirm.SetActive(false);
        restartconfirm.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf == false)
        {
            wavestate = 1;
        }
        else
        {
            wavestate = 0;
        }
        if ((Input.GetKeyDown("left")) && (restartconfirm.GetComponent<restartconfirm>().wait == 0) && (continueconfirm.GetComponent<continueconfirm>().wait == 0))
        {
            SceneManager.LoadScene("Main Menu");
        }
        //autoscroll;
        if (row1.activeSelf == true)
        {
            time1 = time1 - 1;
            if (time1 <= 0)
            {
                row1.SetActive(false);
                row2.SetActive(true);
                time2 = 90;
                FindObjectOfType<AudioManager>().Play("Scroll");
            }
        }
        if (row2.activeSelf == true)
        {
            time2 = time2 - 1;
            {
                if (time2 <= 0)
                {
                    row1.SetActive(true);
                    row2.SetActive(false);
                    time1 = 90;
                    FindObjectOfType<AudioManager>().Play("Scroll");
                }
            }
        }
        //scene reload and new scene load
        if ((Input.GetKeyDown("right")) && (row1.activeSelf == true))
        {
            

            //gameObject.SetActive(false);
            continueconfirm.SetActive(true);
            continueconfirm.GetComponent<continueconfirm>().wait = 10;
            gameObject.GetComponent<winscreen>().enabled = false;

        }
        if ((Input.GetKeyDown("right")) && (row2.activeSelf == true))
        {
            restartconfirm.SetActive(true);
            restartconfirm.GetComponent<restartconfirm>().wait = 10;
            gameObject.GetComponent<winscreen>().enabled = false;
            
        }
    }
}
