using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour {

    [SerializeField] private float AiUpdateTimer;
    [SerializeField] private float CriticalAiUpdateTimer;

    private float timer;
    private float criticalTimer;

    public delegate void UpdateAnimalBehaviour();
    public static event UpdateAnimalBehaviour AnimalAiUpdate;

    public delegate void CriticalUpdateAnimalBehaviour();
    public static event CriticalUpdateAnimalBehaviour CriticalAnimalAiUpdate;

	// Update is called once per frame
	void Update () {

        float deltaTime = Time.deltaTime;

		if(AnimalAiUpdate != null && (timer += deltaTime) >= AiUpdateTimer)
        {
            timer = 0;
            AnimalAiUpdate();
        }

        if(CriticalAnimalAiUpdate != null && (criticalTimer += deltaTime) >= CriticalAiUpdateTimer)
        {
            criticalTimer = 0;
            CriticalAnimalAiUpdate();
        }
	}
}
