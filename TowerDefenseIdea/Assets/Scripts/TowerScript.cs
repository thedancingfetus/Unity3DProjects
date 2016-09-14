using UnityEngine;
using System.Collections;

public class TowerScript : MonoBehaviour {
    public Vector3 mousePostion;
    public Vector3 towerPostion;
    public Vector3 towerExtents;
    public float towerPostionY;
    public float towerPostionZ;
    public bool clickedOnMe;
	// Use this for initialization
	void Start () {
        towerPostion = GetComponent<Transform>().position;
        towerPostion.z = towerPostion.y / 100;
        GetComponent<Transform>().position = towerPostion;
        towerExtents = GetComponent<SpriteRenderer>().bounds.extents;
        clickedOnMe = false;
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Transform>().position = towerPostion;
        mousePostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetMouseButtonDown(0) && mousePostion.x > towerPostion.x - towerExtents.x && mousePostion.x < towerPostion.x + towerExtents.x && mousePostion.y > towerPostion.y - towerExtents.y && mousePostion.y < towerPostion.y + towerExtents.y)
        {
            GameObject.Find("ControllerObject").GetComponent<ClickChecker>().clickedObject.Add(new ClickChecker.clickObject() { distance = Vector3.Distance(mousePostion ,towerPostion) , thing = GetComponent<TowerScript>() });        
        }
        if (Input.GetMouseButton(0) && clickedOnMe == true)
        {
            towerPostion.z = mousePostion.y / 100;
            towerPostion = new Vector3(mousePostion.x, mousePostion.y, towerPostion.z);
        }
        if (Input.GetMouseButtonUp(0))
        {
            clickedOnMe = false;
        }
    }
}
