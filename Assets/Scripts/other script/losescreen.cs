using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class losescreen : MonoBehaviour
{
    public GameObject audioManager;
    public GameObject row1;
    public GameObject row2;
    public GameObject restartscreen;
    public GameObject retryscreen;
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
        retryscreen.SetActive(false);
        restartscreen.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown("left")) && (restartscreen.GetComponent<restartconfirm>().wait == 0))
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
        if ((Input.GetKeyDown("right")) && (row1.activeSelf == true))
        {
            //gameObject.SetActive(false);
            retryscreen.SetActive(true);
            //continueconfirm.GetComponent<continueconfirm>().wait = 10;
            gameObject.GetComponent<losescreen>().enabled = false;

        }
        if ((Input.GetKeyDown("right")) && (row2.activeSelf == true))
        {
            restartscreen.SetActive(true);
            //restartconfirm.GetComponent<restartconfirm>().wait = 10;
            gameObject.GetComponent<losescreen>().enabled = false;

        }

    }
}