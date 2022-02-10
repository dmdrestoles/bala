using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class PlayerState : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxHealth = 100f;
    public bool isVisible, isCrouching, isSprinting;
    public Text healthText;
    public GameManager gameManager;
    public Image damageScreen;
    private Color alphaColor;
    private float currentHealth;

    public AudioSource playerDamaged;
    public AudioSource playerDied;

    public GameObject mainCamera;
    private PostProcessProfile cameraEFfectProfile;
    private CameraShake cameraShake;

    void Start()
    {
        Cursor.visible = false;
        currentHealth = maxHealth;
        isVisible = true;
        alphaColor = damageScreen.color;
        cameraEFfectProfile = mainCamera.GetComponent<PostProcessVolume>().sharedProfile;
        cameraShake = mainCamera.GetComponent<CameraShake>();
        UpdatePostProcessSaturation(0.0f);
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
        StartCoroutine(cameraShake.Shake(0.05f,0.5f));
        if (currentHealth <= 0f)
        {
            Die();
        }
        else
        {
            StartCoroutine(UpdateUIOnHit());
            playerDamaged.Play();
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
        playerDied.Play();
        gameManager.EndGame();
    }

    void UpdatePostProcessSaturation(float value)
    {
        ColorGrading colorGradingLayer = null;
        cameraEFfectProfile.TryGetSettings(out colorGradingLayer);   
        colorGradingLayer.saturation.value = value;
    }

    IEnumerator UpdateUIOnHit()
    {
        alphaColor.a = 0.1f;
        damageScreen.color = alphaColor;
        UpdatePostProcessSaturation(-40.0f);
        yield return new WaitForSeconds(0.07f);
        alphaColor.a = 0.0f;
        damageScreen.color = alphaColor;
    }
}
