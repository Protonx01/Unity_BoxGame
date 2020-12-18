using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public Animator panel_tint;
   

    public bool follow_player_xy;
    public bool get_controlled = false;
    [HideInInspector]
    public Vector3 default_pos;
    [HideInInspector]
    public Vector3 target_pos;
    [HideInInspector]
    public Vector3 controlled_pos;
    [HideInInspector]
    public Vector3 activebox_pos;
    [HideInInspector]
    public float default_size;

   

    private GameObject left;
    private GameObject right;
    private GameObject up;
    private GameObject down;

    private bool Out_of_screen = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        left = transform.Find("Left_CamBorder").gameObject; 
        right = transform.Find("Right_CamBorder").gameObject; 
        //up = transform.Find("Up_CamBorder").gameObject; 
        down = transform.Find("Down_CamBorder").gameObject; 

        default_size = gameObject.GetComponent<Camera>().orthographicSize;

       
        if(follow_player_xy)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        }
        else{
            transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        }

        default_pos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        try {activebox_pos = player.GetComponent<PlayerScript>().current_box.transform.position;}
        catch{}
        
        if(!get_controlled)
        {

            if (!follow_player_xy)
            {
                if (player.GetComponent<PlayerScript>().doll_connected && !Out_of_screen)
                {
                    var rel_player_pos = Camera.main.WorldToViewportPoint(player.transform.position);

                    if ((rel_player_pos.x >= 1f || rel_player_pos.x <= 0f) ||
                    (rel_player_pos.y >= 1f || rel_player_pos.y <= 0f)) 
                    {
                        Out_of_screen = true;
                    }
                    else{
                        target_pos = new  Vector3((player.transform.position.x + activebox_pos.x)/2, transform.position.y, transform.position.z);
                    }
                }

                else{
                    var rel_box_pos = Camera.main.WorldToViewportPoint(activebox_pos);

                    if ((rel_box_pos.x <= 0.95f && rel_box_pos.x >= 0.05f) &&
                    (rel_box_pos.y <= 0.95f && rel_box_pos.y >= 0.05f)) 
                    {
                        Out_of_screen = false;
                    }
                    target_pos = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
                }
                
                transform.Translate((target_pos - transform.position) * speed * Time.deltaTime);
                Out_of_screen = false;
            }

            else
            {
                if (player.GetComponent<PlayerScript>().doll_connected && !Out_of_screen)
                {
                    var rel_player_pos = Camera.main.WorldToViewportPoint(player.transform.position);

                    if ((rel_player_pos.x >= 1f || rel_player_pos.x <= 0f) ||
                    (rel_player_pos.y >= 1f || rel_player_pos.y <= 0f)) 
                    {
                        Out_of_screen = true;
                    }
                    else{
                        target_pos = new  Vector3((player.transform.position.x + activebox_pos.x)/2, 
                        (player.transform.position.y + activebox_pos.y)/2, transform.position.z);
                    }

                }

                else{
                    var rel_box_pos = Camera.main.WorldToViewportPoint(activebox_pos);

                    if ((rel_box_pos.x <= 0.95f && rel_box_pos.x >= 0.05f) &&
                    (rel_box_pos.y <= 0.95f && rel_box_pos.y >= 0.05f)) 
                    {
                        Out_of_screen = false;
                    }
                    target_pos = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
                }
                
                transform.Translate((target_pos - transform.position) * speed * Time.deltaTime);
                Out_of_screen = false;
            }
                
        }

        else
        {
            transform.Translate((controlled_pos - transform.position) * speed * Time.deltaTime);
        }

        
    }

    public IEnumerator ChangeFloat( float v_start, float v_end, float duration )
    {
        float elapsed = 0.0f;
        while (elapsed < duration )
        {
            gameObject.GetComponent<Camera>().orthographicSize = Mathf.Lerp( v_start, v_end, elapsed / duration );
            elapsed += Time.deltaTime;
            yield return null;
        }
        gameObject.GetComponent<Camera>().orthographicSize = v_end;
    }

}
