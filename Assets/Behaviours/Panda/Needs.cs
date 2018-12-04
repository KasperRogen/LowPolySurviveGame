using Panda;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Needs : MonoBehaviour {
    [SerializeField] private GameObject currentFood;
    [SerializeField] private AnimalScript animal;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    [Tooltip("The amount of food an animal eats per bite")]
    [SerializeField] private float biteSize;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    [Task]
    void IsFoodClose(float radius)
    {Collider[] colls = Physics.OverlapSphere(transform.position, radius);
        colls = colls.Where(coll => coll.tag == "Food").ToArray();
        if (colls.Any(coll => Vector3.Distance(transform.position, coll.transform.position) < radius))
        {
            Task.current.Succeed();
            return;
        }
        Task.current.Fail();
    }



    [Task]
    void SeekFood(float radius)
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, radius);


        colls = colls.Where(coll => coll.tag == "Food").ToArray();


        if (colls.Length <= 0 || colls == null) { 
            Task.current.Fail();
            return;
        }

        GameObject target = colls.ToList().Aggregate((coll1, coll2) => 
            Vector3.Distance(transform.position, coll1.transform.position) <
            Vector3.Distance(transform.position, coll2.transform.position) ? coll1 : coll2).gameObject;


        currentFood = target;
        agent.destination = target.transform.position + (transform.position - target.transform.position).normalized;

        Task.current.Succeed();
    }

    bool IsFoodWithinEatingRange(float range, GameObject target)
    {
        if(Vector3.Distance(transform.position, target.transform.position) <= range)
        {
            return true;
        } else
        {
            return false;
        }
    }

    [Task]
    void Eat()
    {
        Debug.Log("GINGER BEER");
        animator.SetBool("Eating", true);
           Resource resource = currentFood.GetComponent<Resource>();
        if (resource != null && resource.type == Resource.ResourceType.FOOD)
        {
            if(animal.calories >= animal.maxCalories)
            {
                Task.current.Succeed();
                animator.SetBool("Eating", false);
                return;
            }

            if (resource.resourceAmount > biteSize)
            {
                resource.resourceAmount -= biteSize;
                animal.calories += biteSize;
            }
            else
            {
                animal.calories += resource.resourceAmount;
                resource.resourceAmount = 0;
                Destroy(resource.gameObject);
                animator.SetBool("Eating", false);
                Task.current.Succeed();
            }
        }
        Task.current.Succeed();
    }
    
    [Task]
    public void IsNight()
    {
        if (GameObject.FindGameObjectWithTag("MainLight").GetComponent<DayNightCycle>().IsNight())
        {
            Task.current.Succeed();
            return;
        }

        Task.current.Fail();
    }


    [Task]
    public void Hungry()
    {
        if (animal.IsHungry)
        {
            Task.current.Succeed();
            return;
        }

        Task.current.Fail();
        animator.SetBool("Eating", false);
    }

    [Task]
    public void Full()
    {
        if (animal.calories >= animal.maxCalories)
        {
            Task.current.Succeed();
            return;
        }

        Task.current.Fail();
    }


}
