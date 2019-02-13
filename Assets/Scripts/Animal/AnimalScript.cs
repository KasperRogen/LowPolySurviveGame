using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalScript : MonoBehaviour {

    public float calories;
    public float maxCalories;

    [SerializeField] float energy;
    [SerializeField] float maxEnergy;
    [SerializeField] Animator animator;
    [SerializeField] float maxHealth;
    [SerializeField] NavMeshAgent agent; 
    [SerializeField] private GameObject Player;


    public bool IsHungry = false;



    public float health;

    public AnimalStates state;

    public enum AnimalStates
    {
        ROAMING,
        FLEEING,
        EATING
    };


	// Use this for initialization
	void Start () {
        
        AnimalController.AnimalAiUpdate += BasicNeedsUpdater;

        //state = AnimalStates.ROAMING;
        //AnimalController.AnimalAiUpdate += DecideBehaviour;

        //NoiseManager.NoiseMessage += AlertNoise;
    }

    // Update is called once per frame
    void FixedUpdate () {
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }



    void BasicNeedsUpdater()
    {
        calories -= 10;



        IsHungry = (calories < maxCalories * 0.5f);

    }


    

}
