using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class pausemenuthing : MonoBehaviour
{
    public GameObject gridcontainer;
    public bool activewave;
    public GameObject option1;
    public GameObject option2;
    public float time1;
    public float time2;
    // Start is called before the first frame update
    void Start()
    {
        option1.SetActive(true);
        option2.SetActive(false);
        time1 = 90;
        time2 = 0;
        gridcontainer = GameObject.Find("GridContainer");
        activewave = gridcontainer.GetComponent<MapControl>().waveActive;
        activewave = false;
    }

    // Update is called once per frame
    void Update()
    {
        //autoscroll
        if (option1.activeSelf == true)
        {
            time1 = time1 - 1;
            if (time1 <= 0)
            {
                option1.SetActive(false);
                option2.SetActive(true);
                time2 = 90;
                FindObjectOfType<AudioManager>().Play("Scroll");
            }
        }
        if (option2.activeSelf == true)
        {
            time2 = time2 - 1;
            {
                if (time2 <= 0)
                {
                    option1.SetActive(true);
                    option2.SetActive(false);
                    time1 = 90;
                    FindObjectOfType<AudioManager>().Play("Scroll");
                }
            }
        }
        if ((Input.GetKeyDown("left")) && (option2.activeSelf == true))
        {
            Debug.Log("hammunah");
            SceneManager.LoadScene("Main Menu");
            FindObjectOfType<AudioManager>().Stop("Ambience");
        }
        if ((Input.GetKeyDown("left")) && (option1.activeSelf == true))
        {
            Debug.Log("letsgoboys");
            gridcontainer.GetComponent<MapControl>().ResetSelector();
            gameObject.SetActive(false);
        }
        if (Input.GetKeyDown("right"))
        {
            gridcontainer.GetComponent<MapControl>().wavecount += 1;
            gridcontainer.GetComponent<MapControl>().waveActive = true;
            gridcontainer.GetComponent<MapControl>().enemyCap += 2;
            gameObject.SetActive(false);
            FindObjectOfType<AudioManager>().Stop("Ambience");
            FindObjectOfType<AudioManager>().Play("WaveMusic");
        }
    }
}
