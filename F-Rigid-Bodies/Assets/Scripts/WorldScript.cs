using UnityEngine;
using System.Collections;

public class WorldScript : MonoBehaviour {
    Vector3 playerPosition;
    Vector3 playerExtents;
    GameObject player;
    Vector3 thisObjectPostion;
    Vector3 thisObjectExtent;
    bool isJumping;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPosition = player.GetComponent<Player>().position;
        playerExtents = player.GetComponent<SpriteRenderer>().bounds.extents;
        thisObjectPostion = GetComponent<Transform>().position;
        thisObjectExtent = GetComponent<SpriteRenderer>().bounds.extents;
    }
	
	// Update is called once per frame
	void Update () {
        playerPosition = player.GetComponent<Player>().position;
        isJumping = player.GetComponent<Player>().jumping;
        if (playerPosition.x - playerExtents.x <= thisObjectPostion.x + thisObjectExtent.x && playerPosition.x + playerExtents.x >= thisObjectPostion.x - thisObjectExtent.x && playerPosition.y - playerExtents.y > thisObjectPostion.y && isJumping == false)
        {
            player.GetComponent<Player>().overSomething = true;
            player.GetComponent<Player>().worldYPositions.Add(thisObjectPostion.y + thisObjectExtent.y);
            Debug.Log("OverMe");            
        } 
    }
}