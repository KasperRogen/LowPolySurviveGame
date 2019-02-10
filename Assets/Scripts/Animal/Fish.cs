using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{

    public List<Vector3> wayPoints = new List<Vector3>();
    private Vector3 currentWaypoint = Vector3.zero;
    private Vector3 waypointVariator = Vector3.zero;
    public int speed;

    Random rng;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        

        if(Random.RandomRange(0,1000) > 997)
        {
            currentWaypoint = wayPoints[Random.RandomRange(0, wayPoints.Count)];
            waypointVariator = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
            transform.LookAt(currentWaypoint);
        }
        
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint + waypointVariator, speed * Time.deltaTime);
    }
}
