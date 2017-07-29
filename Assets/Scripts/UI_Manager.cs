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

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        UpdatePLifeBar();
        UpdateWLifeBar();
        UpdateSBar();
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
}
