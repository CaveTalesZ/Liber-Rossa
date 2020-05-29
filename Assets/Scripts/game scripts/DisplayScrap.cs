using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScrap : MonoBehaviour
{
    public GameObject grid;
    Text scrap;
    private float scrp;

    // Start is called before the first frame update
    void Start()
    {
        scrap = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
       scrp = grid.GetComponent<MapControl>().scrap;
       scrap.text = "SCRAP:" + scrp;
    }
}
