using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.IO;

public class main : MonoBehaviour
{
	// Start is called before the first frame update
	public int N = 0;
	public List<GameObject> players = new List<GameObject>();
	public GameObject tileManager;
	public bool modeA = false;
	public bool modeB = false;
	public bool modeC = false;
	private NavMeshAgent agent;
	private string path;
	private string report;
	private bool done; 

	void Start()
	{
		N = transform.childCount;
		for (int i = 0; i < N; i++)
		{
			players.Add(gameObject.transform.GetChild(i).gameObject);
		}
		path = Application.dataPath + "/Log/output.txt";
		report = "Grid count: " + tileManager.transform.childCount + "\n";
		done = false;


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

	bool assignNewDestC(GameObject player)
	{
		return player.GetComponent<playerMovement>().moveNextC();
	}

	void output()
	{
		//Create a file and add text
		//Think about printing the entirety of the log later
		if (done) return;
		File.AppendAllText(path, "Terminated\n Overall Summary\n");
		
		Dictionary<Vector2Int, int> log = tileManager.GetComponent<tileManager>().visitLog;
		foreach(var item in log)
		{
			File.AppendAllText(path, "BlockID: " + item.Key + ", " + "Visited:" + item.Value + " times\n");
		} 
		File.AppendAllText(path, "Summary for each player:\n");
		foreach (GameObject player in players)
		{
			File.AppendAllText(path, "Player " + player.GetComponent<playerMovement>().playerID + ":\n");
			Dictionary<Vector2Int, int> curLog = player.GetComponent<playerMovement>().selfVisitLog;
			foreach(var item in curLog)
			{
				File.AppendAllText(path, "BlockID: " + item.Key + ", " + "Visited:" + item.Value + " times\n");
			}
		}
		done = true;
		return;
	}



	// Update is called once per frame
	void Update()
	{
		bool finishedAssign = true;
		bool finishedMoving = true;
		

		if (Input.GetKeyDown("a") && !modeA && !modeB && !modeC)
		{
			Debug.Log("Type A: All players visit all tiles");
			modeA = true;
			// We use a dictionary for each playerMovement script, each player gameobject.
			File.WriteAllText(path, report);
			File.AppendAllText(path, "ModeA, all players visit all tiles\n");
		}
		if (Input.GetKeyDown("b") && !modeA && !modeB && !modeC)
		{
			Debug.Log("Type B: players collectively visit all tiles");
			modeB = true;
			// here we use dictionary in tileManager.
			File.WriteAllText(path, report);
			File.AppendAllText(path, "ModeB, collectively visit all tiles\n");
		}

		if (Input.GetKeyDown("c") && !modeA && !modeB && !modeC)
		{
			Debug.Log("Type C: grabbing all items on the wall");
			modeC = true;
			foreach(GameObject player in players)
			{
				player.GetComponent<playerMovement>().modeC = true;
			}
			// here we use dictionary in tileManager.
			File.WriteAllText(path, report);
			File.AppendAllText(path, "ModeC, grabbing items\n");
		}
		if (!modeA && !modeB && !modeC)
		{
			return;
		}
		foreach (GameObject player in players)
		{
			agent = player.GetComponent<NavMeshAgent>();
			if (agent.remainingDistance <= agent.stoppingDistance && player.GetComponent<playerMovement>().finishedInteracting)
			//Finished moving
			{
				Debug.Log("finsihed moving, from playerManager");
				if(modeA)
				{
					finishedAssign = assignNewDestA(player) && finishedAssign;
				}
				if(modeB)
				{
					finishedAssign = assignNewDestB(player) && finishedAssign;
				}
				if(modeC)
				{
					finishedAssign = assignNewDestC(player) && finishedAssign;
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
			output();
		}



	}


	


}
