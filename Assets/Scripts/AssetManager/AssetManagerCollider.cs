using UnityEngine;

public class AssetManagerCollider : MonoBehaviour
{
    // Start is called before the first frame update
    AssetStateManager assetStateManager;
    void Start()
    {
        assetStateManager = GameObject.Find("AssetManager").GetComponent<AssetStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
     void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            assetStateManager.collisionName = gameObject.name;
        }
    }
}
