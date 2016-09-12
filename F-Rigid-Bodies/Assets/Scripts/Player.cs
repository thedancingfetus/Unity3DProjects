using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
    /*  Player Position Variables */
    private SpriteRenderer playerRenderer;
    private Vector3 playerExtents;
    public Vector3 position;
    private float beforeJumpY;
    /*  Player Position Variables */
    /*     Player State Variables */
    public bool overSomething;
    public bool jumping;
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
    public List<float> worldYPositions = new List<float>(); 
    private float currentWorldY;    
    void Awake ()
    {  
        playerRenderer = GetComponent<SpriteRenderer>();
        fallSpeed = 6.85f;
        moveSpeed = 6.0f;
        jumpSpeed = 15f;
        jumpMaxHeight = 10f;
        lowestYPoint = -25f;
        jumpForce = 1f;
    }
	// Use this for initialization
	void Start () {                
        playerExtents = playerRenderer.bounds.extents;
        position = GetComponent<Transform>().position;  
    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<Transform>().position = position;        
        if(worldYPositions.Count > 0)
        {
            worldYPositions.Sort();
            currentWorldY = worldYPositions.ToArray()[worldYPositions.Count - 1];
        }            
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
        }
        if (Input.GetKeyUp(KeyCode.Space) || (position.y >= (beforeJumpY + (jumpMaxHeight/jumpForce)) && jumping == true))
        {
            jumping = false;
            decending = true;
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
        if (jumping == false)
        {
            Fall(currentWorldY);
        }            
	}

    void Fall(float worldY)
    {
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
            }
            overSomething = false;
        }
        worldYPositions.Clear();
    }
    void Jump()
    {  
        position.y += jumpSpeed * Time.fixedDeltaTime;
    }
}
