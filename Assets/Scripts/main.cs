using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class main : MonoBehaviour
{
	// Start is called before the first frame update
	public int N = 0;
	public List<GameObject> players = new List<GameObject>();
	public GameObject tileManager;
	public bool modeA = false;
	public bool modeB = false;
	private NavMeshAgent agent;
	void Start()
	{
		N = transform.childCount;
		for (int i = 0; i < transform.childCount; i++)
		{
			players.Add(gameObject.transform.GetChild(i).gameObject);
		}
	}
	bool assignNewDestA(GameObject player)
	//return false if successful, return true if no where to visit
	{
		return player.GetComponent<playerMovement>().moveNextA();
	}
	bool assignNewDestB(GameObject player)
	//return false if successful, return true if no where to visit
	{
		//Using the dictionary from tileManager
		return player.GetComponent<playerMovement>().moveNextB();
	}

	// Update is called once per frame
	void Update()
	{
		bool finishedAssign = true;
		bool finishedMoving = true;
		if (Input.GetKeyDown("a") && !modeA && !modeB)
		{
			Debug.Log("Type A: All players visit all tiles");
			modeA = true;
			// We use a dictionary for each playerMovement script, each player gameobject.
		}
		if (Input.GetKeyDown("b") && !modeA && !modeB)
		{
			Debug.Log("Type B: players cumulatively visit all tiles");
			modeB = true;
			// here we use dictionary in tileManager.
		}
		if (!modeA && !modeB)
		{
			return;
		}
		foreach (GameObject player in players)
		{
			agent = player.GetComponent<NavMeshAgent>();
			if (agent.remainingDistance <= agent.stoppingDistance)
			//Finished moving
			{
				finishedMoving = finishedMoving && true;
				Debug.Log("finsihed moving, from playerManager");
				if(modeA)
				{
					// Lets forget about this for now
					finishedAssign = assignNewDestA(player);
				}
				if(modeB)
				{
					finishedAssign = assignNewDestB(player) && finishedAssign;
				}
			}
			else 
			{
				//Some players are still moving
				finishedMoving = false;
			}
		}
		if (finishedAssign && finishedMoving)
		{
			//Terminated, go print results
			Debug.Log("Terminated");
		}

	}

	


}
