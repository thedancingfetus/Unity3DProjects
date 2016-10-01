using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour {
    private GameObject player;
    private Vector3 playerPosition;
    private Vector3 thisCameraPosition;
    private Transform thisCameraTransform;
    private Player script;
    public Transform playerTransform;
    public Camera thisCamera;
    public float screenBuffer;
    float lowest = -8.495851f;
    float orthHeight;
    float orthWidth;
    float farLeft;
    float farRight;
    float top;
    float bottom;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
        script = player.GetComponent<Player>();
        thisCamera = GetComponent<Camera>();
        thisCameraTransform = thisCamera.transform;
        thisCameraPosition = thisCameraTransform.position;
        orthHeight = 2f * thisCamera.orthographicSize;
        orthWidth = orthHeight * thisCamera.aspect;
        screenBuffer = 15f;
        playerPosition = playerTransform.position;
        ViewLimits();
	}
	
	// Update is called once per frame
	void Update () {
        playerPosition = playerTransform.position;
        ViewLimits();
        if (playerPosition.y >= lowest)
        {
            thisCameraPosition.y = playerPosition.y + 4f;
        }
        if (bottom >= lowest && playerPosition.y > bottom + 3f)
        {
            thisCameraPosition.y += script.moveSpeed * Time.fixedDeltaTime;
        }
        else if (bottom >= lowest && playerPosition.y < bottom + 3f)
        {
            thisCameraPosition.y -= script.moveSpeed * Time.fixedDeltaTime;
        }
        if (playerPosition.x < farLeft + 8f)
        {
            thisCameraPosition.x -= script.moveSpeed * Time.fixedDeltaTime;
        }
        else if (playerPosition.x > farRight - 8f)
        {
            thisCameraPosition.x += script.moveSpeed * Time.fixedDeltaTime;
        }
        thisCameraTransform.position = thisCameraPosition;
    }

    void ViewLimits()
    {
        farLeft = thisCameraPosition.x - (orthWidth / 2);
        farRight = thisCameraPosition.x + (orthWidth / 2);
        top = thisCameraPosition.y + thisCamera.orthographicSize;
        bottom = thisCameraPosition.y - thisCamera.orthographicSize;
    }
}
