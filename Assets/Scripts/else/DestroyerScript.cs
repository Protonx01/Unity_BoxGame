using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerScript : MonoBehaviour
{
    public float time = 5f;
    
    void Update()
    {
        Destroy(gameObject, time);
    }
}
