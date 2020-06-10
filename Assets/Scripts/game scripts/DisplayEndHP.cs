using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayEndHP : MonoBehaviour
{
    public GameObject end;
    Text hpp;
    private float hp;

    // Start is called before the first frame update
    void Start()
    {
        hpp = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        end = GameObject.Find("End(Clone)");
        hp = end.GetComponent<losegame>().health;
        hpp.text = "Health Left: " +  hp;
    }
}
