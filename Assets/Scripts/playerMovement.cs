using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class playerMovement : MonoBehaviour
{
	// Start is called before the first frame update
	private CharacterController controller;
	private Vector3 playerVelocity;
	private bool groundedPlayer;
	//private float playerSpeed = 4.0f;
	public NavMeshAgent agent;
	public GameObject tileManagerObject;
	public Camera cam;
	public Dictionary<Vector2Int, int> selfVisitLog;
	public ThirdPersonCharacter character;
	public string playerID;
	public int targetID = -1;
	public GameObject itemMan;
	
	Vector2Int destination;
	Vector3 move;
	void Start()
	{
		controller = gameObject.AddComponent<CharacterController>();
		agent = gameObject.GetComponent<NavMeshAgent>();
		tileManagerObject = GameObject.Find("tileManager");
		selfVisitLog = new Dictionary<Vector2Int, int>();
		GameObject[] tiles = GameObject.FindGameObjectsWithTag("tile");
		Vector3 tilePos;
		foreach (GameObject tile in tiles)
		{
			tilePos = tile.transform.position;
			Vector2Int blockID = new Vector2Int ((int)tilePos[0], (int)tilePos[2]);
			selfVisitLog.Add(blockID, 0);
		}
		//agent.updateRotation = false;
	}
	void output()
	{

	}


	// Update is called once per frame
	void Update()
	{
		/*
		move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		//controller.Move(move * Time.deltaTime * playerSpeed);
		if (move != Vector3.zero)
		{
			gameObject.transform.forward = move;
		}
		*/
		if (Input.GetMouseButtonDown(0)) 
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				agent.SetDestination(hit.point);
				Debug.Log("destination" + hit.point);
				Debug.Log("cur:" + gameObject.transform.position);
			}
		}

		if (agent.remainingDistance > agent.stoppingDistance)
		{
			character.Move(agent.desiredVelocity, false, false);
		}
		else
		{
			character.Move(Vector3.zero, false, false);
			grabItem(targetID);
		}

	}	

	public void grabItem(int itemID)
	{
		if (targetID == -1) return;
		itemMan.GetComponent<itemManager>().takeItem(gameObject, targetID);
	}

	public void updateLog(Vector3 curPos)
	{
		Vector2Int blockID = new Vector2Int ((int)curPos[0], (int)curPos[2]);
		selfVisitLog[blockID] = selfVisitLog[blockID] + 1;
	}

	public bool moveNextA()
	{
		List<Vector2Int> unvisited = new List<Vector2Int>();
		foreach(var item in selfVisitLog)
		{
			if (item.Value == 0) 
			{
				unvisited.Add(item.Key);
			}
		}
		if (unvisited.Count == 0)
		{
			Debug.Log("No unvisited tiles");

			return true;
		}
		destination = unvisited[Random.Range (0, unvisited.Count)];
		Debug.Log("Destination:" + destination);
		agent.SetDestination(new Vector3(destination[0]+0.5f, 2, destination[1]-0.5f));
		return false;
	}
	public bool moveNextB()
	//Function is called to give the agent a new destination to visit. Does not wait until reaching destination.
	{
		List<Vector2Int> unvisited = new List<Vector2Int>();
		Dictionary<Vector2Int, int> visitLog = tileManagerObject.GetComponent<tileManager>().visitLog;
		foreach(var item in visitLog)
		{
			if (item.Value == 0) 
			{
				unvisited.Add(item.Key);
			}
		}
		if (unvisited.Count == 0)
		{
			Debug.Log("No unvisited tiles");
			gameObject.GetComponent<Rigidbody>().isKinematic = true;
			return true;
		}
		destination = unvisited[Random.Range (0, unvisited.Count)];
		Debug.Log("Destination:" + destination);
		agent.SetDestination(new Vector3(destination[0]+0.5f, 2, destination[1]-0.5f));
		return false;
	}
	public bool moveNextC()
	//Function is called to give agent new destination to visit, including if can grab an item.
	{
		return false;
	}
	
}
