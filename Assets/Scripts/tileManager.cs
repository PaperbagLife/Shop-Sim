using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileManager : MonoBehaviour
{
	
	// Start is called before the first frame update
	public Dictionary<Vector2Int, int> visitLog = new Dictionary<Vector2Int, int>();
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			Debug.Log("Printing log");
			foreach(var item in visitLog)
			{
				Debug.Log("BlockID: " + item.Key + ", " + "Visited:" + item.Value + " times");
			}
		}
	}

	public void initDict(Vector3 curPos)
	{
		Vector2Int blockID = new Vector2Int ((int)curPos[0], (int)curPos[2]);
		visitLog.Add(blockID, 0);

	}
	public void onVisit(Vector3 curPos)
	{
		Vector2Int blockID = new Vector2Int ((int)curPos[0], (int)curPos[2]);
		visitLog[blockID] = visitLog[blockID] + 1;
	}
}
