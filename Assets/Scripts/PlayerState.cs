using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    // Start is called before the first frame update
    public float health = 100f;
    public bool isVisible;

    void Start()
    {
        isVisible = true;
    }

    public void TakeDamage(float amount)
    {
        Debug.Log("Player hit! Health remaining: " + health);
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("GameOver");
    }
}
