using Panda;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Roam : MonoBehaviour {


    [SerializeField] private NavMeshAgent agent;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    [Task]
    public void RoamTask()
    {
        if(agent.destination != null && Vector3.Distance(transform.position, agent.destination) < 2) { 
            agent = gameObject.GetComponent<NavMeshAgent>();
            agent.destination = PickDestination(10);
            agent.isStopped = false;
            Task.current.Succeed();
        }

        Task.current.Succeed();
    }

    public Vector3 PickDestination(float radius)
    {
        Vector3 destination = gameObject.transform.position + new Vector3(Random.Range(-radius, radius), 0, Random.Range(-radius, radius));
            return agent.CalculatePath(destination, new NavMeshPath()) != null ? destination : PickDestination(radius);
    }
}
