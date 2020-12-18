using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3UI : MonoBehaviour
{
    
    private Colliderscript col1;
    private Colliderscript col2;
    private Colliderscript col3;
    private Colliderscript col4;
    private Colliderscript col5;
    private Colliderscript col6;
    private Colliderscript col7;

    private bool[] trigger = {false, false, false, false, false, false, false, false, false}; //? 9 values(upto [8])
    
    private float timer1 = 0.0f;
    
    
    void Start()
    {
        col1 = transform.Find("collider1").GetComponent<Colliderscript>();
        col2 = transform.Find("collider2").GetComponent<Colliderscript>();
        col3 = transform.Find("collider3").GetComponent<Colliderscript>();
        col4 = transform.Find("collider4").GetComponent<Colliderscript>();
        col5 = transform.Find("collider5").GetComponent<Colliderscript>();
        col6 = transform.Find("collider6").GetComponent<Colliderscript>();
        col7 = transform.Find("collider7").GetComponent<Colliderscript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!trigger[0])
        {
            if(col1.triggered)
            {
                transform.Find("tutorial1").gameObject.SetActive(false);
                trigger[0] = true;
            }
        }

        if (!trigger[1])
        {
            if(col2.triggered)
            {
                if(!trigger[2])
                {
                    transform.Find("tutorial2").gameObject.SetActive(true);
                    trigger[2] = true;
                }
                if(Input.GetMouseButtonDown(0))
                {
                    transform.Find("tutorial2").gameObject.SetActive(false);
                    transform.Find("tutorial3").gameObject.SetActive(true);
                }

                if(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().doll_connected)
                {
                    transform.Find("tutorial3").gameObject.SetActive(false);
                    transform.Find("tutorial4").gameObject.SetActive(true);
                    transform.Find("tutorial2").gameObject.SetActive(false);

                }
                else if(Input.GetMouseButtonUp(0))
                {
                    transform.Find("tutorial2").gameObject.SetActive(true);
                    transform.Find("tutorial3").gameObject.SetActive(false);
                }

            }

            if(col4.triggered)
            {
                transform.Find("tutorial4").gameObject.SetActive(false);
                transform.Find("tutorial5").gameObject.SetActive(true);
                trigger[1] = true;
            }
        }

        if(!trigger[3])
        {
            if(col3.triggered)
            {
                transform.Find("tutorial5").gameObject.SetActive(false);
                transform.Find("tutorial7").gameObject.SetActive(true);
                trigger[3] = true;
            }
        }

        if(!trigger[4])
        {
            if(col5.triggered)
            {
                transform.Find("tutorial6").gameObject.SetActive(true);
                if(trigger[3])
                {
                    transform.Find("tutorial7").gameObject.SetActive(false);
                }
                else{
                    transform.Find("tutorial5").gameObject.SetActive(false);
                }
                trigger[4] = true;
            }
        }

        if(!trigger[5])
        {
            if(col6.triggered)
            {
                
                if(!trigger[6])
                {
                    transform.Find("tutorial8").gameObject.SetActive(true);
                    trigger[6] = true;
                }
                timer1 += Time.deltaTime;
                if(timer1 >= 5f)
                {
                    transform.Find("tutorial8").gameObject.SetActive(false);
                    transform.Find("tutorial9").gameObject.SetActive(true);
                    timer1 = 0.0f;
                    trigger[5] = true;
                }
            }
        }

        if (!trigger[7])
        {
            if (col7.triggered)
            {
                if(!trigger[8])
                {
                    transform.Find("tutorial10").gameObject.SetActive(true);
                    transform.Find("tutorial9").gameObject.SetActive(false);
                    trigger[8] = true;
                }
                timer1 += Time.deltaTime;
                if(timer1 >= 5f)
                {
                    transform.Find("tutorial10").gameObject.SetActive(false);
                    transform.Find("tutorial11").gameObject.SetActive(true);
                }
                if(timer1 >= 10f)
                {
                    transform.Find("tutorial11").gameObject.SetActive(false);
                    timer1 = 0.0f;
                    trigger[7] = true;
                }
            }
        }
    }
}
