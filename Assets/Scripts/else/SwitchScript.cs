using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour
{
    //! type 1 is turn-on only
    //! type 2 is toggle
    //! type 3 is hold
    
    [Range(1, 3)]
    public int type_of_switch;
    public bool Switch_on = false;
    public GameObject sound_effect;

    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    private GameObject c_object;
    private bool object_set_done = false;
    private Color base_color;
    private Color trigger_color;
    private float timer1 = 0.0f;
    private bool isdisabled = false;
    private bool small_disable = false;

    private void Start() {
        sprite = transform.parent.gameObject.GetComponent<SpriteRenderer>();
        coll = gameObject.GetComponent<BoxCollider2D>();

        base_color = sprite.color;
        ColorUtility.TryParseHtmlString("#FFED47", out trigger_color); //? yellow
    }
    
    private void Update() {
        if (!Switch_on){
            sprite.color = base_color;
            coll.enabled = true;

        }
           
        else
            sprite.color = trigger_color;
        

        if (isdisabled)
            disabled(0.2f);

        if (small_disable)
            disabled(0.05f);

       
        
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        
        if (!(isdisabled || small_disable))
        {
            if (!(other.CompareTag("floor") || other.CompareTag("glass") || other.CompareTag("reticle")))
            {
                if (type_of_switch == 1)
                {
                    Switch_on = true;
                    Instantiate(sound_effect, transform.position, Quaternion.identity);
                    coll.enabled = false;
                }

                else if (type_of_switch == 2)
                {
                    if (!Switch_on){
                        Switch_on = true;
                        isdisabled = true;
                        Instantiate(sound_effect, transform.position, Quaternion.identity);
                    }
                    else{
                        Switch_on = false;
                        isdisabled = true;
                        Instantiate(sound_effect, transform.position, Quaternion.identity);
                    }
                }
                else if(type_of_switch == 3)
                {
                    if(!object_set_done)
                    {
                        c_object = other.gameObject;
                        Instantiate(sound_effect, transform.position, Quaternion.identity);
                        object_set_done = true;
                        small_disable = true;
                    }
                    Switch_on = true;
                }

            }
        }
    }
    
    
    private void OnTriggerExit2D(Collider2D other) {
        
        if (type_of_switch == 3)
        {
           if(other.gameObject == c_object)
           {
                Switch_on = false;
                Instantiate(sound_effect, transform.position, Quaternion.identity);
                object_set_done = false;
           }
        }
    }

    private void disabled(float time)
    {
        timer1 += Time.deltaTime;
        coll.isTrigger = false;
        if (timer1 >= time)
        {
            coll.isTrigger = true;
            isdisabled = false;
            small_disable = false;
            timer1 = 0.0f;
        }
    }
}
