﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // To manipulate the slider which denotes our speed.

public class GameManager : MonoBehaviour {

	public static GameManager instance; // Singleton design pattern

	public float speed = 1f; // The speed of the player.

	public const float MIN_SPEED = 1/16f; 
	public const float MAX_SPEED = 16f;

    public const float TILT_THRESHHOLD = 0.5f;

	//private const float MIN_Z_TILT = -0.25f;
	//private const float MAX_Z_TILT = -1f;

	private bool isPhone = false;

    //the values of the acceleration to be exposed
    public float Xinput;
    public float Yinput;
    public float Zinput;

	//public Slider speedometer;
	//public Text test;

	// Set up the singleton design pattern
	private void Awake() {
		if (instance == null) {
			instance = this;//setup singleton
		}
	}

	private void Start() {

	}

	private void Update() {
#if UNITY_EDITOR
        //get the inputs directly from the UI
        Xinput = Mathf.Round(UIManager.instance.tiltometer.value * 100.0f) / 100.0f;
        Yinput = 0.0f;
        Zinput = Mathf.Round(UIManager.instance.speedometer.value * 100.0f) / 100.0f;
#else //this would be on a phone
        //get all 3 axis of rotation, rounded to two decimal places
            Xinput = Mathf.Round(-Input.acceleration.x * 100.0f)/100.0f;
            Yinput = Mathf.Round(-Input.acceleration.y * 100.0f)/100.0f;
            Zinput = Mathf.Round(-Input.acceleration.z * 100.0f)/100.0f;
        //call the UI Manager to update the values
            UIManager.instance.UpdateUI();
#endif
    }


	// From https://stackoverflow.com/questions/3451553/value-remapping
	private float Map(float value, float low1, float high1, float low2, float high2) {
		return low2 + (high2 - low2) * ((value - low1) / (high1 - low1));
	}
}
