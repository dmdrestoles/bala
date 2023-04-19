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
    [SerializeField] private float currentHealth;

    public AudioSource playerDamaged;
    public AudioSource playerDied;

    public GameObject mainCamera;
    public GameObject armsCamera;
    private PostProcessProfile mainCameraEffectProfile;
    private PostProcessProfile armsCameraEffectProfile;
    
    private CameraShake cameraShake;

    void Start()
    {
        Cursor.visible = false;
        currentHealth = maxHealth;
        isVisible = true;
        alphaColor = damageScreen.color;
        mainCameraEffectProfile = mainCamera.GetComponent<PostProcessVolume>().sharedProfile;
        armsCameraEffectProfile = armsCamera.GetComponent<PostProcessVolume>().sharedProfile;
        cameraShake = mainCamera.GetComponent<CameraShake>();
        UpdatePostProcessSaturation(0.0f);
    }

    void Update()
    {
        //for demo
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
            StartCoroutine(Heal());
            playerDamaged.Play();
        }
    }

    public IEnumerator Heal()
    {
        while (currentHealth < maxHealth)
        {
            IncrementPostProcessSaturation(3.0f);
            currentHealth += 1;
            yield return new WaitForSeconds(0.5f);
        }
    }

    void Die()
    {
        playerDied.Play();
        gameManager.EndGame();
    }

    void UpdatePostProcessSaturation(float value)
    {
        ColorGrading mainCameraColorGradingLayer, armsCameraColorGradingLayer = null;
        mainCameraEffectProfile.TryGetSettings(out mainCameraColorGradingLayer);
        armsCameraEffectProfile.TryGetSettings(out armsCameraColorGradingLayer);

        mainCameraColorGradingLayer.saturation.value = value;
        armsCameraColorGradingLayer.saturation.value = value;
    }

    void IncrementPostProcessSaturation(float value)
    {
        ColorGrading mainCameraColorGradingLayer, armsCameraColorGradingLayer = null;
        mainCameraEffectProfile.TryGetSettings(out mainCameraColorGradingLayer);
        armsCameraEffectProfile.TryGetSettings(out armsCameraColorGradingLayer);

        if (mainCameraColorGradingLayer.saturation.value < 0.0f)
        {
            mainCameraColorGradingLayer.saturation.value += value;
            armsCameraColorGradingLayer.saturation.value += value;
        }
    }

    IEnumerator UpdateUIOnHit()
    {
        Debug.Log("Player hit, updating UI.");
        alphaColor.a = 0.1f;
        damageScreen.color = alphaColor;
        UpdatePostProcessSaturation(-90.0f);
        yield return new WaitForSeconds(0.07f);
        alphaColor.a = 0.0f;
        damageScreen.color = alphaColor;
    }
}
