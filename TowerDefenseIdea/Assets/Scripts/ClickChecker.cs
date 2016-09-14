using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClickChecker : MonoBehaviour {
    public struct clickObject
    {
        public float distance;
        public TowerScript thing;
    }
    private Vector3 mousePostion;
    public List<clickObject> clickedObject;
	// Use this for initialization
	void Start () {
        clickedObject = new List<clickObject>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (clickedObject.Count > 0)
        {
            Debug.Log(clickedObject.Count);
            clickedObject.Sort((obj1, obj2) => obj1.distance.CompareTo(obj2.distance));
            clickedObject.ToArray()[0].thing.GetComponent<TowerScript>().clickedOnMe = true;
        }
        clickedObject.Clear();
    }
}

