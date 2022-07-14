using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObjectEasterEgg : PickableObjectStateManager
{
    public override void HandleObjectPickup(string objectName){
        if (Input.GetKeyDown(KeyCode.F)){
            Debug.Log("Picked up object: " + objectName);
            // Add to codex here
            Destroy(gameObject);
        }
    }
}
