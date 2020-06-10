using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathFollower : MonoBehaviour
{

    Node[] pathNode;
    public GameObject Player;
    public float MoveSpeed;
    float Timer;
    static Vector3 CurrentPositionHolder;
    int CurrentNode;
    private Vector2 startPosition;


    // Use this for initialization
    void Start()
    {
        pathNode = GetComponentsInChildren<Node>();
        CheckNode();
    }

    void CheckNode()
    {
        Timer = 0;
        startPosition = Player.transform.position;
        CurrentPositionHolder = pathNode[CurrentNode].transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        Timer += Time.deltaTime * MoveSpeed;

        if (Player.transform.position != CurrentPositionHolder)
        {

            Player.transform.position = Vector3.Lerp(startPosition, CurrentPositionHolder, Timer);
        }
        else
        {

            CurrentNode++;
            CheckNode();

        }
    }
}