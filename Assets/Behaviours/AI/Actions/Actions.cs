using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Actions : MonoBehaviour {


    // Use this for initialization
    void Start () {		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
[Action("MyActions/Eat")]
public class Eat : GOAction
{
    [InParam("FoodObject")]
    public GameObject Food;

    private float eatingAmount = 25;
    private Resource resource;
    private AnimalScript animal;

    public override TaskStatus OnUpdate()
    {
        if (Food.GetComponent<Resource>() != null) { 
            resource = Food.GetComponent<Resource>();
            if(resource.type == Resource.ResourceType.FOOD)
            {
                if(gameObject.GetComponent<AnimalScript>() != null)
                {
                    animal = gameObject.GetComponent<AnimalScript>();

                    if(resource.resourceAmount > eatingAmount)
                    {
                        animal.calories += eatingAmount;
                        resource.resourceAmount -= eatingAmount;
                    } else
                    {
                        animal.calories += resource.resourceAmount;
                        resource.resourceAmount = 0;
                    }
                    return TaskStatus.COMPLETED;
                }
            }
            
        }

        return TaskStatus.FAILED;
    } // OnUpdate
}





[Action("MyActions/Flee")]
public class Flee : GOAction
{
    [InParam("Player")]
    public GameObject player;

    [InParam("FleeDistance")]
    public float fleeDistance;

    private AnimalScript animal;
    private NavMeshAgent agent;
    private Vector3 targetPositon;


    [OutParam("LegIt")]
    public bool Fleeing;


    private Vector3 GetPosition()
    {
        Vector3 randDirection = Random.insideUnitSphere * 5;
        randDirection += gameObject.transform.position;
        NavMeshHit hit;

        LayerMask mask = new LayerMask();
        mask = LayerMask.NameToLayer("Landscape");

        NavMesh.SamplePosition(randDirection, out hit, 20, mask);

        Vector3 fleeDirection = gameObject.transform.position - player.transform.position;
        Vector3 newDirection = hit.position - gameObject.transform.position;
        
        if (Vector3.Angle(fleeDirection, newDirection) < 90)
        {
            return hit.position;
        }
        else
        {
            return GetPosition();
        }
    }


    Vector3 PickDestination(float radius)
    {
        Vector3 destination = gameObject.transform.position + new Vector3(Random.Range(-radius, radius), 0, Random.Range(-radius, radius));
        if (Vector3.Angle(gameObject.transform.position - player.transform.position, destination - gameObject.transform.position) < 45)
            return agent.CalculatePath(destination, new NavMeshPath()) != null ? destination : PickDestination(radius);
        else
            return PickDestination(radius);
    }


    public override void OnStart()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        animal = gameObject.GetComponent<AnimalScript>();
        targetPositon = GetPosition();

        
        agent.destination = targetPositon;
        animal.state = AnimalScript.AnimalStates.FLEEING;
        Fleeing = true;
        agent.isStopped = false;
    }

    public override TaskStatus OnUpdate()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            Debug.Log("DISTANCE: " + Vector3.Distance(gameObject.transform.position, player.transform.position));
            if (Vector3.Distance(gameObject.transform.position, player.transform.position) > fleeDistance)
            {
                animal.state = AnimalScript.AnimalStates.ROAMING;
                Fleeing = false;
                return TaskStatus.COMPLETED;
            }
            else { 
                agent.destination = GetPosition();
                agent.isStopped = false;
            }
        }

        
        return TaskStatus.RUNNING;
        

    } // OnUpdate
}




[Action("MyActions/IsFleeing")]
[Help("Checks whether the gameobject is currently fleeing")]
public class IsFleeing : GOAction
{
    [OutParam("IsFleeing")]
    public bool Fleeing;

    public override void OnStart()
    {
        AnimalScript animal = gameObject.GetComponent<AnimalScript>();
        Fleeing = (animal.state == AnimalScript.AnimalStates.FLEEING);
        Debug.LogWarning(Fleeing);
    }


    public override TaskStatus OnUpdate()
    {
        Debug.LogWarning("qwer");
        return TaskStatus.COMPLETED;
    } // OnUpdate

} // class IsNightCondition
