using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3 : MonoBehaviour
{
    public PhysicsMaterial2D material;
    
    private GameObject player;
    private LevelInstance instance;
    private LevelEditInstance e_instance;
    private CameraScript cam;
    
    private Colliderscript col1;
    private Colliderscript col2;
    private Colliderscript col3;
    private Colliderscript col3_2;
    private Colliderscript col4;
    private Colliderscript col5;
    private Colliderscript col6;
    private Colliderscript col7;
    private Colliderscript col8;
    private Colliderscript col9;
    private Colliderscript col10;
    private Colliderscript col11;
    private Colliderscript col12;
    private Colliderscript col13;

   private Vector3 cam_pos_control;
   private float timer1 = 0.0f;

    private bool[] done_trigger; 
   
    // Start is called before the first frame update
    void Start()
    {
        instance = gameObject.GetComponent<LevelInstance>();
        e_instance = gameObject.GetComponent<LevelEditInstance>();

        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>();
        player = GameObject.FindGameObjectWithTag("Player");

        done_trigger = new bool[] {false, false, false, false, false, 
        false, false, false, false, false, false, false, false, false, false, 
        false, false, false};
        
        col1 = transform.Find("collider1").gameObject.GetComponent<Colliderscript>();
        col2 = transform.Find("collider2").gameObject.GetComponent<Colliderscript>();
        col3 = transform.Find("collider3").gameObject.GetComponent<Colliderscript>();
        col3_2 = transform.Find("collider3_2").gameObject.GetComponent<Colliderscript>();
        col4 = transform.Find("collider4").gameObject.GetComponent<Colliderscript>();
        col5 = transform.Find("collider5").gameObject.GetComponent<Colliderscript>();
        col6 = transform.Find("collider6").gameObject.GetComponent<Colliderscript>();
        col7 = transform.Find("collider7").gameObject.GetComponent<Colliderscript>();
        col8 = transform.Find("collider8").gameObject.GetComponent<Colliderscript>();
        col9 = transform.Find("collider9").gameObject.GetComponent<Colliderscript>();
        col10 = transform.Find("collider10").gameObject.GetComponent<Colliderscript>();
        col11 = transform.Find("collider11").gameObject.GetComponent<Colliderscript>();
        col12 = transform.Find("collider12").gameObject.GetComponent<Colliderscript>();
        col13 = transform.Find("collider13").gameObject.GetComponent<Colliderscript>();

        
    }

    // Update is called once per frame
    void Update()
    {
        //* Part 1: Updating the information to LevelInstance
        instance.lvl_trigger_intance.Clear();
        for (var i = 0; i < done_trigger.Length; i++)
        {
            instance.lvl_trigger_intance.Add(done_trigger[i]);
            Debug.Log("instance");
        }

        if (done_trigger[3]){
            cam.controlled_pos = new Vector3(cam.activebox_pos.x, cam.activebox_pos.y, cam.default_pos.z);
        }

        if (done_trigger[5]){
            cam.controlled_pos = new Vector3(player.transform.position.x, cam.default_pos.y, cam.default_pos.z);
            timer1 += Time.deltaTime;
            if(timer1 >= 2f)
            {
                timer1 = 0.0f;
                cam.get_controlled = false;
                cam.follow_player_xy = false;
                done_trigger[5] = false;
            }
        }

        if (done_trigger[10]){
            var bridge = col8.c_object.gameObject;
            cam.controlled_pos = new Vector3(bridge.transform.position.x, bridge.transform.position.y, cam.default_pos.z);
            timer1 += Time.deltaTime;
            if(timer1 >= 2f)
            {
                if(done_trigger[8])
                {
                    done_trigger[3] = true;
                    done_trigger[10] = false;
                }
                else{
                    cam.follow_player_xy = true;
                    cam.get_controlled = false;
                }
            }
        }
            
        if (!done_trigger[0]){
            if(col1.triggered)
            {
                var current_size = cam.gameObject.GetComponent<Camera>().orthographicSize;
                StartCoroutine(cam.ChangeFloat(current_size, cam.default_size - 2.7f, 1f));
                done_trigger[0] = true;
            }
        }

        if (!done_trigger[1]){
            if(col2.triggered)
            {
                var current_size = cam.gameObject.GetComponent<Camera>().orthographicSize;
                StartCoroutine(cam.ChangeFloat(current_size, cam.default_size + 1.5f, 0.5f));
                done_trigger[1] = true;
            }
        }

        if (!done_trigger[2]){
            if(col3.triggered)
            {
                var current_size = cam.gameObject.GetComponent<Camera>().orthographicSize;
                cam.get_controlled = true;
                StartCoroutine(cam.ChangeFloat(current_size, cam.default_size + 4f, 1f));
                done_trigger[3] = true;
                done_trigger[2] = true;
            }
            else if(col3_2.triggered)
            {
                var current_size = cam.gameObject.GetComponent<Camera>().orthographicSize;
                cam.get_controlled = false;
                cam.follow_player_xy = true;
                StartCoroutine(cam.ChangeFloat(current_size, cam.default_size + 4f, 1f));
                done_trigger[2] = true;

            }
        }

        if (!done_trigger[4]){
            if(col4.triggered)
            {
                var current_size = cam.gameObject.GetComponent<Camera>().orthographicSize;
                StartCoroutine(cam.ChangeFloat(current_size, cam.default_size, 1f));
                done_trigger[4] = true;
                done_trigger[5] = true;
                done_trigger[3] = false;
                
            }
        }

        if(!done_trigger[6]){
            if(col5.triggered)
            {
                cam.follow_player_xy = true;
                done_trigger[6] = true;
            }
        }

        if(!done_trigger[7]){
            if(col6.triggered)
            {
                cam.get_controlled = true;
                done_trigger[7] = true;
                done_trigger[5] = true;
            }
        }

        if(!done_trigger[8]){
            if(col11.triggered)
            {
                if(col7.triggered)
                {
                    var current_size = cam.gameObject.GetComponent<Camera>().orthographicSize;
                    StartCoroutine(cam.ChangeFloat(current_size, cam.default_size + 5f, 1f));
                    cam.get_controlled = true;
                    done_trigger[3] = true;
                    done_trigger[8] = true;
                }
            }
        }

        if (!done_trigger[9]){
            if (col8.triggered)
            {
                cam.get_controlled = true;
                done_trigger[3] = false;
                done_trigger[10] = true;
                done_trigger[9] = true;

            }
        }

        if (!done_trigger[11]){
            if(col9.triggered)
            {
                cam.get_controlled = true;
                done_trigger[5]  = true;
                done_trigger[11] = true;
            }
        }

        if (!done_trigger[12]){
            if(col10.triggered)
            {
                cam.get_controlled = false;
                var current_size = cam.gameObject.GetComponent<Camera>().orthographicSize;
                StartCoroutine(cam.ChangeFloat(current_size, cam.default_size + 20f, 4f));
                done_trigger[12] = true;
                done_trigger[3] = false;
            }
        }

        if (!done_trigger[13]){
            if(col12.triggered)
            {
                cam.follow_player_xy = true;
                cam.get_controlled = false;
                done_trigger[13] = true;
            }
        }

        if(done_trigger[14]){
            if(col13.triggered)
            {
                col13.c_object.gameObject.GetComponent<Rigidbody2D>().sharedMaterial = material;
                done_trigger[14] = true;
            }
        }

        //* Part 2: Updating the information from LevelEditInstance (On checkpoint load)
        if (instance.check_it_out)
        {
            for (var i = 0; i < e_instance.trigger_new_data.Count; i++)
            {
                done_trigger[i] = e_instance.trigger_new_data[i];
                Debug.Log("Edit");
            }
            instance.check_it_out = false;
        }


    }
}
