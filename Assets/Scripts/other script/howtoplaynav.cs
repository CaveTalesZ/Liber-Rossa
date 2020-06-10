using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class howtoplaynav : MonoBehaviour
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
            FindObjectOfType<AudioManager>().Play("Select");
            FindObjectOfType<AudioManager>().Stop("MainTheme");
            FindObjectOfType<AudioManager>().Play("Ambience");
        }
        if (Input.GetKeyDown("left"))
        {
            SceneManager.LoadScene("Main Menu");
            FindObjectOfType<AudioManager>().Play("Cancel");
        }
    }
}
