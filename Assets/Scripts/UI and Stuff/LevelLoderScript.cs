using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoderScript : MonoBehaviour
{
    public Animator trans_left;
    public Animator trans_right;

    [HideInInspector]
    public GameObject[] Boxes, Movables, Gates, Switches;
    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public GameObject cam;
    [HideInInspector]
    public GameObject lvl_master;
    [HideInInspector]
    public CheckpointRecordScript last_record;

    public GameObject checkpoint;
    
    private float transition_time = 1f;

    private void Start() {
        
        Boxes = GameObject.FindGameObjectsWithTag("DollsInactive");
        Movables = GameObject.FindGameObjectsWithTag("Movables");
        Gates = GameObject.FindGameObjectsWithTag("Gates");
        Switches = GameObject.FindGameObjectsWithTag("Switch");
        player = GameObject.FindGameObjectWithTag("Player");
        cam = Camera.main.gameObject;
        try{
            lvl_master = GameObject.FindGameObjectWithTag("LevelMaster");
            last_record = checkpoint.GetComponent<CheckpointRecordScript>();
        }catch{}
        

    }

    public void LoadNextLevel()
    {
        StartCoroutine(Loadit(SceneManager.GetActiveScene().buildIndex + 1));
    }
    public void ReloadLevel()
    {
        StartCoroutine(Loadit(SceneManager.GetActiveScene().buildIndex));
    }
    public void LoadLevel(int index)
    {
        StartCoroutine(Loadit(index));
    }
    public void LoadMenu()
    {
        StartCoroutine(Loadit(0));
    }
    public void LoadCheckpoint()
    {
        
        StartCoroutine(loadcheckpoint());
    }
    public void ExitLevel()
    {
        StartCoroutine(exitlevel());
    }
    
    
    
    IEnumerator exitlevel()
    {
        trans_left.SetTrigger("start_ending");
        trans_right.SetTrigger("start_ending");

        yield return new WaitForSecondsRealtime(1f);

        Debug.Log("Exit!!");
        Application.Quit();

    }

    IEnumerator Loadit(int index)
    {
        trans_left.SetTrigger("start_ending");
        trans_right.SetTrigger("start_ending");

        yield return new WaitForSecondsRealtime(transition_time);

        SceneManager.LoadScene(index);
    }

    IEnumerator loadcheckpoint()
    {
        trans_left.SetTrigger("start_ending");
        trans_right.SetTrigger("start_ending");

        yield return new WaitForSecondsRealtime(transition_time);

        player.transform.position = last_record.player_pos.position;
        player.transform.rotation = last_record.player_pos.rotation;
        player.GetComponent<BoxCollider2D>().enabled = true;
        player.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        player.GetComponent<Rigidbody2D>().useAutoMass = true;


        cam.transform.position = last_record.camera_pos;
        cam.GetComponent<CameraScript>().follow_player_xy = last_record.cam_follow;
        cam.GetComponent<CameraScript>().get_controlled = last_record.cam_control;
        cam.GetComponent<Camera>().orthographicSize = last_record.cam_size;

        lvl_master.GetComponent<LevelInstance>().check_it_out = true;
        lvl_master.GetComponent<LevelEditInstance>().trigger_new_data.Clear();
        for (var i = 0; i < last_record.Lvltriggers_list.Count; i++)
        {
            lvl_master.GetComponent<LevelEditInstance>().trigger_new_data.Add(last_record.Lvltriggers_list[i]);
        }

        for (var i = 0; i < last_record.Boxes_list.Count; i++)
        {
            Boxes[i].transform.position = last_record.Boxes_list[i].position;
            Boxes[i].transform.rotation = last_record.Boxes_list[i].rotation;
            Boxes[i].GetComponent<InactiveDollScript>().FirstRayHit = false;
            Boxes[i].GetComponent<ActiveDollScript>().connected = false;
        }
        for (var i = 0; i < last_record.Movables_list.Count; i++)
        {
            Movables[i].transform.position = last_record.Movables_list[i].position;
            Movables[i].transform.rotation = last_record.Movables_list[i].rotation;
        }
        for (var i = 0; i < Gates.Length; i++)
        {
            Gates[i].GetComponent<GateScript>().speed = Mathf.Infinity;
            
        }
        for (var i = 0; i < last_record.Switches_list.Count; i++)
        {
            Switches[i].GetComponent<SwitchScript>().Switch_on = last_record.Switches_list[i];
        }
        
        trans_left.SetTrigger("start_start");
        trans_right.SetTrigger("start_start");
    }
}
