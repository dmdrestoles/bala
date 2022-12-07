using UnityEngine;

public class EndTrigger : MonoBehaviour
{

    public GameManager gameManager;
    public int level;
    [HideInInspector]
    private float objectiveTime = 3.0f;
    private float timer = 0.0f;
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (timer < objectiveTime)
            {
                timer += Time.deltaTime;
                Debug.Log(timer);
            }
            else if ( level == 1 )
            {
                TutorialCutScene.reachedLastMarker = true;
                gameManager.CompleteLevelOne();
            }

            else if( level == 2 )
            {
                gameManager.CompleteLevelTwo();
            }

            else if ( level == 3 )
            {
                gameManager.CompleteLevelThree();
            }

            else if ( level == 4 )
            {
                gameManager.CompleteLevelFour();
            }
            else if (level == 5)
            {
                gameManager.CompleteLevelFive();
            }
        }
    }
}
