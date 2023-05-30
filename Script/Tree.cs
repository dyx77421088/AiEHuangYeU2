using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 v3 = transform.position;
            v3.z = -2.1f;
            transform.position = v3;
        }
        else
        {
            Vector3 v3 = transform.position;
            v3.z = -0.9f;
            transform.position = v3;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 v3 = transform.position;
            v3.z = 1f;
            transform.position = v3;
        }
    }
}
