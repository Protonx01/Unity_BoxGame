using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveDollScript : MonoBehaviour
{
   
    public float movement_speed;
    public Sprite InactiveboxSprite;
    public PhysicsMaterial2D Material;

    private float move_amount;
    private bool jump = false;

    [HideInInspector]
    public bool connected = true;

    private SpriteRenderer sprite;
    private Rigidbody2D rigid;
    private CharacterController2D controller;
    private InactiveDollScript inactive;
    
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        rigid = gameObject.GetComponent<Rigidbody2D>();
        inactive = gameObject.GetComponent<InactiveDollScript>();
        controller = gameObject.GetComponent<CharacterController2D>();
    }

   
    void Update()
    {
        if (!connected)
        {
            sprite.sprite = InactiveboxSprite;

            rigid.gravityScale = 3;
            rigid.sharedMaterial = Material;
            rigid.useAutoMass = false;
            rigid.mass = 50f;
            rigid.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

            inactive.enabled = true;
            controller.enabled = false;
            gameObject.tag = "DollsInactive";
            gameObject.name = "DollCube";

            connected = false;
            this.enabled = false;
        }

        if (Input.GetKey(KeyCode.D))
        {
            move_amount = movement_speed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            move_amount = -movement_speed;
        }
        else{
            move_amount = 0;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            jump = true;
        }
    }

    private void FixedUpdate() {
        controller.Move (move_amount * Time.deltaTime, false, jump);
        jump = false;
        
    }

    
}

