using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VisionScript : MonoBehaviour {

    [SerializeField] private float seeingDistance;
    public float seeingAngle;
    [SerializeField] public float hearingDistance;
    [SerializeField] private float smellingDistance;



    private void Start()
    {


    }



    public GameObject LookFor(List<GameObject> GOS)
    {
        
        
        foreach(GameObject GO in GOS)
        {
            if (IsInVision(GO))
            {
                return GO;
            }
        }

        return null;
    }


    public GameObject LookFor(GameObject GO)
    {
            if (IsInVision(GO))
            {
                return GO;
            }

        return null;
    }


    


    public GameObject SmellFor(List<GameObject> GOS)
    {
        foreach(GameObject GO in GOS) { 
            if (IsWithingSMellingRadius(GO))
            {
                return GO;
            }
        }

        return null;
    }


    private bool IsWithingSMellingRadius (GameObject GO)
    {
        return Vector3.Distance(GO.transform.position, transform.position) <= smellingDistance;
    }

    private bool IsInVision(GameObject GO)
    {
        Vector3 visionVector = transform.forward;
        Vector3 targetVector = (GO.transform.position - transform.position).normalized;

        
        if (Vector3.Angle(visionVector, targetVector) <= seeingAngle / 2)
        {
            Debug.DrawRay(transform.position, targetVector * seeingDistance, Color.red, 0.1f);

            RaycastHit hit;

            if(Physics.Raycast(transform.position, targetVector, out hit, seeingDistance)){
                if (hit.transform == GO.transform)
                {
                    return true;
                }
            }
        }

        return false;

    }




    private void OnDrawGizmosSelected()
    {
        Color c = new Color(0, 0, 1, 0.3f);

        float detectionAngle = seeingAngle;

        UnityEditor.Handles.color = c;
        Vector3 rotatedForward = Quaternion.Euler(0, -detectionAngle * 0.5f, 0) * transform.forward;
        UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.up, rotatedForward, detectionAngle, seeingDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, hearingDistance);

    }


}
