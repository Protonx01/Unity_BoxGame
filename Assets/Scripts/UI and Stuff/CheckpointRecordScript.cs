using System.Collections.Generic;
using UnityEngine;

public class CheckpointRecordScript : MonoBehaviour 
{
    [HideInInspector]
    public List<PointInTime> Boxes_list, Movables_list, Gates_list;
    [HideInInspector]
    public List<bool> Switches_list, Lvltriggers_list;
    [HideInInspector]
    public PointInTime player_pos;
    [HideInInspector]
    public GameObject player;

    [HideInInspector]
    public LevelLoderScript lvl_loader;
    [HideInInspector]
    public LevelInstance lvl_master;

    [HideInInspector]
    public Vector3 camera_pos;
    [HideInInspector]
    public bool cam_follow, cam_control; 
    [HideInInspector]
    public float cam_size;

    private void Start() {
        
        Boxes_list = new List<PointInTime>();
        Movables_list = new List<PointInTime>();
        // Gates_list = new List<PointInTime>();
        Switches_list = new List<bool>();

        lvl_master = GameObject.FindGameObjectWithTag("LevelMaster").GetComponent<LevelInstance>();
        lvl_loader = GameObject.FindGameObjectWithTag("LvlLoader").GetComponent<LevelLoderScript>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.CompareTag("Player"))
        {
            Boxes_list.Clear();
            Movables_list.Clear();
            //Gates_list.Clear();
            Switches_list.Clear();
            Lvltriggers_list.Clear();

            Debug.Log("Saved!!!");
            player_pos = new PointInTime(player.transform.position, Quaternion.identity);

            camera_pos = Camera.main.transform.position;
            cam_follow = Camera.main.GetComponent<CameraScript>().follow_player_xy;
            cam_control = Camera.main.GetComponent<CameraScript>().get_controlled;
            cam_size = Camera.main.GetComponent<Camera>().orthographicSize;


            for (var i = 0; i < lvl_loader.Boxes.Length; i++)
            {
                var item = new PointInTime(lvl_loader.Boxes[i].transform.position, lvl_loader.Boxes[i].transform.rotation);
                Boxes_list.Add(item);
            }
            for (var i = 0; i < lvl_loader.Movables.Length; i++)
            {
                var item = new PointInTime(lvl_loader.Movables[i].transform.position, lvl_loader.Movables[i].transform.rotation);
                Movables_list.Add(item);
            }
            // for (var i = 0; i < lvl_loader.Gates.Length; i++)
            // {
            //     var item = new PointInTime(lvl_loader.Movables[i].transform.position, lvl_loader.Movables[i].transform.rotation);
            //     Gates_list.Add(item);
            // }
            for (var i = 0; i < lvl_loader.Switches.Length; i++ )
            {
                Switches_list.Add(lvl_loader.Switches[i].GetComponent<SwitchScript>().Switch_on);
            }
            for (var i = 0; i < lvl_master.lvl_trigger_intance.Count; i++)
            {
                Lvltriggers_list.Add(lvl_master.lvl_trigger_intance[i]);
            }

            lvl_loader.last_record = this;
        }
    }
    
}