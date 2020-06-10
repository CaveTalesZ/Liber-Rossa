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
        
        animator.SetFloat("Horizontal", diff.x);
        animator.SetFloat("Vertical", diff.y);
        animator.SetFloat("Magnitude", diff.magnitude);

        lastPos = currentPos;
    }
}






