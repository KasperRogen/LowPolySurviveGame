using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSchool : MonoBehaviour
{
    public GameObject fish;
    public int schoolSize;
    public List<Vector3> wayPoints;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            wayPoints.Add(transform.GetChild(i).transform.position);
        }

        for(int i = 0; i < schoolSize; i++)
        {
            GameObject newFish = Instantiate(fish, transform.position, Quaternion.identity);
            newFish.GetComponent<Fish>().wayPoints = wayPoints;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
