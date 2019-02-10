using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{


    public List<GameObject> buildingBlocks;

    public bool isBuilding = false;
    private bool pauseBuilding = false;
    public float stickTolerance = 1.5f;

    GameObject previewGO;
    BuildingPreview previewScript;

    public LayerMask layerMask; 

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

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f, layerMask.value))
                {
                    if ((hit.point - previewGO.transform.position).magnitude > stickTolerance)
                    {
                        pauseBuilding = false;
                    } 

                }



                //float mouseX = Input.GetAxis("Mouse X");
                //float mouseY = Input.GetAxis("Mouse Y");

                //Debug.Log(Mathf.Abs(mouseX));

                //if (Mathf.Abs(mouseX) >= stickTolerance || Mathf.Abs(mouseY) >= stickTolerance)
                //{
                //    pauseBuilding = false;
                //}
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



    private void DoBuildRay()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f, layerMask.value))
        {

            previewGO.transform.position = hit.point;

        }

    }



}
