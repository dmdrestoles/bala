using UnityEngine;

public class QuestArea : MonoBehaviour
{
    public GameObject questMarker;
    public GameObject minimapHighlight;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            minimapHighlight.SetActive(true);
            questMarker.SetActive(false);
            Debug.Log("Player inside Search Area");
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            minimapHighlight.SetActive(true);
            questMarker.SetActive(false);
            Debug.Log("Player still inside of Search Area");
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            minimapHighlight.SetActive(false);
            questMarker.SetActive(true);
            Debug.Log("Player went out");
        }
    }
}
