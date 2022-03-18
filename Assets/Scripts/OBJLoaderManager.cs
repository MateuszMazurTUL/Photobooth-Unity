using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBJLoaderManager : MonoBehaviour
{
    //TODO: handle error if 0 models
    Object[] loadedObject;
    GameObject preview;
    int index = 0;

    void Start()
    {
        try
        {
            loadedObject = Resources.LoadAll("Input", typeof(Object));
            DisplayObject(loadedObject[0]);
        }
        catch
        {
            Debug.Log ("Can't load any model!");
            //TODO: UI error display
        }
        /*Debug.Log("Start");
        foreach (var t in loadedObject)
        {
            Debug.Log(t.name);
        }
        Debug.Log(loadedObject.GetLength(0));*/
    }

    void Update()
    {
        //TODO: Input system
        /*if (Input.GetButtonDown("Fire2"))
            prevObject();
        if (Input.GetButtonDown("Fire1"))
           nextObject();*/
    }

    public void NextObject()
    {
        //set index of next model
        index++;
        if (index > loadedObject.GetLength(0)-1) index = 0; //loop

        if(!DisplayObject(loadedObject[index])) NextObject(); //
    }

    public void PrevObject()
    {
        //set index of previous model
        index--;
        if (index < 0) index = loadedObject.GetLength(0) - 1;

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
