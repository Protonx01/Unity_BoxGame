using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinisherScript : MonoBehaviour
{
    public Animator filling;
    
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    private Color base_color;
    private Color transparent;
    private Color trigger_color;
    private bool finish_hit = false;
    private bool all_done = false;
    private LevelLoderScript lvl_loader;
    private GameObject canvas;

    private float timer = 0.0f;
    private float timer1 = 0.0f;

    void Start()
    {
        sprite  = gameObject.GetComponent<SpriteRenderer>();
        coll = gameObject.GetComponent<BoxCollider2D>();
        canvas = transform.Find("Canvas").gameObject;

        // try{
        //     lvl_master = GameObject.FindGameObjectWithTag("LevelMaster").GetComponent<CheckpointLoaderScript>();
        // }
        // catch{}
        lvl_loader = GameObject.FindGameObjectWithTag("LvlLoader").GetComponent<LevelLoderScript>();

        base_color = sprite.color;
        transparent = new Color(0,0,0,0);
        ColorUtility.TryParseHtmlString("#EF4747", out trigger_color); //? red
    }

    // Update is called once per frame
    void Update()
    {
        if (finish_hit && !all_done)
        {
            // sprite.color = transparent;
            timer1 += Time.deltaTime;
            var lerped_color = Color.Lerp (base_color, trigger_color, timer1 + 0.1f);
            sprite.color = lerped_color;
            if (timer1 >= 2f)
            {
             
                //* change Scene

                finish_hit = false;
                all_done = true;

                // rigid.mass = 50f;
                // rigid.gravityScale = 5;

                
                lvl_loader.LoadMenu();
            }
            
        }

        if (Input.GetKey(KeyCode.Backspace))
        {
            filling.SetBool("fill_up", true);
            timer += Time.deltaTime;
            if(timer >= 1f)
            {
                lvl_loader.LoadCheckpoint();
                timer = 0.0f;
            }
        }
        if (Input.GetKeyUp(KeyCode.Backspace))
        {
            timer = 0.0f;
            filling.SetBool("fill_up", false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.SetActive(true);
            Time.timeScale = 0.1f;
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.CompareTag("Player"))
        {
            finish_hit = true;
        }
    }

    private void OnCollitionExit2D(Collider2D other) {
        
        if (other.CompareTag("Player"))
        {
            sprite.color = base_color;
            timer1 = 0.0f;
        }
    }

    
}
