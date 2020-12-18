using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InactiveDollScript : MonoBehaviour
{
    public Sprite ActiveboxSprite;
    public PhysicsMaterial2D Material;
    [SerializeField] public LayerMask WhatIsGround;

    [HideInInspector] 
    public bool FirstRayHit = false;

    private Transform GroundCheck;
    private SpriteRenderer sprite;
    private Rigidbody2D rigid;
    private CharacterController2D controller;
    private ActiveDollScript active;

    private float timer1 = 0.0f;
    private bool bodytype_done = false;


    void Start()
    {
        GroundCheck = gameObject.transform.GetChild(0);
        
        sprite = gameObject.GetComponent<SpriteRenderer>();
        rigid = gameObject.GetComponent<Rigidbody2D>();
        active = gameObject.GetComponent<ActiveDollScript>();
        controller = gameObject.GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (FirstRayHit)
        {
            sprite.sprite = ActiveboxSprite;
            
            rigid.bodyType = RigidbodyType2D.Dynamic;
            rigid.gravityScale = 4;
            rigid.sharedMaterial = Material;
            rigid.useAutoMass = true;
            rigid.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

            active.enabled = true;
            controller.enabled = true;
            gameObject.tag = "DollActive";
            gameObject.name = "ControlledCube";

            FirstRayHit = false;
            bodytype_done = false;
            this.enabled = false;
        }
    }

    private void FixedUpdate() {
        
        if (!bodytype_done)
        {
            var collider = Physics2D.OverlapCircle (GroundCheck.position, 0.1f, WhatIsGround);
            if (collider)
            {
                timer1 += Time.fixedDeltaTime;
                if (timer1 >= 0.5f)
                {
                    rigid.bodyType = RigidbodyType2D.Static;
                }
                if (timer1 >= 0.9f)
                {
                    rigid.bodyType = RigidbodyType2D.Dynamic;
                    rigid.gravityScale = 3;
                    rigid.sharedMaterial = Material;
                    rigid.useAutoMass = false;
                    rigid.mass = 50f;
                    rigid.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
                    bodytype_done = true;
                }
            }
            else{
                timer1 = 0.0f;
            }

        }
        
    }
    

}
