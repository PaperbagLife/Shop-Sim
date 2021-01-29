using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class visit : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject manager;
    private string path;
    private bool raised;
    private GameObject colorTile;
    void Start()
    {
        manager = transform.parent.gameObject;
        manager.GetComponent<tileManager>().initDict(transform.position);
        path = Application.dataPath + "/Log/output.txt";
        raised = false;
        colorTile = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    Vector2Int getID(Vector3 curPos)
    {
        Vector2Int blockID = new Vector2Int ((int)curPos[0], (int)curPos[2]);
        return blockID;
    }

    void OnTriggerEnter(Collider other)
    {
        // Debug.Log("Visiting:" + transform.position);
        // Use position to determine the block ID.
        // Use Tuple of (X, Z) as block ID. can use another representation.
        manager.GetComponent<tileManager>().onVisit(transform.position);
        string visitString = "Player " + other.gameObject.GetComponent<playerMovement>().playerID + " visited " + getID(transform.position) + "\n";
        File.AppendAllText(path, visitString);
        other.gameObject.GetComponent<playerMovement>().updateLog(transform.position);
        if (!raised) colorTile.transform.position += Vector3.up * 0.31f;
        raised = true;

        colorTile.GetComponent<Renderer>().material.color += new Color(0.1f, 0, 0);

    }
}
