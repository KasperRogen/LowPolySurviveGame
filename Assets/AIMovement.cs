using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour {

    NavMeshAgent agent;
    VisionScript vision;
    AnimalScript animal;

    [SerializeField] float fleeingDistance;
    [SerializeField] private GameObject Player;
    

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        vision = GetComponent<VisionScript>();
        animal = GetComponent<AnimalScript>();
    }


    Vector3 PickDestination(float radius)
    {
        Vector3 destination = transform.position + new Vector3(Random.Range(-radius, radius), 0, Random.Range(-radius, radius));
        if (agent.CalculatePath(destination, new NavMeshPath()) != null)
            return destination;
        else
            return PickDestination(radius);
    }

    public void Flee()
    {

        animal.state = AnimalScript.AnimalStates.FLEEING;

        Vector3 fleeingDirection = (transform.position - Player.transform.position);
        Vector3 destination = PickDestination(8);
        Vector3 newDirection = (destination - transform.position).normalized;

        
        if (fleeingDirection.magnitude < fleeingDistance)
        {
            if(Vector3.Distance(transform.position, agent.destination) < 2 || Vector3.Angle((agent.destination - transform.position).normalized, fleeingDirection) > vision.seeingAngle / 2)
            {
                if(Vector3.Angle(newDirection, fleeingDirection.normalized) > vision.seeingAngle / 2)
                {
                    Flee();
                }
                else
                {
                    agent.destination = destination;
                }
                
            } 
        } else {
            animal.state = AnimalScript.AnimalStates.ROAMING;
            AnimalController.CriticalAnimalAiUpdate -= Flee;
        }
        
    }

    
    public void Roam()
    {
        animal.state = AnimalScript.AnimalStates.ROAMING;
        Vector3 roamPosition = PickDestination(40);

        if (agent.CalculatePath(roamPosition, new NavMeshPath()))
            agent.destination = roamPosition;
        else Roam();
    }


    public void SeekFood()
    {

        animal.state = AnimalScript.AnimalStates.EATING;
        List<GameObject> foods = GameObject.FindGameObjectsWithTag("Food").ToList();

        foods.Sort((food1, food2) => Vector3.Distance(transform.position, food1.transform.position).CompareTo(
                                     Vector3.Distance(transform.position, food1.transform.position)));

        foods = foods.Where(food => agent.CalculatePath(food.transform.position, new NavMeshPath()) != null).ToList();
        GameObject target = vision.SmellFor(foods);
        
        if(target != null) { 
            agent.destination = target.transform.position;
        } else {
            Roam();
        }
    }

    

}
