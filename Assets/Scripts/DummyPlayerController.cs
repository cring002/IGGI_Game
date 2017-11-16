﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPlayerController : MonoBehaviour {
	public float health;
	public float speed;
	public float damage;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		//changed colour based on gene (speed and bullet speed) information
		transform.GetChild(0).GetChild(0).gameObject.GetComponent<MeshRenderer>().materials[1].color = new Color(ValueRemapping( damage, 100, 225)/225, 20/225, 20/225, 0);
		transform.GetChild(0).GetChild(0).gameObject.GetComponent<MeshRenderer>().materials[3].color = new Color(1, ValueRemapping(speed, 500, 128)/225, 0, 0);
		transform.GetChild(0).GetChild(0).gameObject.GetComponent<MeshRenderer>().materials[0].color = new Color(0, 0, ValueRemapping(health, 1000, 225)/225, 0);
	}

	private static float ValueRemapping(float initialVal, float initialHigh,  float targetHigh)
	{
		return ((initialVal*targetHigh)/initialHigh);
	}
}
