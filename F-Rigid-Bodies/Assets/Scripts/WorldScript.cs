using UnityEngine;
using System.Collections;
using System;

public class WorldScript : MonoBehaviour {
    // Player Vars
    public Vector3 playerPosition;
    Player playerScript;
    SpriteRenderer playerRenderer;
    Vector3 playerExtents;
    GameObject player;
    bool isJumping;
    // End Player Vars
    // Platform Vars
    Transform thisTransform;
    Vector3 thisObjectPostion;
    SpriteRenderer thisSpriteRenderer;
    Vector3 thisObjectExtent;
    // End Platform Vars
    // used to rotate Z to 0 to get true width and height of sprite, then rotate back
    Vector3 angle;
    float currentZ;
    float sizeX;  // Width of sprite when Z rotated to 0
    // End  
    // if platform is rotated these are used to calculate where the player should land on the slope. They are set by CalcAngle()  
    float differenceX;    
    float CAngle;
    float AAngle;
    float aSide;
    float bSide;
    // End    
    // If player falls between these, platform sends player Y position to land. Set by SetSides()
    public float maxLeft;
    public float maxRight;
    // End
    float lowestY;  // the lowest postions of the sprite
    float highestY; // the hightes position of the sprite
    float height; // the Y position the platform will send the player.
    // Use this for initialization
    void Start () {
        // Player Varables
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        playerPosition = playerScript.position;
        playerRenderer = player.GetComponent<SpriteRenderer>();
        playerExtents = playerRenderer.bounds.extents;
        // Player Variables

        // Platform Variables
        thisTransform = GetComponent<Transform>();
        thisSpriteRenderer = GetComponent<SpriteRenderer>();
        thisObjectPostion = thisTransform.position;
        currentZ = thisTransform.rotation.eulerAngles.z;
        angle = new Vector3(0, 0, -currentZ);
        thisTransform.Rotate(angle);
        thisObjectExtent = thisSpriteRenderer.bounds.extents;
        sizeX = thisObjectExtent.x + thisObjectExtent.x;
        angle = new Vector3(0, 0, currentZ);
        thisTransform.Rotate(angle);
        thisObjectExtent = thisSpriteRenderer.bounds.extents;        
        lowestY = thisObjectPostion.y - thisObjectExtent.y;
        highestY = thisObjectPostion.y + thisObjectExtent.y;
        // Player Variables

        // Set Max Left and Right
        SetSides();
        height = CalcAngle();
    }
	
	// Update is called once per frame
	void Update ()
    {
        playerPosition = playerScript.position;
        isJumping = playerScript.jumping;
        if (playerPosition.x - playerExtents.x <= maxRight && playerPosition.x + playerExtents.x >= maxLeft && playerPosition.y /*- playerExtents.y*/ > lowestY + aSide && isJumping == false)
        {
            height = CalcAngle();
            playerScript.overSomething = true;
            playerScript.worldYPositions.Add(new Player.platformSettngs() { platformY = (lowestY + height), platformRotation = thisTransform.rotation });
        }
    }

    float CalcAngle ()  // This method returns the Y position based on the angle of the platform
    {
        float yHeight = 0f;
        if (thisTransform.rotation.eulerAngles.z == 0 || thisTransform.rotation.eulerAngles.z == 180) //If the is no angle on the platform
        {
            maxLeft = thisObjectPostion.x - thisObjectExtent.x;
            maxRight = thisObjectPostion.x + thisObjectExtent.x;
            yHeight = (thisObjectExtent.y + thisObjectExtent.y);
        }
        if (thisTransform.rotation.eulerAngles.z < 180)
        {
            differenceX = playerPosition.x - maxLeft; //sets the bottom line length of the Triangle for when the 90 degree angle is on the left
            AAngle = thisTransform.rotation.eulerAngles.z;
            CAngle = 180 - (AAngle + 90);
            aSide = (differenceX * Mathf.Sin(AAngle * Mathf.Deg2Rad)) / Mathf.Sin(CAngle * Mathf.Deg2Rad); //basic formula for getting the height of a triangle. The Mathf.Sin is expecting Radians, so multiplying it by Mathf.Deg2Rad converts the degress to Radians.
            yHeight = aSide + playerExtents.y;
        }
        else if (thisTransform.rotation.eulerAngles.z > 180)
        {
            differenceX = (playerPosition.x - maxRight) * -1f; //sets teh bottom line length of the Triangle for when the 90 degree angle is on the left
            AAngle = 360 - thisTransform.rotation.eulerAngles.z; //Subtracts from 360 to get degree of angle Ex: 360 - 335 = 25 degrees
            CAngle = 180 - (AAngle + 90);
            aSide = (differenceX * Mathf.Sin(AAngle * Mathf.Deg2Rad)) / Mathf.Sin(CAngle * Mathf.Deg2Rad); //basic formula for getting the height of a triangle. The Mathf.Sin is expecting Radians, so multiplying it by Mathf.Deg2Rad converts the degress to Radians.
            yHeight = aSide + playerExtents.y;
        }        
        return yHeight;      
    }

    void SetSides ()
    {
        float xWidth = Mathf.Sqrt((sizeX * sizeX) - ((thisObjectExtent.y + thisObjectExtent.y) * (thisObjectExtent.y + thisObjectExtent.y)));
        if (thisTransform.rotation.eulerAngles.z == 0 || thisTransform.rotation.eulerAngles.z == 180) //If the is no angle on the platform
        {
            maxLeft = thisObjectPostion.x - thisObjectExtent.x;
            maxRight = thisObjectPostion.x + thisObjectExtent.x;
            height = highestY;
        }
        if (thisTransform.rotation.eulerAngles.z < 180)
        {
            maxLeft = thisObjectPostion.x - thisObjectExtent.x;
            maxRight = maxLeft + xWidth;          
        }
        else if (thisTransform.rotation.eulerAngles.z > 180)
        {
            maxRight = thisObjectPostion.x + thisObjectExtent.x;
            maxLeft = maxRight - xWidth;            
        }
    }
}