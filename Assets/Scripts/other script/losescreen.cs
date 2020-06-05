using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class losescreen : MonoBehaviour
{
    public GameObject audioManager;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("left"))
        {
            SceneManager.LoadScene("Main Menu");
        }
    
        if (Input.GetKeyDown("right"))
        {
            SceneManager.LoadScene("Game");     
        }
 
    }
}