using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headScript : MonoBehaviour
{
   
    private GameObject parent;

    void Start()
    {
        parent = gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(parent.transform.position.x, parent.transform.position.y + 0.807f, transform.position.z);

    }

    private void OnCollisionEnter2D(Collision2D other) {
        
        if (other.collider.gameObject != parent)
        {
            if (other.collider.CompareTag("Player") || other.collider.CompareTag("DollActive"))
            {
                // parent.GetComponent<CharacterController2D>().m_JumpForce *= 2;
                Debug.Log("Si");
            }
        }
    }
}

