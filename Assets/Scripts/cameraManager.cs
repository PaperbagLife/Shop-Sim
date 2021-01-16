using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Camera[] cams;
    void Start()
    {
    	for (int i = 1; i < cams.Length; i++)
		{
			cams[i].enabled = false;
		}
    }
    void switchCamera(int i)
	{
		Debug.Log("swtiching" + i);
		foreach(Camera cam in cams)
		{
			cam.enabled = false;
		}
		cams[i].enabled = true;
	}
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < cams.Length; ++i)
		{
			if (Input.GetKeyDown("" + i))
			{
				Debug.Log("keydown" + i);
				switchCamera(i);
			}
		}
    }
}
