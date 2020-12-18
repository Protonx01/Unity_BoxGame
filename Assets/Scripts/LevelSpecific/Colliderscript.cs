using UnityEngine;

public class Colliderscript : MonoBehaviour {
    
    public GameObject c_object;
    public bool keep_triggerd = false;
    public bool triggered = false;

    
    private void OnTriggerEnter2D(Collider2D other) {
        
        if (c_object != null){

            if(other.gameObject == c_object)
            {
                triggered = true;
            }
        }
        else{

            if (other.CompareTag("Player"))
            {
                triggered = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        
        if (c_object != null){

            if(other.gameObject == c_object)
            {
                if(!keep_triggerd)
                {
                    triggered = false;
                }
            }
        }
        else{

            if (other.CompareTag("Player"))
            {
                if(!keep_triggerd)
                {
                    triggered = false;
                }
            }
        }
    }
}