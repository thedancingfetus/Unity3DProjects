using UnityEngine;
using UnityEditor;
using System.Collections;

public class TowerScript : MonoBehaviour {
    public Vector3 mousePostion;
    public Vector3 towerPostion;
    public Quaternion towerRotation;
    public Vector3 towerExtents;
    public Transform towerTransform;
    public float towerPostionY;
    public float towerPostionZ;
    public bool clickedOnMe;
    private bool updateList;
    private bool createNewTower;
    public PrefabUtility prefab;
    public ClickChecker controllerObject;
    public GameObject newTower;
    public float newTowerZ;
    // Use this for initialization
    void Start () {
        towerTransform = GetComponent<Transform>();
        towerPostion = towerTransform.position;
        towerRotation = towerTransform.rotation;
        towerPostion.z = towerPostion.y / 100; //Set Z position based on Y position
        towerTransform.position = towerPostion;
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
        if (Input.GetMouseButton(1) && newTower != null)
        {
            Debug.Log("newTower not null : " + newTower.name);
            newTowerZ = mousePostion.y / 100;
            newTower.GetComponent<TowerScript>().towerPostion = new Vector3(mousePostion.x, mousePostion.y, newTowerZ);
        }
        if (Input.GetMouseButtonUp(1))
        {
            newTower = null;
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
            CreateNewTower();
            createNewTower = false;
        }
    }

    void CreateNewTower()
    {
        int count = GameObject.FindGameObjectsWithTag("Towers").Length;
        newTower = GameObject.Instantiate(PrefabUtility.GetPrefabParent(gameObject), mousePostion, towerRotation) as GameObject;
        newTower.name = "TowerPrefab" + (count + 1).ToString();
        newTower.tag = "Towers";
        newTower.transform.parent = towerTransform.parent;
    }
}
