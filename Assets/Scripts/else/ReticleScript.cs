using UnityEngine;

public class ReticleScript : MonoBehaviour 
{
    [HideInInspector]
    public bool doll_found = false;
    [HideInInspector]
    public GameObject doll;

    private PlayerScript player;
    
    private void Start() {
        doll_found = false;
        transform.position = GameObject.FindGameObjectWithTag("Player").gameObject.transform.position;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }
    
    private void Update() {

        var ms_pos = Input.mousePosition;
        ms_pos.z = 10;
        var mouse_pos = Camera.main.ScreenToWorldPoint(ms_pos);
        transform.position = mouse_pos;
        var current_pos = Camera.main.WorldToViewportPoint(transform.position);
    }  

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag("DollsInactive")){
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().reticle_connected)
            {
                doll_found = true;
                doll = other.gameObject;
                other.GetComponent<ActiveDollScript>().connected = true;
                Instantiate(player.connected_sound, transform.position, Quaternion.identity);
                other.GetComponent<InactiveDollScript>().FirstRayHit = true;
            }
        }
        else if (other.CompareTag("DollActive"))
        {
            doll_found = true;
            doll = other.gameObject;
            other.GetComponent<ActiveDollScript>().connected = true;
        }
        
    }  
}