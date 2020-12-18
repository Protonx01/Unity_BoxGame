using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGaudioScript : MonoBehaviour
{
    public static BGaudioScript instance;

    private void Awake() {
        
        if(instance == null)
        {
            instance = this;
        }
        else{
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

}
