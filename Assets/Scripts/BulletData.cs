﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletData : MonoBehaviour {

	public float damage = 0f;
	public string parentShip = "";
	public GameObject parent;
	public GameObject sparksPrefab;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void updateParentDamageDealt()
	{
		if(parent!= null) parent.GetComponent<EnemyBrain>().updateDamageDealt(damage);
	}

	public void explode()
	{
		GameObject sparks = Instantiate (sparksPrefab, transform.position, transform.rotation);
		sparks.GetComponent<ParticleSystem>().Play();
		Destroy (sparks, 0.2f);
		Destroy (gameObject, 0.1f);
	}

}
