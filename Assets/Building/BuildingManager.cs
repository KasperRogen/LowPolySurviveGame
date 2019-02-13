using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    Vector3 hitPoint = Vector3.zero;

    public List<GameObject> buildingBlocks;

    [HideInInspector]
    public bool isBuilding = false;
    private bool pauseBuilding = false;
    public float stickTolerance = 1.5f;

    GameObject previewGO;
    BuildingPreview previewScript;
    

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))//Rotate
        {
            if(previewGO != null) { 
                previewGO.transform.Rotate(Vector3.up, 90);
            }
        }


        if (Input.GetKeyDown(KeyCode.X))//Cancel
        {
            isBuilding = false;
            Destroy(previewGO);
            previewGO = null;
            previewScript = null;
        }

        if (Input.GetKeyDown(KeyCode.H)) //Build foundation
        {
            NewBuild(buildingBlocks.Find(block => block.GetComponent<BuildingPreview>().buildingType == BuildingPreview.BuildingType.Foundation));
        }


        if (Input.GetKeyDown(KeyCode.J)) //Build foundation
        {
            NewBuild(buildingBlocks.Find(block => block.GetComponent<BuildingPreview>().buildingType == BuildingPreview.BuildingType.Wall));
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            NewBuild(buildingBlocks.Find(block => block.GetComponent<BuildingPreview>().buildingType == BuildingPreview.BuildingType.Floor));
        }


        if (Input.GetKeyDown(KeyCode.L))
        {
            NewBuild(buildingBlocks.Find(block => block.GetComponent<BuildingPreview>().buildingType == BuildingPreview.BuildingType.Ramp));
        }

        if (Input.GetMouseButtonDown(0))//Rotate
        {
            if(previewScript == null)
            {
                return;
            }

            if (previewScript.GetSnapped())
            {
                StopBuilding();
            } else
            {
                Debug.Log("Not Snapped, can't place!");
            }
        }

        if (isBuilding)
        {
            if (pauseBuilding)
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f, previewScript.layerMask.value))
                {
                    if ((hit.point - previewGO.transform.position).magnitude > stickTolerance)
                    {
                        pauseBuilding = false;
                    } 

                }
            }
            else
            {
                DoBuildRay();
            }
        }

    }


    public void NewBuild(GameObject GO)
    {
        if(GO == null)
        {
            return;
        }

        if (isBuilding == true) { 
            CancelBuilding();
        }

        previewGO = Instantiate(GO, Vector3.zero, Quaternion.identity);
        previewGO.layer = LayerMask.NameToLayer("BuildingPreview");
        previewScript = previewGO.GetComponent<BuildingPreview>();
        previewScript.buildingManager = this;
        isBuilding = true;
    }


    public void CancelBuilding()
    {
        Destroy(previewGO);
        previewGO = null;
        previewScript = null;
        isBuilding = false;
    }



    public void StopBuilding()
    {
        previewScript.Place();
    }




    public void PauseBuilding(bool value)
    {
        pauseBuilding = value;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(hitPoint, 2);
    }



    private void DoBuildRay()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f, previewScript.layerMask.value))
        {
            previewGO.transform.position = hit.point;
            hitPoint = hit.point;
            
            SnapPoint snap = hit.transform.GetComponent<SnapPoint>();

            if(snap != null && previewScript.pointsISnapTo.Contains(snap.snapPointType))
            {
                previewGO.transform.position = snap.transform.GetChild(0).transform.position;
                PauseBuilding(true);
                previewScript.isSnapped = true;
            } else
            {
                if (previewScript.isFoundation)
                {
                    Collider[] colls = Physics.OverlapSphere(hit.point, 2);
                    
                    string points = "";
                    foreach (Collider coll in colls){
                        SnapPoint snappeDoo = coll.transform.GetComponent<SnapPoint>();
                        if(snappeDoo != null)
                        {
                            points += snappeDoo.snapPointType + ", ";
                            if (previewScript.pointsISnapTo.Contains(snappeDoo.snapPointType))
                            {
                                previewScript.isSnapped = false;
                                return;
                            }
                        }
                    }
                    Debug.Log(points);
                    previewScript.isSnapped = true;
                    return;
                }


                previewScript.isSnapped = false;
            }

        }

    }



}
