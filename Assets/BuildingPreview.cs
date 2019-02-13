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


    [HideInInspector]
    public bool isSnapped = false;
    private MeshRenderer myRenderer;




    [Header("Base settings")]
    public LayerMask layerMask;
    [Space(10)]

    [Header("Materials")]
    public Material goodMat;
    public Material badMat;
    [Space (10)]

    [Header("Building Block")]
    public BuildingType buildingType;
    public bool isFoundation;
    public List<SnapPoint.SnapPointType> pointsISnapTo;
    public GameObject BuildingBlock;
    [Space(10)]

    [HideInInspector]
    public BuildingManager buildingManager;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<MeshRenderer>();
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
            myRenderer.material = goodMat;
        }
        else
        {
            myRenderer.material = badMat;
        }
    }

    // Update is called once per frame
    void Update()
    {




        //if (isFoundation)
        //{
        //    isSnapped = true;
        //}
        ChangeColor();


        





    }



}
