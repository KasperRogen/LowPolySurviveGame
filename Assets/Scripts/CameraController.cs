using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity;
    private float xAxisClamp;

    [SerializeField] private Transform playerTransform;


    private void Awake()
    {
        LockCursor();
        xAxisClamp = 0;
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Update()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xAxisClamp += mouseY;

        if (xAxisClamp < 90 && xAxisClamp > -90)
            transform.Rotate(Vector3.left * mouseY);
        else xAxisClamp -= mouseY;

        playerTransform.Rotate(Vector3.up * mouseX);

    }


    //private void OnMouseDown()
    //{
    //    Debug.Log("YAAASS");
    //    RaycastHit hit;
    //    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
    //    {
    //        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
    //        Debug.Log("Did Hit " + hit.transform.gameObject.name);
    //    }
    //}


}
