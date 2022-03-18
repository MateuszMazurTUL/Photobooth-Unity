using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class OBJLoaderManager : MonoBehaviour
{
    [SerializeField] string path= "Recources/Input";

    Object[] loadedObject;
    GameObject preview;
    int index = 0;

    void Start()
    {
        Debug.Log(Application.dataPath);
        //loading model
        try
        {
            loadedObject = Resources.LoadAll(path, typeof(Object));
            DisplayObject(loadedObject[0]);
        }
        catch
        {
            Debug.Log ("Can't load any model!");
            //TODO: UI error display
        }
        //create path directory
        try
        {
            Directory.CreateDirectory(path);
        }
        catch
        {
            Debug.Log("CreateDirectory failed;");
        }
    }

    public void NextObject()
    {
        //set index of next model
        index++;
        if (index > loadedObject.GetLength(0)-1) index = 0; //loop

        //if can't load go to next
        if (!DisplayObject(loadedObject[index])) NextObject(); //
    }

    public void PrevObject()
    {
        //set index of previous model
        index--;
        if (index < 0) index = loadedObject.GetLength(0) - 1;

        //if can't load go to previous
        if (!DisplayObject(loadedObject[index])) PrevObject();
    }

    bool DisplayObject(Object obj)
    {
        try
        {
            Destroy(preview);
            preview = (GameObject)Instantiate(obj, new Vector3(0, 0, 0), Quaternion.identity);
            return true;
        }
        catch
        {
            //Debug.Log(obj);
            return false;
        }
    }
}
