using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalScript : MonoBehaviour {

    public float calories;
    public float maxCalories;

    [SerializeField] float energy;
    [SerializeField] float maxEnergy;

    [SerializeField] float maxHealth;

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


    private AIMovement movement;
    private VisionScript vision;

	// Use this for initialization
	void Start () {
        vision = GetComponent<VisionScript>();
        movement = GetComponent<AIMovement>();

        AnimalController.AnimalAiUpdate += BasicNeedsUpdater;

        //state = AnimalStates.ROAMING;
        //AnimalController.AnimalAiUpdate += DecideBehaviour;

        //NoiseManager.NoiseMessage += AlertNoise;
    }

    // Update is called once per frame
    void Update () {
		
	}



    void BasicNeedsUpdater()
    {
        calories -= 10;




        
        IsHungry = (calories < maxCalories * 0.5f);

    }



    //void DecideBehaviour()
    //{
    //    if(state != AnimalStates.FLEEING) { 

    //        if(calories < maxCalories * 0.2f) { 

    //                movement.SeekFood();

    //        } else
    //        {

    //                movement.Roam();

    //        }
            
    //    }


    //    calories -= 10;
    //}

    //void AlertNoise(Vector3 position, float volume)
    //{

    //    if (Vector3.Distance(transform.position, position) < vision.hearingDistance && Vector3.Distance(transform.position, position) < volume)
    //    {
    //        if (state != AnimalStates.FLEEING)
    //            AnimalController.CriticalAnimalAiUpdate += movement.Flee;

    //        movement.Flee();
    //    }
    //}

    //public void Alert()
    //{

    //    if(vision.LookFor(Player) != null)
    //    {
    //        if (state != AnimalStates.FLEEING)
    //            AnimalController.CriticalAnimalAiUpdate += movement.Flee;

    //        movement.Flee();
    //    }
    //}


}
