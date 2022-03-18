using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] CameraManipulator cm;

    Vector3 oldMP; //old mouse position
    Vector3 newMP;
    Vector3 mouseShift;

    [SerializeField] float maxShiftH = .1f; //max (de)incres value per input
    [SerializeField] float maxShiftV = .1f; //max (de)incres value per input
    [SerializeField] float maxShiftZ = .7f; //max (de)incres value per input
    [SerializeField] float maxShiftMP = .1f; //max (de)incres value per input


    // Start is called before the first frame update
    void Start()
    {
        oldMP = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {

        float newH = Input.GetAxis("Horizontal");
        float newV = Input.GetAxis("Vertical");
        float newZ = Input.mouseScrollDelta.y; //zoom

        if (Input.GetButton("Fire1"))
        {
            cm.SmoothMode(false);
            newMP = Input.mousePosition;
            mouseShift = oldMP - newMP;
            oldMP = newMP;
        }
        else
        {
            cm.SmoothMode(true);
        }

        /*Debug.Log(mouseShift);
        if (newH > maxShiftH) newH = maxShiftH;
        if (newV > maxShiftV) newV = maxShiftV;
        if (newZ > maxShiftZ) newZ = maxShiftZ;
        if (newZ < -maxShiftZ) newZ = -maxShiftZ;*/
        //if (newMP.x > maxShiftMP) newMP = maxShiftMP;
        //if (newMP.x < -maxShiftMP) newMP = -maxShiftMP;


        if (newV != 0) cm.RotationX(-newV);
        if (newH != 0) cm.RotationY(-newH);
        if (newZ != 0) cm.Zoom(newZ);
        if (mouseShift.x != 0) cm.RotationY(mouseShift.x);
        if (mouseShift.y != 0) cm.RotationX(mouseShift.y);
    }
}
