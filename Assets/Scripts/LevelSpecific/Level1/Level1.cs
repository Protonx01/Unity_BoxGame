using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour {
    
    private GameObject player;
    private LevelInstance instance;
     private LevelEditInstance e_instance;
    private CameraScript cam;
    private Colliderscript col1;
    private Colliderscript col2;
    private Colliderscript col3;
    private Colliderscript col4;

    private bool[] done_trigger = {false, false, false};

    // private List<bool> trigger1_list;
    // private List<bool> trigger2_list;

    // private bool isRewinding = false;
    // private float recordTime = 5f;
    

    private void Start() {
        
        instance = gameObject.GetComponent<LevelInstance>();
        e_instance = gameObject.GetComponent<LevelEditInstance>();

        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>();
        player = GameObject.FindGameObjectWithTag("Player");

        col1 = transform.Find("collider1").gameObject.GetComponent<Colliderscript>();
        col2 = transform.Find("collider2").gameObject.GetComponent<Colliderscript>();
        col3 = transform.Find("collider3").gameObject.GetComponent<Colliderscript>();
        col4 = transform.Find("collider4").gameObject.GetComponent<Colliderscript>();

        // trigger1_list = new List<bool>();
        // trigger2_list = new List<bool>();

    }

    private void Update() {
        
        //* Part 1: Updating the information to LevelInstance
        instance.lvl_trigger_intance.Clear();
        for (var i = 0; i < done_trigger.Length; i++)
        {
            instance.lvl_trigger_intance.Add(done_trigger[i]);
        }

        cam.controlled_pos = new Vector3 (player.transform.position.x, cam.default_pos.y, cam.default_pos.z); 

        //* After getting on the lift, camera should follow player completely 
        if (!done_trigger[0])     
        {
            if(col1.triggered)
            {
                cam.get_controlled = false;
                cam.follow_player_xy = true;
                done_trigger[0] = true;
            }
        }

        //* After the end of the lift, camera should only track the player x position
        if (!done_trigger[1])    
        {
            if (col2.triggered)
            {
                cam.get_controlled = false;
                cam.follow_player_xy = false;
                done_trigger[1] = true;
            }
        }

        //* Last part of the Level (Bonus), Pan out the camera
        if(!done_trigger[2])
        {
            if (col4.triggered)
            {
                cam.follow_player_xy = true;
                var current_size = cam.GetComponent<Camera>().orthographicSize;
                StartCoroutine(cam.ChangeFloat(current_size, cam.default_size + 5f, 1f));
                done_trigger[2] = false;
            }
        }

        //* In case the player falls down, the camera should slowly return to it's default y, and continue following player x
        //* Also reset the lvl_triggers, in case the player tries again
        if (col3.triggered)    
        {                      
            cam.get_controlled = true;
            var current_size = cam.GetComponent<Camera>().orthographicSize;
            StartCoroutine(cam.ChangeFloat(current_size, cam.default_size, 1f));
            done_trigger[0] = false;
            done_trigger[1] = false;
            done_trigger[2] = false;
            
        }

        //* Part 2: Updating the information from LevelEditInstance (On checkpoint load)
        if (instance.check_it_out)
        {
            for (var i = 0; i < done_trigger.Length; i++)
            {
                done_trigger[i] = e_instance.trigger_new_data[i];
            }
            instance.check_it_out = false;
        }

        // if (Input.GetKeyDown(KeyCode.Return))
		// {
		// 	StartRewind();
		// }
		// if (Input.GetKeyUp(KeyCode.Return))
		// {
		// 	StopRewind();
		// }

    }
}