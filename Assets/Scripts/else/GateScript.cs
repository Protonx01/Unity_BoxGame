using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class GateScript : MonoBehaviour
{
    public float speed;
    public GameObject switch_conneceted;
    [HideInInspector]
    public Vector3 current_target;
    public Color active_color;
    public bool use_color = false;

    private Tilemap sprite;
    private GameObject trigger;
    private Vector3 target_pos;
    private Vector3 default_pos;
    private Color base_color;
    private float default_speed;
    

    void Start()
    {
        sprite = gameObject.GetComponent<Tilemap>();
        base_color = sprite.color;
        if (!use_color)
            ColorUtility.TryParseHtmlString("#FFEC47", out active_color); //? yellow

        trigger = switch_conneceted.transform.Find("trigger").gameObject;
        target_pos = transform.Find("target").position;
        default_pos = transform.position;
        default_speed = speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (trigger.GetComponent<SwitchScript>().Switch_on)
        {
            current_target = target_pos;
            sprite.color = active_color;    
        }
        else
        {
            current_target = default_pos;
            sprite.color = base_color;
        }
        
        if (transform.position != current_target)
        {
            transform.position =  Vector2.MoveTowards(transform.position, current_target, speed * Time.fixedDeltaTime);
            gameObject.GetComponent<AudioSource>().enabled = true;
            gameObject.GetComponent<AudioSource>().loop = true;
            Debug.Log(speed);
        }
        else
        {
            gameObject.GetComponent<AudioSource>().enabled = false;
            speed = default_speed;
        }
    }
}
