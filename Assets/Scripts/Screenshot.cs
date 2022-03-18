using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    [SerializeField] InputManager im;

    [SerializeField] string path_pre = "/Output/";
    [SerializeField] string filename = "screenshot_";
    [SerializeField] string path_post = ".png";
    string path;

    bool isScreenshoting = false;

    void Start()
    {
        path = Application.dataPath + path_pre + filename + "0" + path_post;
        //create path directory
        try
        {
            Directory.CreateDirectory(Application.dataPath + path_pre);
        }
        catch
        {
            Debug.Log("CreateDirectory failed;");
        }
    }

    public void CaptureTheScreen()
    {
        im.switchGUI(false); // turn off GUI. We dont wanna it on SS

        int i = 0; //iterator for filename

        while (true){
            if (File.Exists(path)) //search for free filename
            {
                i++;
                path = Application.dataPath + path_pre + filename + i.ToString() + path_post;
            }
            else // end when we have free filename
                break;
        }

        try
        {
            ScreenCapture.CaptureScreenshot(path); // capture screen
        }
        catch
        {
            Debug.LogError("Screenshot failed;");
        }
        finally
        {
            isScreenshoting = true; //flag for turning on GUI in update method
        }

    }

    void Update()
    {
        //turning on GUI when ss is captured
        if (isScreenshoting)
        {
            if (File.Exists(path))
            {
                Debug.Log("Screenshot saved;");
                im.switchGUI(true);
                isScreenshoting = false;
            }
        }
    }
}
