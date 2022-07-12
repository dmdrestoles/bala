using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObjectStateManager : MonoBehaviour
{
    public PickableObjectBaseState currentState;
    PickableObjectInventory inInventory;
    PickableObjectWorld onWorld;
    public MouseLook mouseLook;
    public string objectName;

    public WeaponManager weaponManager;

    // Start is called before the first frame update
    void Start(){
        gameObject.AddComponent<PickableObjectInventory>();
        gameObject.AddComponent<PickableObjectWorld>();

        inInventory = gameObject.GetComponent<PickableObjectInventory>();
        onWorld = gameObject.GetComponent<PickableObjectWorld>();

        currentState = onWorld;

        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update(){
        currentState.UpdateState(this);
    }

    public virtual void HandleObjectPickup(string objectName){
        if (Input.GetKeyDown(KeyCode.F)){
            Debug.Log("Picked up object: " + objectName);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
    
}
