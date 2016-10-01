using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
    /*  Player Position Variables */
    private SpriteRenderer playerRenderer;
    private Vector3 playerExtents;
    private Transform playerTransform;
    public Vector3 position;
    public Quaternion rotation;
    public Animator animator;
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
    //public List<float> worldYPositions = new List<float>();     
    public struct platformSettngs  //Platform Y and Roation
    {
        public float platformY;
        public Quaternion platformRotation;
    }
    public List<platformSettngs> worldYPositions = new List<platformSettngs>(); //List of platformSettings
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
        playerTransform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        position = playerTransform.position;
        rotation = playerTransform.rotation; 
    }
	
	// Update is called once per frame
	void Update () {
        playerTransform.position = position;
        playerTransform.rotation = rotation;     
        if(worldYPositions.Count > 0)
        {
            worldYPositions.Sort((x1, x2) => x1.platformY.CompareTo(x2.platformY));
            currentWorldY = worldYPositions.ToArray()[worldYPositions.Count - 1].platformY;
            if (worldYPositions.ToArray()[worldYPositions.Count - 1].platformRotation.eulerAngles.z == 0)
            {
                rotation = worldYPositions.ToArray()[worldYPositions.Count - 1].platformRotation;
            }            
        }
        if (worldYPositions.Count == 0)
        {
            overSomething = false;
        }            
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            playerRenderer.flipX = true;
            position.x -= moveSpeed * Time.fixedDeltaTime;
            if (jumping == false && decending == false)
            {
                animator.SetBool("running", true);
            }         
        }
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            playerRenderer.flipX = false;
            position.x += moveSpeed * Time.fixedDeltaTime;
            animator.SetBool("running", true);
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
        if (Input.GetKeyUp(KeyCode.Space) || (position.y >= (beforeJumpY + (jumpMaxHeight/jumpForce)) && jumping == true) || decending == true)
        {
            jumping = false;
        }        
        if (jumping == true)
        {
            Jump();
            rotation.eulerAngles = new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y, rotation.eulerAngles.z - rotation.eulerAngles.z);
        }
        if (jumping == true || decending == true)
        {
            animator.SetBool("running", false);
            animator.SetBool("jumping", true);
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
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) || jumping == true /*|| decending == true*/)
        {
            animator.SetBool("running", false);
        }        
        worldYPositions.Clear();
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
            decending = true;
            if (position.y /*- playerExtents.y*/ < worldY + .5f)
            {
                rotation = worldYPositions.ToArray()[worldYPositions.Count - 1].platformRotation;
            }
            if (position.y /*- playerExtents.y*/ < worldY)
            {
                position.y = worldY;// + playerExtents.y; 
                animator.SetBool("jumping", false);
                decending = false;
            }
            overSomething = false;
        }
        //worldYPositions.Clear();
    }
    void Jump()
    {  
        position.y += jumpSpeed * Time.fixedDeltaTime;
    }
}
