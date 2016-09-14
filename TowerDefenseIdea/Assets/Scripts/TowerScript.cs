using UnityEngine;
using UnityEditor;
using System.Collections;

public class TowerScript : MonoBehaviour {
    public Vector3 mousePostion;
    public Vector3 towerPostion;
    public Vector3 towerExtents;
    public float towerPostionY;
    public float towerPostionZ;
    public bool clickedOnMe;
    private bool updateList;
    private bool createNewTower;
    public PrefabUtility prefab;
    public ClickChecker controllerObject;
	// Use this for initialization
	void Start () {
        towerPostion = GetComponent<Transform>().position;
        towerPostion.z = towerPostion.y / 100;
        GetComponent<Transform>().position = towerPostion;
        towerExtents = GetComponent<SpriteRenderer>().bounds.extents;
        controllerObject = GameObject.Find("ControllerObject").GetComponent<ClickChecker>();
        clickedOnMe = false;
        updateList = false;
        createNewTower = false;
    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<Transform>().position = towerPostion;        
        mousePostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) && mousePostion.x > towerPostion.x - towerExtents.x && mousePostion.x < towerPostion.x + towerExtents.x && mousePostion.y > towerPostion.y - towerExtents.y && mousePostion.y < towerPostion.y + towerExtents.y)
        {
            updateList = true;
        }
        if (Input.GetMouseButton(0) && clickedOnMe == true)
        {
            Debug.Log(GetComponent<Transform>().localScale.normalized);
            towerPostionZ = mousePostion.y / 100;
            towerPostion = new Vector3(mousePostion.x, mousePostion.y, towerPostionZ);
        }
        if (Input.GetMouseButtonUp(0))
        {
            clickedOnMe = false;
        }
        if (Input.GetMouseButtonDown(1) && mousePostion.x > towerPostion.x - towerExtents.x && mousePostion.x < towerPostion.x + towerExtents.x && mousePostion.y > towerPostion.y - towerExtents.y && mousePostion.y < towerPostion.y + towerExtents.y)
        {
            createNewTower = true;
        }
    }

    void FixedUpdate ()
    {
        if (updateList == true)
        {
            controllerObject.clickedObject.Add(new ClickChecker.clickObject() { distance = Vector3.Distance(mousePostion, towerPostion), thing = gameObject });
            updateList = false;
        } 
        if (createNewTower == true)
        {
            PrefabUtility.InstantiatePrefab(PrefabUtility.GetPrefabParent(gameObject));
            createNewTower = false;
        }
    }
}
