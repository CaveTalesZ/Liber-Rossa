using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restartconfirm : MonoBehaviour
{
    public GameObject winscreen;
    public GameObject losescreen;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("right"))
        {
            SceneManager.LoadScene("Game");
        }
        if(Input.GetKeyDown("left"))
        {
            winscreen.GetComponent<winscreen>().waitaframe = 10;
            winscreen.GetComponent<winscreen>().enabled = true;
            winscreen.GetComponent<winscreen>().time1 = 90;
            winscreen.GetComponent<winscreen>().time2 = 0;
            losescreen.GetComponent<losescreen>().waitaframe = 10;
            losescreen.GetComponent<losescreen>().enabled = true;
            losescreen.GetComponent<losescreen>().time1 = 90;
            losescreen.GetComponent<losescreen>().time2 = 0;
            gameObject.SetActive(false);
        }
    }
}
