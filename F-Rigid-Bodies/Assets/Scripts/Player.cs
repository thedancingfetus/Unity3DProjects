using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {    
    private Transform playerTransform;
    private SpriteRenderer playerRenderer;
    private GameObject[] worldObjects;
    private Vector3 playerExtents;
    public float fallSpeed = 0.2f;
    public float moveSpeed = 0.1f;
    private bool overSomething;
    private Vector3 position;
    void Awake ()
    {
        worldObjects = GameObject.FindGameObjectsWithTag("World");
        playerTransform = GetComponent<Transform>();
        playerRenderer = GetComponent<SpriteRenderer>();
    }
	// Use this for initialization
	void Start () {
        Debug.Log("World Y position " + worldObjects[0].transform.position.y.ToString());
        playerExtents = playerRenderer.bounds.extents;
        position = GetComponent<Transform>().position;
        Debug.Log("Player Transform position" + playerTransform.position.ToString());
        Debug.Log("Y bounds extent: " + playerExtents.y.ToString());  
    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<Transform>().position = position;
        overSomething = false;
        if (Input.GetKey(KeyCode.A))
        {
            position.x -= moveSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            position.x += moveSpeed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            position.y += moveSpeed;
        }
        foreach (GameObject world in worldObjects)
        {
            if (position.x - playerExtents.x < world.transform.position.x + world.GetComponent<SpriteRenderer>().bounds.extents.x && position.x + playerExtents.x > world.transform.position.x - world.GetComponent<SpriteRenderer>().bounds.extents.x)
            {
                Debug.Log("I'm over something");
                overSomething = true;
                Fall(world.GetComponent<SpriteRenderer>().bounds.extents.y, world.transform.position.y);             
            }
        }
        if(overSomething == false)
        {
            Debug.Log("Death You Fall");
        }
	}

    void Fall(float worldYExtent, float worldYPosition)
    {
        if(worldYExtent + worldYPosition < position.y - playerExtents.y)
        {
            if ((position.y - playerExtents.y) - fallSpeed < worldYExtent + worldYPosition)
            {
                position.y = playerExtents.y + worldYExtent + worldYPosition;
            }
            else
            {
                position.y -= fallSpeed;
            }
        }
    }
}
