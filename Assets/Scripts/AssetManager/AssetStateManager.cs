using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetStateManager : MonoBehaviour
{
    public AssetBaseState currentState;
    public string collisionName = "Module1";
    public InM1State m1State = new InM1State();
    public InM2State m2State = new InM2State();
    public InM3State m3State = new InM3State();
    public InM4State m4State = new InM4State();
    public InM5State m5State = new InM5State();
    public InM6State m6State = new InM6State();
    public InM7State m7State = new InM7State();
    public InH1State h1State = new InH1State();
    public InH2State h2State = new InH2State();
    public InH3State h3State = new InH3State();
    public InH4State h4State = new InH4State();
    public InH5State h5State = new InH5State();
    public InH6State h6State = new InH6State();
    public InH7State h7State = new InH7State();
    public InH8State h8State = new InH8State();
    public InH9State h9State = new InH9State();
    public InH10State h10State = new InH10State();
    public InH11State h11State = new InH11State();

    //Handled GameObjects
    public List<GameObject> modules = new List<GameObject>();
    public List<GameObject> hallways = new List<GameObject>();


    void Start()
    {
        FindModulesAndHallwaysGO();
        currentState = m1State;
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(AssetBaseState state)
    {
        currentState = state;
        Debug.Log("Debug: " + currentState.ToString());
        DeactivateAll();
        currentState.EnterState(this);
    }

    void FindModulesAndHallwaysGO()
    {
        for (int i = 1; i <= 7; i++)
        {
            modules.Add(GameObject.Find("Module" + i));
            modules[i-1].SetActive(false);
            Debug.Log("Debug: " + "Module" + i + " found and added to List");
        }
        for (int i = 1; i <= 11; i++)
        {
            hallways.Add(GameObject.Find("Hallway" + i));
            hallways[i-1].SetActive(false);
            Debug.Log("Debug: " + "Hallway" + i + " found and added to List");
        }
   }

   void DeactivateAll()
   {
        foreach (GameObject module in modules)
        {
            module.SetActive(false);
        }
        foreach (GameObject hallway in hallways)
        {
            hallway.SetActive(false);
        }
   }
}
