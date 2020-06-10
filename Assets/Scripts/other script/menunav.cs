using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menunav : MonoBehaviour
{
    public GameObject audioManager;
    public GameObject playgamebtn;
    public GameObject howtoplaybtn;
    public GameObject selector;
    public GameObject row1;
    public GameObject row2;
    public int time1 = 90;
    public int time2 = 0;
    //public int selectedzone = 0;
    //public Vector2 selectedSpace = new Vector2(-1, -1);
    //private int selectedbtn = -1;
    //private int currentrow = 0;
    //private int currentcolumn = 0;
    //private int gridSize;

    // Start is called before the first frame update
    void Start()
    {
        row1.SetActive(true);
        row2.SetActive(false);
        FindObjectOfType<AudioManager>().Play("MainTheme");
        FindObjectOfType<AudioManager>().Stop("Ambience");
        FindObjectOfType<AudioManager>().Stop("WaveMusic");
    }

    // Update is called once per frame
    void Update()
    {
        //Quitting the game with this button
        if (Input.GetKeyDown("left"))
        {
            doquitgame();
        }
        //Attempting to recreate the autoscroll thing
        //end me please
        if (row1.activeSelf == true)
        {
            time1 = time1 - 1;
            if (time1 == 0)
            {
                row1.SetActive(false);
                row2.SetActive(true);
                time2 = 90;
                FindObjectOfType<AudioManager>().Play("Scroll");
            }
        }
        if(row2.activeSelf == true)
        {
            time2 = time2 - 1;
            {
                if(time2 == 0)
                {
                    row1.SetActive(true);
                    row2.SetActive(false);
                    time1 = 90;
                    FindObjectOfType<AudioManager>().Play("Scroll");
                }
            }
        }
        //loading respective scene on respective button
        if((Input.GetKeyDown("right")) && (row1.activeSelf == true))
        {
            SceneManager.LoadScene("Game");
            FindObjectOfType<AudioManager>().Stop("MainTheme");
            FindObjectOfType<AudioManager>().Play("Select");
            FindObjectOfType<AudioManager>().Play("Ambience");
        }
        if((Input.GetKeyDown("right")) && (row2.activeSelf == true))
        {
            SceneManager.LoadScene("howtoplay");
            FindObjectOfType<AudioManager>().Play("Select");
        }
    }

    void doquitgame()
    {
        Application.Quit();
    }
}
