using UnityEngine;
using System.Collections;
using System;
using Assets.Scripts;

public class WorldScript : MonoBehaviour {
    GameObject player;
    Transform thisTransform;
    SpriteRenderer thisSpriteRenderer;
    Utility myStuff;
 
    void Start () {       
        player = GameObject.FindGameObjectWithTag("Player");
        thisTransform = GetComponent<Transform>();
        thisSpriteRenderer = GetComponent<SpriteRenderer>();
        myStuff = new Utility(ref player, ref thisTransform, ref thisSpriteRenderer);
        myStuff.SetSides();        
    }

    // Update is called once per frame
    void Update ()
    {
        myStuff.playerPosition = myStuff.playerScript.position;
        myStuff.IsPlayerOverMe();
    }
}