using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class playerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 4.0f;
    public NavMeshAgent agent;
    public GameObject tileManagerObject;
    public Dictionary<Vector2Int, int> visitLog;
    public Camera cam;
    
    Vector2Int destination;
    Vector3 move;
    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        tileManagerObject = GameObject.Find("tileManager");
    }

    // Update is called once per frame
    void Update()
    {
    	List<Vector2Int> unvisited = new List<Vector2Int>();
        move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //controller.Move(move * Time.deltaTime * playerSpeed);
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

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


        if (Input.GetKeyDown("n"))
        {
        	visitLog = tileManagerObject.GetComponent<tileManager>().visitLog;
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
			}
			destination = unvisited[Random.Range (0, unvisited.Count)];
			Debug.Log("Destination:" + destination);
			agent.SetDestination(new Vector3(destination[0]+0.5f, 2, destination[1]-0.5f));
        }
    }
}
