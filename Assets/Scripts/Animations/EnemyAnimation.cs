using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{

    Vector3 lastPos;
    Vector3 currentPos;
    public GameObject enemy;
    public Animator animator;
    public float movementx;
    public float movementy;

    // Start is called before the first frame update
    void Start()
    {

        lastPos = enemy.transform.position;

    }

    // Update is called once per frame
    void Update()
    {

         
        currentPos = enemy.transform.position;
        
        //left
        if (currentPos.x < lastPos.x)
        {
            Vector3 movement = new Vector3(-1f, 0f, 0.0f);
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Magnitude", movement.magnitude);
            

        }

        //right
        else if (currentPos.x > lastPos.x)
        {
            Vector3 movement = new Vector3(1f, 0f, 0.0f);
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Magnitude", movement.magnitude);
                                   
            
        }

        //vertical
        //down
        else if (currentPos.y < lastPos.y)
        {
            Vector3 movement = new Vector3(0f, -1f, 0.0f);
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Magnitude", movement.magnitude);

                         

        }

        //up
        else if (currentPos.y > lastPos.y)
        {
            Vector3 movement = new Vector3(0f, 1f, 0.0f);
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Magnitude", movement.magnitude);


            

        }

        
        lastPos = currentPos;


    }
}






