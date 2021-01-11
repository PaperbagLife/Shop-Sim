using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visit : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject manager;
    void Start()
    {
        manager = transform.parent.gameObject;
        manager.GetComponent<tileManager>().initDict(transform.position);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Visiting:" + transform.position);
        // Use position to determine the block ID.
        // Use Tuple of (X, Z) as block ID. can use another representation.
        manager.GetComponent<tileManager>().onVisit(transform.position);
        other.gameObject.GetComponent<playerMovement>().updateLog(transform.position);
    }
}
