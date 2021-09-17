using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallClimbingSlider : MonoBehaviour {

    public Slider wallClimbingSlider;
    public BasePlayerMovement playerMovement;

    // Use this for initialization
    void Start ()
    {
        wallClimbingSlider = transform.GetComponent<Slider>();
        playerMovement = GameObject.Find("TestCharacter").GetComponent<BasePlayerMovement>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        wallClimbingSlider.minValue = 0;
        wallClimbingSlider.value = playerMovement.wallClimbTime;
    }
}
