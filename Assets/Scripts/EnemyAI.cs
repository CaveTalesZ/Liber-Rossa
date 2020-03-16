﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // The amount of damage the unit can take
    public int hitPoints = 1;

    // Container of all walkpoints
    public Transform walkpointContainer;

    // Array of walkpoints
    private List<Transform> walkpoints = new List<Transform>();

    // Walk speed
    public float moveSpeed = 5.0f;

    // Index of current waypoint from which Enemy walks to the next one
    private int waypointIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        // Set up path to move along
        for (int i = 0; i < walkpointContainer.childCount; i++)
        {
            walkpoints.Add(walkpointContainer.GetChild(i));
        }

        transform.position = walkpoints[waypointIndex].position;
    }

    // Update is called once per frame
    void Update()
    {
        if(hitPoints <= 0)
        {
            Destroy(gameObject);
        }
        Move();
    }

    private void Move()
    {
        // If enemy didn't reach last waypoint it can move
        // If enemy reached the last, it stops
        if (waypointIndex <= walkpoints.Count - 1)
        {
            // Move enemy from current waypoint to the next one using MoveTowards method
            transform.position = Vector3.MoveTowards(transform.position, walkpoints[waypointIndex].position, moveSpeed * Time.deltaTime);

            // if enemy reachers position of waypoint it walked towards
            // then waypointIndex is increased by 1
            // and enemy starts walking to the next waypoint
            if (transform.position == walkpoints[waypointIndex].position)
            {
                waypointIndex += 1;
            }

        }
    }
}
