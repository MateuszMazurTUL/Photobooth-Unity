using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManipulator : MonoBehaviour
{

    [SerializeField] [Range(-180, 180)]internal float y_axis = 0; //left / right
    [SerializeField] [Range(-180, 180)] internal float x_axis = -18; //up / down
    [SerializeField] [Range(1, 50)] internal float zoom = 5; //zoom

    //limits
    [SerializeField] float min_zoom = 1;
    [SerializeField] float max_zoom = 50;
    [SerializeField] float max_zoom_change = .5f;
    [SerializeField] float max_angle_change = 5f;
    //smooth camera
    [SerializeField] [Range(0, 10)] float smooth = 1.0f;

    bool smoothMode = true;

    //target values for rotation/position. Used by smoothMode
    Quaternion targetRotation;
    Vector3 targetPosition;
    
    // Update is called once per frame
    void Start()
    {
        UpdateCam();
    }

    void Update()
    {
        if (this.smoothMode) //smooth mode. Used by UI_btns and keyboard input
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, this.targetRotation, Time.deltaTime * smooth);
            Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, this.targetPosition, Time.deltaTime * smooth);
        }
        else //precision mode. Used by mouse
        {
            transform.rotation = targetRotation;
            Camera.main.transform.localPosition = targetPosition;
        }
    }

    public void RotationX(float angleX = 0)
    {
        //Clamp arg angle between maximum and minimum angle change
        angleX = -Mathf.Clamp(angleX, -this.max_angle_change, this.max_angle_change);
        this.x_axis += angleX;
        UpdateCam();
        
    }

    public void RotationY(float angleY = 0)
    {
        //Clamp arg angle between maximum and minimum angle change
        angleY = -Mathf.Clamp(angleY, -this.max_angle_change, this.max_angle_change);

        this.y_axis += angleY;
        UpdateCam();
        
    }

    public void Zoom(float zoom = 0)
    {
        //Clamp arg zoom between maximum and minimum zoom change
        zoom = -Mathf.Clamp(zoom, -this.max_zoom_change, this.max_zoom_change);

        //Clamp value between maximum and minimum zoom in world position
        float newZoom = this.zoom + zoom;
        this.zoom = Mathf.Clamp(newZoom, this.min_zoom, this.max_zoom);
        UpdateCam();
        
    }

    void UpdateCam()
    {
        //new position for cam
        targetRotation = Quaternion.Euler(x_axis, y_axis, 0);
        targetPosition = new Vector3(0, 0, zoom);
    }

    //switch smoothmode
    public void SmoothMode(bool b)
    {
        this.smoothMode = b;
        //this.targetRotation = transform.rotation;
        //this.targetPosition = Camera.main.transform.localPosition;
    }
}
