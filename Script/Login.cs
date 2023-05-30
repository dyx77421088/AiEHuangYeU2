using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public InputField userName;
    public InputField password;
    public GameObject registerPanel;

    private LoginRequest request;

    private void Start()
    {
        request = GetComponent<LoginRequest>(); 
    }
    public void Logins()
    {
        request.username = userName.text;
        request.password = password.text;
        request.DefaultRequest();


    }

    public void ToRegister()
    {
        registerPanel.SetActive(true);
    }
}
