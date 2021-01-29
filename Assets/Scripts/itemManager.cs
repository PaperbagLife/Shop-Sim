using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Dictionary<Vector2Int, int> blockID2ItemID;
    public Dictionary<int, GameObject> ID2Item;
    public Dictionary<int, Vector2Int> itemID2blockID;
    public Dictionary<int, bool> itemTaken;
    void Start()
    {
        // We need a dictionary mapping blockID to itemID,
        // and a dictionary mapping itemID to item gameobjects
        blockID2ItemID = new Dictionary<Vector2Int, int>();
        ID2Item = new Dictionary<int, GameObject>();
        itemTaken = new Dictionary<int, bool>(); 
        itemID2blockID = new Dictionary<int, Vector2Int>();
        GameObject cur;
        // Loop through itemID.
        int N = transform.childCount;
        for (int id = 0; id < N; id++)
		{
			cur = transform.GetChild(id).gameObject;
			ID2Item.Add(id, cur);
			int curX = (int)(cur.transform.position.x + 0.5);
			int curZ = Mathf.CeilToInt(cur.transform.position.z);
			blockID2ItemID.Add(new Vector2Int(curX, curZ), id);
			itemID2blockID.Add(id, new Vector2Int(curX, curZ));
			itemTaken.Add(id, false);
			Debug.Log(curX+"," + curZ +","+ id);
		}

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeItem(GameObject player, Vector2Int dest)
    {
    	if(dest == Vector2Int.zero) return;
    	Debug.Log("before itemID" + dest);
    	int itemID = blockID2ItemID[dest];
    	if (itemTaken[itemID]) return;
    	GameObject cur = ID2Item[itemID];
    	cur.GetComponent<itemFollow>().follow(player);
    	itemTaken[itemID] = true;
    }
}
