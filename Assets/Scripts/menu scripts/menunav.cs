using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menunav : MonoBehaviour
{
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
        if (Input.GetKeyDown("left"))
        {
            doquitgame();
        }
    }
    void doquitgame()
    {
        Application.Quit();
    }
}
