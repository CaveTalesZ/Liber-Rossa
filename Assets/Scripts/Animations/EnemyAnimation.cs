using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    Vector3 lastPos;
    Vector3 currentPos;
    public GameObject enemy;
    public Animator animator;

    public Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        lastPos = enemy.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentPos = enemy.transform.position;

        Vector3 diff = currentPos - lastPos;

        //left
        if (diff.x < 0) movement = new Vector3(-1f, 0f, 0.0f);

        //right
        else if (diff.x > 0)movement = new Vector3(1f, 0f, 0.0f);

        //vertical
        //down
        else if (diff.y < 0) movement = new Vector3(0f, -1f, 0.0f);

        //up
        else if (diff.y > 0) movement = new Vector3(0f, 1f, 0.0f);
        
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);

        lastPos = currentPos;
    }
}






