using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1UI : MonoBehaviour
{
    private Colliderscript col2;
    private Colliderscript col3;
    private Colliderscript col4;

    private bool trigger1 = false;
    private bool trigger2 = false;
    private bool trigger3 = false;
    private bool trigger4 = false;
    
    
    
    void Start()
    {
        col2 = transform.Find("collider2").GetComponent<Colliderscript>();
        col3 = transform.Find("collider3").GetComponent<Colliderscript>();
        col4 = transform.Find("collider4").GetComponent<Colliderscript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!trigger1)
        {
            if( Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.UpArrow))
            {
                transform.Find("tutorial1").gameObject.SetActive(false);
                transform.Find("tutorial2").gameObject.SetActive(true);

                trigger1 = true;
            }
        }
        else if (!trigger2)
        {
            if (col2.triggered)
            {
                transform.Find("tutorial2").gameObject.SetActive(false);
                transform.Find("tutorial3").gameObject.SetActive(true);

                trigger2 = true;
            }
        }
        else if (!trigger3)
        {
            if (col3.triggered)
            {
                transform.Find("tutorial3").gameObject.SetActive(false);
                transform.Find("tutorial4").gameObject.SetActive(true);

                trigger3 = true;
            }
        }
        else if (!trigger4)
        {
            if (col4.triggered)
            {
                transform.Find("tutorial4").gameObject.SetActive(false);

                trigger4 = true;
            }

        }

    }
}
