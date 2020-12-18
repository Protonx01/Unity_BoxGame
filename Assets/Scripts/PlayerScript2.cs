using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript2 : MonoBehaviour
{
    //! It is just here to separate the movement control from the 
    //! rest of the junk
    
    
    public float movement_speed;
    public GameObject effect;
    public GameObject destroy_sound;

    private float move_amount;
    private bool jump = false;

    private CharacterController2D controller;
    //private CheckpointLoaderScript lvl_master;
    private LevelLoderScript lvl_loader;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Rigidbody2D rigid;

    private bool destroyed = false;

    private float timer = 0.0f;
    
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController2D>();
        coll = gameObject.GetComponent<BoxCollider2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        rigid = gameObject.GetComponent<Rigidbody2D>();
        
        try{
            lvl_loader = GameObject.FindGameObjectWithTag("LvlLoader").GetComponent<LevelLoderScript>(); 
        }catch{}
    }

    // Update is called once per frame
    void Update()
    {
         
        if (destroyed)
        {
         
            timer += Time.deltaTime;
            if(timer >= 1f)
            {
                lvl_loader.LoadCheckpoint();
                controller.enabled = true;
                destroyed = false;
                timer = 0.0f;
            }
        }
        
        //! Movement-----------------
        
        if (Input.GetKey(KeyCode.RightArrow))
            move_amount = movement_speed;
        else if (Input.GetKey(KeyCode.LeftArrow))
            move_amount = -movement_speed;
        else
            move_amount = 0;
        

        if (Input.GetKeyDown(KeyCode.UpArrow))
            jump = true;
        
   
    }

    private void FixedUpdate() {
        controller.Move (move_amount * Time.deltaTime, false, jump);
        jump = false;
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        
        if(other.collider.CompareTag("Spikes"))
        {
            destroyed = true;
            sprite.enabled = false;
            coll.enabled = false;
            controller.enabled = false;
            Instantiate(effect, transform.position, Quaternion.identity);
            Instantiate(destroy_sound, transform.position,Quaternion.identity);
            rigid.bodyType = RigidbodyType2D.Static;
            
           
        }
    }

}
