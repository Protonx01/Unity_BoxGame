using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public GameObject reticle;
    public LineRenderer laserLine;
    public GameObject connected_sound;
    public GameObject disconnected_sound;

    [HideInInspector]
    public bool reticle_connected = false;
    [HideInInspector]
    public bool doll_connected = false;
    [HideInInspector]
    public GameObject current_box;

    private GameObject[] dolls;
    private bool data_collected = false;
    private int mask;
    private Transform pointing;
    

    
    void Start()
    {
        
        Cursor.visible = false;


        mask = 1 << LayerMask.NameToLayer("Player");
        mask |= 1 << LayerMask.NameToLayer("TransparentFX"); 
        mask |= 1 << LayerMask.NameToLayer("UI");
        mask = ~mask;
    }

   
    void Update()
    {

        if (!data_collected)
        {
            dolls = GameObject.FindGameObjectsWithTag("DollsInactive");
            data_collected = true;
        }

        if (doll_connected)
            pointing = current_box.transform;
        else
            pointing = reticle.transform;

        //! Lazer target set------------

        var laser = pointing.position - transform.position;
        var laser_direction = new Vector2(laser.x, laser.y);
        var laser_distance = Vector2.SqrMagnitude(laser_direction);

        //! Box Ray Casting----------------

        if (doll_connected)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, laser_direction, laser_distance,mask);
            if (hit)
            {
                laserLine.enabled = true;
                shoot_laser(pointing.position, 1);
                if (!(hit.collider.CompareTag("DollActive") || hit.collider.CompareTag("reticle")))
                 {
                        doll_connected = false;
                        current_box.GetComponent<ActiveDollScript>().connected = false;
                        Instantiate(disconnected_sound, transform.position, Quaternion.identity);
                        laserLine.enabled = false;
                }
            }
        }
        

        //! Reticle control----------(with Left click)

        if (Input.GetMouseButtonDown(0)){
            doll_connected = false;
            reticle.GetComponent<ReticleScript>().doll_found = false;
             try {
                if(current_box.GetComponent<ActiveDollScript>().connected)
                {
                    current_box.GetComponent<ActiveDollScript>().connected = false;
                    Instantiate(disconnected_sound, transform.position, Quaternion.identity);
                }

            }   
            catch{}
        }
        
        if (Input.GetMouseButton(0))
        {    
            
            reticle.SetActive(true);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, laser_direction, laser_distance, mask);
            laserLine.enabled = true;

            if (hit)
            {
                if (hit.collider.CompareTag("reticle")){
                    reticle_connected = true;
                    shoot_laser(pointing.position, 2);
                }
                else{
                    reticle_connected = false;
                    shoot_laser(pointing.position, 3);
                }
            }

            if (reticle.GetComponent<ReticleScript>().doll_found)
            {
                current_box = reticle.GetComponent<ReticleScript>().doll;
                doll_connected = true;
                reticle.transform.position = transform.position;
                reticle.SetActive(false);
            }
           
        }

        if (Input.GetMouseButtonUp(0)){
            reticle.transform.position = transform.position;
            reticle.SetActive(false);
            laserLine.enabled = false;
        }



        //! Auto connect (RightShift)--------------

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            if (current_box != null)
            {
                var _laser = current_box.transform.position - transform.position;
                var _laser_direction = new Vector2(_laser.x, _laser.y);
                var _laser_distance = Vector2.SqrMagnitude(_laser_direction);

                RaycastHit2D hit = Physics2D.Raycast(transform.position, _laser_direction, _laser_distance, mask);

                if (hit)
                {
                    shoot_laser(current_box.transform.position, 1);
                    if (hit.collider.CompareTag("DollsInactive"))
                    {
                        doll_connected = true;
                        current_box.GetComponent<InactiveDollScript>().FirstRayHit = true;
                        current_box.GetComponent<ActiveDollScript>().connected = true;
                        Instantiate(connected_sound, transform.position, Quaternion.identity);
                        laserLine.enabled = true;
                    }
                    else{
                        laserLine.enabled = false;
                    }

                }
                
            }
        }

        //! QuickConnect (with Right Ctrl)--------------

        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            doll_connected = false;
            try {
                if(current_box.GetComponent<ActiveDollScript>().connected)
                {
                    current_box.GetComponent<ActiveDollScript>().connected = false;
                    Instantiate(disconnected_sound, transform.position, Quaternion.identity);
                }

            }   
            catch{}

            for (var i = 0; i < dolls.Length; i++ )
            {
                var __laser = dolls[i].transform.position - transform.position;
                var __laser_direction = new Vector2(__laser.x, __laser.y);
                var __laser_distance = Vector2.SqrMagnitude(__laser_direction);

                RaycastHit2D hit = Physics2D.Raycast(transform.position, __laser_direction, __laser_distance, mask);
                if (hit)
                {
                    shoot_laser(dolls[i].transform.position, 1);
                    if (hit.collider.CompareTag("DollsInactive") && hit.transform == dolls[i].transform)
                    {
                        doll_connected = true;
                        current_box = dolls[i]; 
                        Instantiate(connected_sound, transform.position, Quaternion.identity);
                        current_box.GetComponent<InactiveDollScript>().FirstRayHit = true;
                        current_box.GetComponent<ActiveDollScript>().connected = true;
                        laserLine.enabled = true;
                        break;
                    }
                    else{
                        laserLine.enabled = false;
                    }
                }
            }
        }

       
    }

   
    
    void shoot_laser(Vector3 target, int color_type)
    {

        Color color1_1, color1_2, color2_1, color2_2, color3_1, color3_2;

        ColorUtility.TryParseHtmlString("#EE5D64", out color1_1);
        ColorUtility.TryParseHtmlString("#79E0F1", out color1_2);
        ColorUtility.TryParseHtmlString("#FF34FE", out color2_1);
        ColorUtility.TryParseHtmlString("#EE5DCC", out color2_2);
        ColorUtility.TryParseHtmlString("#CF2D19", out color3_1);
        ColorUtility.TryParseHtmlString("#FF4134", out color3_2);


        laserLine.SetPosition(0, transform.position);
        laserLine.SetPosition(1, target);
        
        if (color_type == 1) //* Red-blue connected lazer
        {
            laserLine.startColor = color1_1;
            laserLine.endColor = color1_2;
        }
        else if (color_type == 2) //* pink-violet cursor lazer
        {
            laserLine.startColor = color2_1;
            laserLine.endColor = color2_2;
        }
        else if (color_type == 3) //* red disconnect lazer
        {
            laserLine.startColor = color3_1;
            laserLine.endColor = color3_2;
        }
    }
}
