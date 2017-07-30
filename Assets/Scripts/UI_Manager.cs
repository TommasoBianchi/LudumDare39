using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour {

    public Witch witch;
    public Player player;
    public Slider pLifeBar;
    public Slider wLifeBar;
    public Slider sBar;
    public Text sAmount;
    public Canvas gameUI;
    public Canvas PauseCanvas;

    bool paused;


    // Use this for initialization
    void Start () {
        paused = false;
	}
	
	// Update is called once per frame
	void Update () {
        UpdatePLifeBar();
        UpdateWLifeBar();
        UpdateSBar();
        UpdateSAmount();
        if (Input.GetKeyDown("escape") && !paused)
        {
            Time.timeScale = 0;
            gameUI.GetComponent<Canvas> ().enabled = false;
            PauseCanvas.GetComponent<Canvas>().enabled = true;
            paused = true;
        }

        else if (Input.GetKeyDown("escape") && PauseCanvas.GetComponent<Canvas>().enabled == true)
        {
            Unpause();
        }
	}
    
    void UpdatePLifeBar ()
    {
        pLifeBar.value = (player.hp * 100) / player.maxHp;
    }

    void UpdateWLifeBar ()
    {
        wLifeBar.value = (witch.hp * 100) / witch.maxHp;
    }
     
    void UpdateSBar ()
    {
        sBar.value = (witch.souls * 100) / witch.maxSoulNumber;
    }

    void UpdateSAmount ()
    {
        sAmount.text = witch.souls + "/" + witch.maxSoulNumber;
    }

    public void Unpause ()
    {
        paused = false;
        gameUI.GetComponent<Canvas>().enabled = true;
        PauseCanvas.GetComponent<Canvas>().enabled = false;
        Time.timeScale = 1;
    }  
}
