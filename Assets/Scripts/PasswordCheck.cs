using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordCheck : MonoBehaviour
{
    // Start is called before the first frame update
    public InputField passwordField;
    private string password = "Para sa Katipunan";
    public GameObject nextScreen;

    void Start()
    {
        passwordField.text = "";
    }
    public void CheckPasswordIfCorrect()
    {
        if (passwordField.text == password)
        {
            nextScreen.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
