using UnityEngine;

public class EndTrigger : MonoBehaviour
{

    public GameManager gameManager;
    public int level;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if ( level == 1 )
            {
                TutorialCutScene.reachedLastMarker = true;
                gameManager.CompleteLevelOne();
            }

            if( level == 2 )
            {
                gameManager.CompleteLevelTwo();
            }

            if ( level == 3 )
            {
                gameManager.CompleteLevelThree();
            }
        }
    }
}
