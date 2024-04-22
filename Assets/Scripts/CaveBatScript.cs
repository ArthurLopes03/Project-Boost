using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CaveBatScript : MonoBehaviour
{
    public Transform[] wayPoints;

    private Transform nextTarget;

    private bool isAudible = false;

    private bool idling = false;

    public float distanceToMoveOn = 3f;

    public float proximityDetection = 13f;

    private int wayPointIndex = 0;

    [SerializeField]
    private Transform playerPos;

    private Vector3 targetPosVector;

    [SerializeField]
    AIDestinationSetter destinationSetter;

    enum AIState
    {
        Idle, Patrol, Chase, Search
    }

    AIState batState = AIState.Patrol;

    // Start is called before the first frame update
    void Start()
    {
        nextTarget = wayPoints[wayPointIndex];

        destinationSetter.target = nextTarget;
    }

    // Update is called once per frame
    void Update()
    {
        Listen();

        if (batState == AIState.Patrol)
        {
            idling = false;
            BatPatrol();
        }
        else if(batState == AIState.Chase)
        {
            idling = false;
            BatChase();
        }
        else if(batState == AIState.Search)
        {
            idling = false;

            if (Vector3.Distance(nextTarget.position, transform.position) < distanceToMoveOn)
            {
                batState = AIState.Idle;
            }
        }
        else if(batState == AIState.Idle)
        {
            if (!idling)
            {
                idling = true;
                Invoke("ReturnToPatrol", 3f);
            }
        }


        if(Vector3.Distance(playerPos.position, transform.position) < proximityDetection)
        {
            //Debug.Log("Player in Bat's proximity");
            batState = AIState.Chase;
        }
        else if(Vector3.Distance(playerPos.position, transform.position) < proximityDetection)
        {
            batState = AIState.Search;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        isAudible = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isAudible = false;
    }

    private void Listen()
    {
        if (isAudible)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                //Debug.Log("Bat Heard Engine");
                targetPosVector = playerPos.position;

                nextTarget.position = targetPosVector;

                batState = AIState.Search;
            }
        }
    }

    private void ReturnToPatrol()
    {
        //Debug.Log("Bat lost target, returning to patrol");
        wayPointIndex = 0;

        targetPosVector = wayPoints[wayPointIndex].position;

        nextTarget.position = targetPosVector;

        destinationSetter.target = nextTarget;

        batState = AIState.Patrol;
    }

    private void BatPatrol()
    {
        if (Vector3.Distance(nextTarget.position, transform.position) < distanceToMoveOn)
        {
            if (wayPointIndex == wayPoints.Length - 1)
            {
                wayPointIndex = 0;
            }
            else
                wayPointIndex++;

            targetPosVector = wayPoints[wayPointIndex].position;

            nextTarget.position = targetPosVector;

            destinationSetter.target = nextTarget;
        }
    }

    private void BatChase()
    {
        targetPosVector = playerPos.position;

        nextTarget.position = targetPosVector;

        destinationSetter.target = nextTarget;
    }
}
