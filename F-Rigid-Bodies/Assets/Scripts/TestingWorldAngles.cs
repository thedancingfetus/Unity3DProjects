using UnityEngine;
using System.Collections;

/*
    This class I have created is mainly used 
    for testing  I attach it to objects and 
    configure a bunch of public varialbes
    to see if I'm getting the output I expect.
*/

public class TestingWorldAngles : MonoBehaviour {

    private Transform myTransform;
    public Vector3 position;
    public Quaternion rotation;
    private Bounds mySpriteBounds;
    public GameObject player;
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public float highestY;
    public float lowestY;
    public float farthestRight;
    public float farthestLeft;
    public float bottomTriWidth;
    public float topAngleDegree;
    public float bottomAngleDegree;
    public float maxYExtents;
    
	// Use this for initialization
    void Start () {
        myTransform = GetComponent<Transform>();
        position = myTransform.position;
        rotation = myTransform.rotation;
        mySpriteBounds = GetComponent<SpriteRenderer>().bounds;
        highestY = mySpriteBounds.extents.y + myTransform.position.y;
        lowestY = myTransform.position.y - mySpriteBounds.extents.y;
        farthestRight = myTransform.position.x + mySpriteBounds.extents.x;
        farthestLeft = myTransform.position.x - mySpriteBounds.extents.x;
        player = GameObject.FindGameObjectWithTag("Player");
        bottomTriWidth = mySpriteBounds.extents.x + mySpriteBounds.extents.x;
        topAngleDegree = rotation.eulerAngles.z;
        bottomAngleDegree = 180 - (topAngleDegree + 90);
        maxYExtents = mySpriteBounds.extents.y + mySpriteBounds.extents.y;
    }
	
	// Update is called once per frame
	void Update () {
        playerPosition = player.transform.position;
    }
}
