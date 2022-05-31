using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody, cameraTransform;
    public GameObject armsCamera;
    float xRotation = 0f;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cameraTransform = GetComponent<Transform>();
    }

    void Update()
    {
        if (GameManager.IsInputEnabled == true)
        {
            mouseSensitivity = PlayerPrefs.GetFloat("sense", 50);
            GetComponent<Camera>().fieldOfView = PlayerPrefs.GetFloat("fov",50);
            armsCamera.GetComponent<Camera>().fieldOfView = PlayerPrefs.GetFloat("fov",50);
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }        
    }

    public IEnumerator Recoil(float recoilVal, float recoilIncrement)
    {
        while (Mathf.Abs(xRotation) <= recoilVal)
        {   
            xRotation -= recoilVal * recoilIncrement;
            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            yield return null;
        }
    }
}
