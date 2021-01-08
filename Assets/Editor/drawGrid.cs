using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(visit))]

public class drawGrid : Editor
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnSceneGUI()
    {
        Handles.color = Color.red;
        visit myObj = (visit)target;
        //Handles.DrawWireCube(myObj.transform.position, new Vector3 (1,1,1));
    }
}
