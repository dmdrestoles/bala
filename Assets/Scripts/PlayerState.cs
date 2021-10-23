using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxHealth = 100f;
    public bool isVisible;
    public Text healthText;
    public GameManager gameManager;
    public Image damageScreen;
    private Color alphaColor;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        isVisible = true;
        alphaColor = damageScreen.color;
    }

    void Update()
    {
        FallOutOfWorld();
    }

    void FallOutOfWorld()
    {
        if (gameObject.transform.position.y < - 5f)
        {
            Die();
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        alphaColor.a += .20f;
        damageScreen.color = alphaColor;
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    public void Heal()
    {
        if (currentHealth < maxHealth)
        {
            alphaColor.a -= .20f;
            damageScreen.color = alphaColor;
            currentHealth += 1;
        }
    }

    void Die()
    {
        gameManager.EndGame();
    }
}
