using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SprintSlider : MonoBehaviour {

    public Slider sprintSlider;
    public BasePlayerMovement playerMovement;

	// Use this for initialization
	void Start ()
    {
        sprintSlider = transform.GetComponent<Slider>();
        playerMovement = GameObject.Find("TestCharacter").GetComponent<BasePlayerMovement>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        sprintSlider.minValue = 0;
        sprintSlider.value = playerMovement.sprintTime;
	}
}
