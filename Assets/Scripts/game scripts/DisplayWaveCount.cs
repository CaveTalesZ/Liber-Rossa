using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayWaveCount : MonoBehaviour
{
    public GameObject grid;
    Text wavecount;
    private float wave;

    // Start is called before the first frame update
    void Start()
    {
        wavecount = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        wave = grid.GetComponent<MapControl>().wavecount;
        wavecount.text = "Wave number:" + wave;
    }
}
