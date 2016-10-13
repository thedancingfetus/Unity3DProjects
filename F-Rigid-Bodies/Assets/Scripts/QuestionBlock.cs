using UnityEngine;
using System.Collections;

public class QuestionBlock : MonoBehaviour {

    GameObject player;
    Transform playerTransform;
    public Vector3 playerPosition;
    Player script;

    Transform thisTransform;
    public Vector3 scale;
    // Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
        thisTransform = GetComponent<Transform>();
        script = player.GetComponent<Player>();
        scale = new Vector3(.15f, .15f, 0f);
        thisTransform.localScale = scale;
    }
	
	// Update is called once per frame
	void Update () {
        playerPosition = playerTransform.position;

    }
}
