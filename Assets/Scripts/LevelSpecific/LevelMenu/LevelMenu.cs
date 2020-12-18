using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMenu : MonoBehaviour
{
    public GameObject[] LevelSwitch;

    public GameObject beggining;
    public GameObject Loadlvl;
    public Colliderscript col1;
    

    private List<SwitchScript> triggers;

    private LevelLoderScript lvl_loader;
    private float timer = 0.0f;


    void Start()
    {
        lvl_loader = GameObject.FindGameObjectWithTag("LvlLoader").GetComponent<LevelLoderScript>();

        triggers = new List<SwitchScript>();
        triggers.Clear();
        for (var i = 0; i < LevelSwitch.Length; i++)
        {
            try{
                triggers.Add(LevelSwitch[i].transform.Find("trigger").GetComponent<SwitchScript>());
            }catch{
                triggers.Add(null);
            }    
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            beggining.SetActive(false);
        }
        
        if (triggers[0].Switch_on)
        {
            Loadlvl.SetActive(true);
            timer += Time.deltaTime;
            if(timer >= 0.5f)
            {
                timer = 0.0f;
                lvl_loader.LoadLevel(1);
            }
        }
        else if (triggers[2].Switch_on)
        {
            Loadlvl.SetActive(true);
            timer += Time.deltaTime;
            if(timer >= 0.5f)
            {
                timer = 0.0f;
                lvl_loader.LoadLevel(2);
            }
        }
        else if (triggers[3].Switch_on)
        {
            Loadlvl.SetActive(true);
            timer += Time.deltaTime;
            if(timer >= 0.5f)
            {
                timer = 0.0f;
                lvl_loader.LoadLevel(3);
            }
        }

        if(col1.triggered)
        {
            lvl_loader.ExitLevel();
            return;
        }

    }
}
