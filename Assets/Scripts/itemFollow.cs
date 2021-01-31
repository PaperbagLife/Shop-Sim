using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public bool following = false;
    public bool started = false;
    public bool ended = false;
    public float moveSpeed = 0.05f;
    Vector3 playerPos;
    Vector3 offSet = new Vector3 (0f, 0.3f, 0f);
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Move gameobject based on a bool towards transform of player.
        //After condition been met, destroy the game object.
        
        if (following && !started)
        {
        	StartCoroutine(delayedFollow());
        }
        if (started && !ended)
        {
            StartCoroutine(delayedFollow2());
            playerPos = player.transform.position + offSet;
            transform.position += moveSpeed * (playerPos - transform.position).normalized;
        }
        if (ended)
        {
            playerPos = player.transform.position + offSet;
            transform.position = playerPos;

        }
    }

    private IEnumerator delayedFollow()
    {
        yield return new WaitForSeconds(0.5f);
        started = true;

    }
    private IEnumerator delayedFollow2()
    {
        yield return new WaitForSeconds(0.1f);
        ended = true;

    }

    public void follow(GameObject playerToFollow)
    {
    	player = playerToFollow;
    	following = true;
    }
}
