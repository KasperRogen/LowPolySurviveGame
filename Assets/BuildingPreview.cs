using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPreview : MonoBehaviour
{

    public enum BuildingType
    {
        Foundation,
        Wall,
        Floor,
        Roof,
        Ramp
    }

    public BuildingType buildingType;

    public bool isSnapped = false;
    MeshRenderer renderer;
    public Material goodMat;
    public Material badMat;
    public bool isFoundation;
    public List<SnapPoint.SnapPointType> pointsISnapTo;
    public GameObject BuildingBlock;
    public BuildingManager buildingManager;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
    }



    public bool GetSnapped()
    {
        return isSnapped;
    }



    public void Place()
    {
        Instantiate(BuildingBlock, transform.position, transform.rotation);
    }


    private void ChangeColor()
    {
        if (isSnapped)
        {
            renderer.material = goodMat;
        }
        else
        {
            renderer.material = badMat;
        }
    }

    // Update is called once per frame
    void Update()
    {




        if (isFoundation)
        {
            isSnapped = true;
        }
        ChangeColor();


        





    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    SnapPoint SnapPoint = other.GetComponent<SnapPoint>();
    //    if(SnapPoint != null && pointsISnapTo.Contains(SnapPoint.snapPointType)){
    //        buildingManager.PauseBuilding(true);
    //        transform.position = other.transform.position;
    //        isSnapped = true;
    //        ChangeColor();
    //    }
    //}


    //private void OnTriggerExit(Collider other)
    //{
    //    SnapPoint SnapPoint = other.GetComponent<SnapPoint>();
    //    if (SnapPoint != null && pointsISnapTo.Contains(SnapPoint.snapPointType))
    //    {
    //        isSnapped = false;
    //        ChangeColor();
    //    }
    //}


}
