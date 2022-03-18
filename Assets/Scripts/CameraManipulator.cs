using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManipulator : MonoBehaviour
{
    
    internal float y_axis = 0; //left / right
    internal float x_axis = -18; //up / down
    internal float zoom = 5; //zoom

    float min_zoom = 1;
    float max_zoom = 50;
    float max_zoom_change = .5f;
    float max_angle_change = 5f;
    float smooth = 1.0f;

    bool smoothMode = true;

    Quaternion targetRotation;
    Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    // Update is called once per frame
    void Update()
    {
        if (this.smoothMode)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smooth);
            Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, targetPosition, Time.deltaTime * smooth);
        }
        else
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

    public void SmoothMode(bool b)
    {
        this.smoothMode = b;
    }
}
