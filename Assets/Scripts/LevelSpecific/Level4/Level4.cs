using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4 : MonoBehaviour
{
    public PhysicsMaterial2D material;

    private GameObject player;
    private LevelInstance instance;
    private LevelEditInstance e_instance;
    private CameraScript cam;
    private Colliderscript col1;
    private Colliderscript col2;
    private Colliderscript col3;
    private Colliderscript col4;
    private Colliderscript col5;

   private Vector3 cam_pos_control;
   private float timer1 = 0.0f;

    private bool[] done_trigger = {false, false, false, false, true};
   
    // Start is called before the first frame update
    void Start()
    {
        instance = gameObject.GetComponent<LevelInstance>();
        e_instance = gameObject.GetComponent<LevelEditInstance>();

        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>();
        player = GameObject.FindGameObjectWithTag("Player");
        
        col1 = transform.Find("collider1").gameObject.GetComponent<Colliderscript>();
        col2 = transform.Find("collider2").gameObject.GetComponent<Colliderscript>();
        col3 = transform.Find("collider3").gameObject.GetComponent<Colliderscript>();
        col4 = transform.Find("collider4").gameObject.GetComponent<Colliderscript>();
        col5 = transform.Find("collider5").gameObject.GetComponent<Colliderscript>();

        col5.enabled = false;

    }

    void Update()
    {
        instance.lvl_trigger_intance.Clear();
        for (var i = 0; i < done_trigger.Length; i++)
        {
            instance.lvl_trigger_intance.Add(done_trigger[i]);
        }

        cam.controlled_pos = new Vector3 (player.transform.position.x, cam.default_pos.y, cam.default_pos.z); 
        
        if(!done_trigger[0])
        {
            if (col1.triggered)
            {
                col1.c_object.GetComponent<Rigidbody2D>().sharedMaterial = material;
                done_trigger[0] = true;
            }
        }
        if(!done_trigger[1])
        {
            if(col2.triggered)
            {
                cam.get_controlled = false;
                cam.follow_player_xy = true;
                done_trigger[1] = true;
                done_trigger[2] = false;
            }

        }

        if(!done_trigger[2])
        {
            if (col3.triggered)
            {
                cam.get_controlled = true;
                cam.follow_player_xy = false;
                done_trigger[1] = false;
                done_trigger[2] = true;
            }
        }
        if(!done_trigger[3])
        {
            if (col4.triggered)
            {
                cam.get_controlled = false;
                cam.follow_player_xy = true;
                done_trigger[4] = false;
                done_trigger[3] = true;
                col5.enabled = true;
            }
        }
        if(!done_trigger[4])
        {
            if(col5.triggered)
            {
                col5.enabled = false;
                cam.get_controlled = true;
                cam.follow_player_xy = false;
                done_trigger[4] = true;
                done_trigger[3] = false;
            }
        }
        

        if (instance.check_it_out)
        {
            for (var i = 0; i < done_trigger.Length; i++)
            {
                done_trigger[i] = e_instance.trigger_new_data[i];
            }
            instance.check_it_out = false;
        }

    }

    void reload(bool trig, float time) {

        
        timer1 += Time.deltaTime;
        if(timer1 > time)
        {
            trig = false;
            timer1 = 0.0f;
        }
    }
}
