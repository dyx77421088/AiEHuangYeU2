using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Register : MonoBehaviour
{
    public InputField username;
    public InputField password;
    public InputField rePassword; 

    private RegisterRequest request;

    private void Start()
    {
        request = GetComponent<RegisterRequest>();
    }
    public void ToLogin()
    {
        gameObject.SetActive(false);
    }

    public void Registers()
    {
        request.username = username.text;
        request.password = password.text;
        request.DefaultRequest();
    }
}
