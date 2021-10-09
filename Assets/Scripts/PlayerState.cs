using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    // Start is called before the first frame update
    public float health = 50f;
    public bool isVisible;
    public Text healthText;

    void Start()
    {
        healthText.text = health.ToString() + "/50";
        isVisible = true;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthText.text = health.ToString() + "/50";
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
