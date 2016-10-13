using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class Utility
    {
        public Player playerScript;        
        public Vector3 playerPosition;
        private Transform playerTransform;
        private SpriteRenderer playerRender;
        private Vector3 playerExtents;
        private Transform thisPrivateTransform;
        private Vector3 thisObjectPostion;
        private SpriteRenderer thisRenderer;
        private Vector3 thisObjectExtent;
        private float maxRight;
        private float maxLeft;
        private float lowestY;
        private float highestY;
        private float height;
        private float aSide;
        private float differenceX;
        private float AAngle;
        private float CAngle;
        private float sizeX;
        private float currentZ;
        private Vector3 angle;

        public Utility (ref GameObject playerObject, ref Transform thisTransform, ref SpriteRenderer thisRenderer)
        {
            playerTransform = playerObject.GetComponent<Transform>();
            playerScript = playerObject.GetComponent<Player>();
            playerPosition = playerTransform.position;
            playerRender = playerObject.GetComponent<SpriteRenderer>();
            playerExtents = playerRender.bounds.extents;
            thisObjectPostion = thisTransform.position;
            thisObjectExtent = thisRenderer.bounds.extents;
            currentZ = thisTransform.rotation.eulerAngles.z;
            angle = new Vector3(0, 0, -currentZ);
            thisTransform.Rotate(angle);
            thisObjectExtent = thisRenderer.bounds.extents;
            sizeX = thisObjectExtent.x + thisObjectExtent.x;
            angle = new Vector3(0, 0, currentZ);
            thisTransform.Rotate(angle);
            thisPrivateTransform = thisTransform;
            thisObjectExtent = thisRenderer.bounds.extents;
            lowestY = thisObjectPostion.y - thisObjectExtent.y;
            highestY = thisObjectPostion.y + thisObjectExtent.y;
        }

        public void IsPlayerOverMe()
        {
            if (playerPosition.x - playerExtents.x <= maxRight && playerPosition.x + playerExtents.x >= maxLeft && playerPosition.y /*- playerExtents.y*/ > lowestY + aSide/* && isJumping == false*/)
            {
                Debug.Log("Over Me");
                playerScript.overSomething = true;
                if (thisPrivateTransform.rotation.eulerAngles.z != 0 || thisPrivateTransform.rotation.eulerAngles.z != 180)
                {
                    CalcAngle();
                }
                playerScript.worldYPositions.Add(new Player.platformSettngs() { platformY = (lowestY + height), platformRotation = thisPrivateTransform.rotation });
            }
        }

        void CalcAngle()  // This method returns the Y position based on the angle of the platform
        {
            if (thisPrivateTransform.rotation.eulerAngles.z < 180)
            {
                differenceX = playerPosition.x - maxLeft; //sets the bottom line length of the Triangle for when the 90 degree angle is on the left
                AAngle = thisPrivateTransform.rotation.eulerAngles.z;
                CAngle = 180 - (AAngle + 90);
                aSide = (differenceX * Mathf.Sin(AAngle * Mathf.Deg2Rad)) / Mathf.Sin(CAngle * Mathf.Deg2Rad); //basic formula for getting the height of a triangle. The Mathf.Sin is expecting Radians, so multiplying it by Mathf.Deg2Rad converts the degress to Radians.
                height = aSide + (playerExtents.y / 2);
            }
            if (thisPrivateTransform.rotation.eulerAngles.z > 180)
            {
                differenceX = (playerPosition.x - maxRight) * -1f; //sets teh bottom line length of the Triangle for when the 90 degree angle is on the left
                AAngle = 360 - thisPrivateTransform.rotation.eulerAngles.z; //Subtracts from 360 to get degree of angle Ex: 360 - 335 = 25 degrees
                CAngle = 180 - (AAngle + 90);
                aSide = (differenceX * Mathf.Sin(AAngle * Mathf.Deg2Rad)) / Mathf.Sin(CAngle * Mathf.Deg2Rad); //basic formula for getting the height of a triangle. The Mathf.Sin is expecting Radians, so multiplying it by Mathf.Deg2Rad converts the degress to Radians.
                height = aSide + (playerExtents.y / 2);
            }
        }

        public void SetSides()
        {
            float xWidth = Mathf.Sqrt((sizeX * sizeX) - ((thisObjectExtent.y + thisObjectExtent.y) * (thisObjectExtent.y + thisObjectExtent.y)));
            if (thisPrivateTransform.rotation.eulerAngles.z == 0 || thisPrivateTransform.rotation.eulerAngles.z == 180) //If the is no angle on the platform
            {
                maxLeft = thisObjectPostion.x - thisObjectExtent.x;
                maxRight = thisObjectPostion.x + thisObjectExtent.x;
                height = (thisObjectExtent.y + thisObjectExtent.y) / 2;
            }
            if (thisPrivateTransform.rotation.eulerAngles.z < 180)
            {
                maxLeft = thisObjectPostion.x - thisObjectExtent.x;
                maxRight = maxLeft + xWidth;
            }
            else if (thisPrivateTransform.rotation.eulerAngles.z > 180)
            {
                maxRight = thisObjectPostion.x + thisObjectExtent.x;
                maxLeft = maxRight - xWidth;
            }
        }
    }
}
