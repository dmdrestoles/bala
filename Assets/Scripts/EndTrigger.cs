﻿using UnityEngine;

public class EndTrigger : MonoBehaviour
{

    public GameManager gameManager;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        { 
            gameManager.CompleteLevel();
        }
    }
}