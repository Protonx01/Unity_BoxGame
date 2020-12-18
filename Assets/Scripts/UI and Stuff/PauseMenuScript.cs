using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    private LevelLoderScript lvl_loader;

    void Start()
    {
        lvl_loader = GameObject.FindGameObjectWithTag("LvlLoader").GetComponent<LevelLoderScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1f;
            gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1f;
            gameObject.SetActive(false);
            lvl_loader.ReloadLevel();
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Time.timeScale = 1f;
            gameObject.SetActive(false);
            lvl_loader.LoadMenu();
        }
    }
}
