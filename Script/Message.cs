using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    public GameObject messagePrefabs;

    private static Message instance;
    public static Message Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        
    }

    public void ShowMessage(string message)
    {
        GameObject go = GameObject.Instantiate(messagePrefabs);
        go.transform.position = Vector3.zero;
        go.transform.SetParent(transform);
        Text messageText = go.GetComponent<Text>();
        Animator animator = go.GetComponent<Animator>();

        messageText.text = message;
        
        animator.SetTrigger("ShowMessage");

        GameObject.Destroy(go, 1.6f);

    }
}
