using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winlosecond : MonoBehaviour
{
    public GameObject[] getcount;
    public float timerr = 2.0f;
    public GameObject winmenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timerr = timerr - 1;
        getcount = GameObject.FindGameObjectsWithTag("Enemy");
        int counter = getcount.Length; 
        if(counter == 0 && timerr <= 0)
        {
            timerr = 0;
            Debug.LogError("wave complete");
            Instantiate(winmenu);
            Destroy(gameObject);

        }
    }
}
