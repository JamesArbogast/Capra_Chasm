using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour {

    public Slider healthBarSlider;
    public PlayerStats playerStats;

    // Use this for initialization
    void Start ()
    {
        healthBarSlider = transform.GetComponent<Slider>();
        playerStats = GameObject.Find("TestCharacter").GetComponent<PlayerStats>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        healthBarSlider.minValue = 0;
        healthBarSlider.maxValue = playerStats.maxPlayerHealth;
        healthBarSlider.value = playerStats.currentPlayerHealth;
	}
}
