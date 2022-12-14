using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivator : MonoBehaviour
{
    public GameObject player;
    public GameObject terrainHolder;
    Transform[] allChildren;
    List<GameObject> childObjects;
    // Start is called before the first frame update
    void Start()
    {
        allChildren = terrainHolder.GetComponentsInChildren<Transform>();
        childObjects = new List<GameObject>();
        
        foreach (Transform child in allChildren)
        { 
            childObjects.Add(child.gameObject);
        }
        
        Debug.Log("Debug: Terrain Children count is: " + childObjects.Count);
    }

    // Update is called once per frame
    void Update()
    {
        DeactivateIfFar();
    }

    void DeactivateIfFar()
    {
        Vector3 playerPos = new Vector3(player.transform.position.x,0,player.transform.position.z);

        foreach (GameObject childGameObject in childObjects)
        {
            Vector3 objectPos = new Vector3(childGameObject.transform.position.x,0,childGameObject.transform.position.z); 
            Debug.Log("Object distance from player:" + Vector3.Distance(player.transform.position, childGameObject.transform.position));
            if (Vector3.Distance(playerPos, objectPos) <= 50)
            {
                childGameObject.GetComponent<GruntStateManager>().enabled = true;
            }
            else
            {
                childGameObject.GetComponent<GruntStateManager>().enabled = false;
            }
            
        }
    }
}
