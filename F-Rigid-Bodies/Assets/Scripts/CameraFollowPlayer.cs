using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour {
    private Vector3 playerPosition;
    private Vector3 thisCameraPosition;
    public float screenBuffer;
	// Use this for initialization
	void Start () {
        thisCameraPosition = GetComponent<Camera>().transform.position;
        screenBuffer = 15f;
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
	}
	
	// Update is called once per frame
	void Update () {
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        thisCameraPosition.y = playerPosition.y;
        thisCameraPosition.x = playerPosition.x;
        GetComponent<Camera>().transform.position = thisCameraPosition;
    }
}
