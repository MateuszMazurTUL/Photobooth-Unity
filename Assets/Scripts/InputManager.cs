using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] CameraManipulator cm;
    [SerializeField] GameObject canvas;

    //mouse input
    Vector3 oldMP; //old mouse position
    Vector3 newMP;
    Vector3 mouseShift;


    // Start is called before the first frame update
    void Start()
    {
        oldMP = Input.mousePosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float newH = Input.GetAxis("Horizontal"); //for keybord input(and other)
        float newV = Input.GetAxis("Vertical");//for keybord input(and other)
        float newZ = Input.mouseScrollDelta.y; //zoom with scroll

        if (Input.GetMouseButton(0))  //update mouse only when LMB is press
        {
            cm.SmoothMode(false); //precision control
            newMP = Input.mousePosition;
            mouseShift = oldMP - newMP; //move direction
            oldMP = newMP;
        } else
        {
            mouseShift = Vector3.zero;
            cm.SmoothMode(true);
        }

        //update position
        if (newV != 0) cm.RotationX(-newV);
        if (newH != 0) cm.RotationY(-newH);
        if (newZ != 0) cm.Zoom(newZ);
        if (mouseShift.x != 0) cm.RotationY(mouseShift.x);
        if (mouseShift.y != 0) cm.RotationX(mouseShift.y);
    }

    internal void switchGUI()
    {
        if (canvas.activeInHierarchy) canvas.SetActive(false); else canvas.SetActive(true);
    }

    internal void switchGUI(bool b)
    {
        canvas.SetActive(b);
    }
}
