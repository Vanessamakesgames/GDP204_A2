using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    // CHANGE CODE TO USE A STATE MACHINE + RUN BEHAVIOURS THROUGH STATE MACHINE


    public GameObject[] checkpoints;
    Transform currentTarget;
    public float threshold;
    public float detectionRange;
    public GameObject player; //Change to initialise in Start() + make private

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    // Change into a state machine model
    void Patrol()
    {
        //keep moving to target + check for player + raycast to see if AI can see player
        float distToPlayer = Vector3.Distance(player.transform.position, this.transform.position);
        if (distToPlayer <= detectionRange) //if player in range of AI
        {
            //raycast to see if AI can see player or if obstructed by scenery
            if (/*raycast to see if AI can see player*/)
            {

            }
            else // AI cannot see player. Keep patrolling
            {
                float distToObj = Vector3.Distance(currentTarget.position, this.transform.position);
                if (distToObj >= threshold)
                {

                }
                else
                {
                    //Change target checkpoint
                }
            }
        }
    }
}
