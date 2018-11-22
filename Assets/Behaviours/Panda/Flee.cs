using Panda;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Flee : MonoBehaviour {

    [SerializeField] private GameObject Player;
    [SerializeField] private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    [Task]
    public void PlayerClose(float radius)
    {
        if (Vector3.Distance(Player.transform.position, transform.position) < radius)
        {
            Task.current.Succeed();
        }
        Task.current.Fail();
    }


    [Task]
    public void PlayerCloseAndInSight(float seeingDistance, float seeingAngle)
    {
        if (Vector3.Distance(Player.transform.position, transform.position) < seeingDistance)
        {
            if(IsInVision(seeingDistance, seeingAngle))
            {
                Task.current.Succeed();
            }
        }

        Task.current.Fail();

    }

    public Vector3 PickDestination(float fleeingDistance, float seeingAngle)
    {
        Vector3 fleeingDirection = (transform.position - Player.transform.position);
        Vector3 destination = PickDestination(fleeingDistance, seeingAngle);
        Vector3 newDirection = (destination - transform.position).normalized;


            if (Vector3.Distance(transform.position, agent.destination) < 2 || Vector3.Angle((agent.destination - transform.position).normalized, fleeingDirection) > seeingAngle / 2)
            {
                if (Vector3.Angle(newDirection, fleeingDirection.normalized) > seeingAngle / 2)
                {
                    PickDestination(fleeingDistance, seeingAngle);
                }
                else
                {
                    return agent.CalculatePath(destination, new NavMeshPath()) ? destination : PickDestination(fleeingDistance, seeingAngle);
                }

            }
        return agent.destination;
    }


    [Task]
    public void FleeTask(float fleeingDistance, float seeingAngle)
    {
        Vector3 fleeingDirection = (transform.position - Player.transform.position);
        if (fleeingDirection.magnitude < fleeingDistance)
        {
            agent.destination = PickDestination(fleeingDistance, seeingAngle);
        }
        Task.current.Succeed();
    }

    private bool IsInVision(float seeingDistance, float seeingAngle)
    {
        Vector3 visionVector = transform.forward;
        Vector3 targetVector = (Player.transform.position - transform.position).normalized;


        if (Vector3.Angle(visionVector, targetVector) <= seeingAngle / 2)
        {
            Debug.DrawRay(transform.position, targetVector * seeingDistance, Color.red, 0.1f);

            RaycastHit hit;

            if (Physics.Raycast(transform.position, targetVector, out hit, seeingDistance))
            {
                if (hit.transform == Player.transform)
                {
                    return true;
                }
            }
        }

        return false;

    }

}
