using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
    /*  Player Position Variables */
    private Transform playerTransform;
    private SpriteRenderer playerRenderer;
    private Vector3 playerExtents;
    private Vector3 position;
    private float beforeJumpY;
    /*  Player Position Variables */
    /*     Player State Variables */
    private bool overSomething;
    private bool jumping;
    private bool decending;
    /*     Player State Variables */
    /*     Speed/Max Variables    */
    public float fallSpeed;
    public float moveSpeed;
    public float lowestYPoint;
    public float jumpSpeed;
    public float jumpMaxHeight;
    private float timeJumpPress;
    private float jumpForce;
    /*     Speed/Max Variables    */
    /*     World Variables        */
    private float yOfWorld;
    private GameObject[] worldObjects;
    /*     World Variables        */
    void Awake ()
    {
        worldObjects = GameObject.FindGameObjectsWithTag("World");
        playerTransform = GetComponent<Transform>();
        playerRenderer = GetComponent<SpriteRenderer>();
        fallSpeed = 6.85f;
        moveSpeed = 6.0f;
        jumpSpeed = 8f;
        jumpMaxHeight = 10f;
        lowestYPoint = -25f;
        jumpForce = 1f;
    }
	// Use this for initialization
	void Start () {        
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
            position.x -= moveSpeed * Time.fixedDeltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            position.x += moveSpeed * Time.fixedDeltaTime; 
        }
        if (Input.GetKey(KeyCode.W))
        {
            position.y += moveSpeed * Time.fixedDeltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space) && jumping == false && decending == false)
        {
            beforeJumpY = position.y;
            jumping = true;
            Debug.Log("beforeJumpY " + beforeJumpY);
            Debug.Log("Max Y from Jump: " + (beforeJumpY + jumpMaxHeight));
        }
        if (Input.GetKeyUp(KeyCode.Space) || (position.y >= (beforeJumpY + (jumpMaxHeight/jumpForce)) && jumping == true))
        {
            jumping = false;
            decending = true;
            Debug.Log("Space Key Up");
        }        
        if (jumping == true)
        {
            Jump();
        }
        if (position.y < lowestYPoint)
        {
            Debug.Log("You Died");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        float currentYofWorld = 0f;        
        foreach (GameObject world in worldObjects)
        {
            if (position.x - playerExtents.x < world.transform.position.x + world.GetComponent<SpriteRenderer>().bounds.extents.x && position.x + playerExtents.x > world.transform.position.x - world.GetComponent<SpriteRenderer>().bounds.extents.x && position.y - playerExtents.y >= world.transform.position.y + world.GetComponent<SpriteRenderer>().bounds.extents.y)
            {
                if(world.GetComponent<SpriteRenderer>().bounds.extents.y + world.transform.position.y > currentYofWorld || currentYofWorld == 0f)
                {
                    currentYofWorld += world.GetComponent<SpriteRenderer>().bounds.extents.y + world.transform.position.y;
                }
                Debug.Log("I'm over something");
                overSomething = true;                             
            }
        }
        if (jumping == false)
        {
            Fall(currentYofWorld);
        }
        Debug.Log("jumping Bool Value: " + jumping);
        Debug.Log("decending Bool Value " + decending); 
	}

    void Fall(float worldY)
    {
        Debug.Log("WorldY Value: " + worldY.ToString());
        if (position.y - playerExtents.y != worldY || overSomething == false)
        {
            position.y -= fallSpeed * Time.fixedDeltaTime;
            decending = true;
        }            
        if (overSomething == true)
        {
            if (position.y - playerExtents.y < worldY)
            {
                position.y = worldY + playerExtents.y;
                decending = false;
                Debug.Log("Decend Condition Met False");
            }
            /*
            if ((position.y - playerExtents.y) - fallSpeed < worldY)
            {
                position.y = playerExtents.y + worldY;
            }
            else
            {
                position.y -= fallSpeed * Time.fixedDeltaTime;
            }
            */
        }
    }

    void Jump()
    {  
        position.y += jumpSpeed * Time.fixedDeltaTime;
    }
}
