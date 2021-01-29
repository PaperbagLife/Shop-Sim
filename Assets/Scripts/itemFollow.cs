using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public bool following = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (following)
        {
        	transform.position = player.transform.position;
        }
    }

    public void follow(GameObject playerToFollow)
    {
    	player = playerToFollow;
    	following = true;
    }
}
