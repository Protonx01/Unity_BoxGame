using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSample : MonoBehaviour
{
    private GameObject player;
    private LevelInstance instance;
    private LevelEditInstance e_instance;
    private CameraScript cam;
    private Colliderscript col1;
    private Colliderscript col2;
    

    private bool[] done_trigger = {false, false};
    
    
    
    
    void Start()
    {
        instance = gameObject.GetComponent<LevelInstance>();
        e_instance = gameObject.GetComponent<LevelEditInstance>();

        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>();
        player = GameObject.FindGameObjectWithTag("Player");

        col1 = transform.Find("collider1").gameObject.GetComponent<Colliderscript>();
        col2 = transform.Find("collider2").gameObject.GetComponent<Colliderscript>();

        
        
    }

    // Update is called once per frame
    void Update()
    {
        instance.lvl_trigger_intance.Clear();
        for (var i = 0; i < done_trigger.Length; i++)
        {
            instance.lvl_trigger_intance.Add(done_trigger[i]);
        }

        if (!done_trigger[0])
        {
            if(col1.triggered)
            {
                cam.get_controlled = false;
                cam.follow_player_xy = true;
                done_trigger[0] = true;
            }
        }

        if (!done_trigger[1])
        {
            if (col2.triggered)    
            {                      
                cam.follow_player_xy = false;
                done_trigger[1] = true;
                // done_trigger[0] = false;
                // done_trigger[1] = false;
                
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
}
