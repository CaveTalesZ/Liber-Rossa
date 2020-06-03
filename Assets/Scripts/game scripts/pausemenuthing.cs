using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class pausemenuthing : MonoBehaviour
{
    public GameObject gridcontainer;
    public bool activewave;
    public GameObject enemyholder;
    // Start is called before the first frame update
    void Start()
    {
        gridcontainer = GameObject.Find("GridContainer");
        activewave = gridcontainer.GetComponent<MapControl>().waveActive;
        activewave = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("left"))
        {
            SceneManager.LoadScene("Main Menu");
            FindObjectOfType<AudioManager>().Stop("BuildMusic");
        }
        if (Input.GetKeyDown("right"))
        {
            gridcontainer.GetComponent<MapControl>().waveActive = true;
            Instantiate(enemyholder);
            Destroy(gameObject);
        }
    }
}
