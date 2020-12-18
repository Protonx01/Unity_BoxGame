using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class feetScript : MonoBehaviour
{
   
    private GameObject parent;
    private float feet_distance;

    void Start()
    {
        parent = gameObject.transform.parent.gameObject;
        feet_distance = 0.7963f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(parent.transform.position.x, parent.transform.position.y - feet_distance, transform.position.z);

    }
}
