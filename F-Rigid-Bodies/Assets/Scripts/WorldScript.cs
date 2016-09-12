using UnityEngine;
using System.Collections;
using System;

public class WorldScript : MonoBehaviour {
    Vector3 playerPosition;
    Vector3 playerExtents;
    GameObject player;
    Vector3 thisObjectPostion;
    Vector3 thisObjectExtent;
    float maxLeft;
    float maxRight;
    public float differenceX;
    public float lowestY;
    public float CAngle;
    public float AAngle;
    public float aSide;
    bool isJumping;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPosition = player.GetComponent<Player>().position;
        playerExtents = player.GetComponent<SpriteRenderer>().bounds.extents;
        thisObjectPostion = GetComponent<Transform>().position;
        thisObjectExtent = GetComponent<SpriteRenderer>().bounds.extents;
        maxLeft = thisObjectPostion.x - thisObjectExtent.x;
        maxRight = thisObjectPostion.x + thisObjectExtent.x;
        lowestY = thisObjectPostion.y - thisObjectExtent.y;
    }
	
	// Update is called once per frame
	void Update () {
        playerPosition = player.GetComponent<Player>().position;
        isJumping = player.GetComponent<Player>().jumping;
        if (playerPosition.x - playerExtents.x <= thisObjectPostion.x + thisObjectExtent.x && playerPosition.x + playerExtents.x >= thisObjectPostion.x - thisObjectExtent.x && playerPosition.y - playerExtents.y > lowestY && isJumping == false)
        {
            player.GetComponent<Player>().overSomething = true;
            if (GetComponent<Transform>().rotation.eulerAngles.z == 0) //If the is no angle on the platform
            {
                player.GetComponent<Player>().worldYPositions.Add(thisObjectPostion.y + thisObjectExtent.y);
            }
            else //If the platform is Tilted
            {
                player.GetComponent<Player>().worldYPositions.Add(lowestY + CalcAngle());
            }           
        } 
    }

    float CalcAngle ()  // This method returns the Y position based on the angle of the platform
    {
        float yHeight;
        if (GetComponent<Transform>().rotation.eulerAngles.z < 180)
        {
            differenceX = playerPosition.x - maxLeft; //sets the bottom line length of the Triangle for when the 90 degree angle is on the left
            AAngle = GetComponent<Transform>().rotation.eulerAngles.z;
            CAngle = 180 - (AAngle + 90);
        }
        else
        {
            differenceX = (playerPosition.x - maxRight) * -1f; //sets teh bottom line length of the Triangle for when the 90 degree angle is on the left
            AAngle = 360 - GetComponent<Transform>().rotation.eulerAngles.z; //Subtracts from 360 to get degree of angle Ex: 360 - 335 = 25 degrees
            CAngle = 180 - (AAngle + 90);
        }
        aSide = (differenceX * Mathf.Sin(AAngle * Mathf.Deg2Rad)) / Mathf.Sin(CAngle * Mathf.Deg2Rad); //basic formula for getting the height of a triangle. The Mathf.Sin is expecting Radians, so multiplying it by Mathf.Deg2Rad converts the degress to Radians.
        yHeight = aSide + thisObjectExtent.y;  // Currently just adding the Y extents of the sprite.  Soon I'll by rotating the player and sending the extents based on the angle.
        return yHeight;      
    }
}