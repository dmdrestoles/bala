using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    public GameObject armsCamera;
    public GameObject pickupHand;
    [SerializeField] public static string selectedObject;
    public RaycastHit hitObject;
    float xRotation = 0f;
    private Transform cameraTransform;
    
    
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

        if (Physics.Raycast(transform.position, transform.forward, out hitObject) && hitObject.distance <= 5)
        {
            selectedObject = hitObject.transform.gameObject.name;
            
            if (hitObject.transform.gameObject.GetComponent<PickupStateManager>() != null)
            {
                pickupHand.SetActive(true);
            }
            else
            {
                pickupHand.SetActive(false);
            }
            // Debug.Log(selectedObject+"------"+hitObject.distance);
        }
        else
        {
            pickupHand.SetActive(false);
            selectedObject = "";
        }    
    }

    public string GetSelectedObject(){
        return selectedObject;
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
