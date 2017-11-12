﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour {
    
    public float topSpeed;
	public float decel;
	public float accel;
	public float turnSpeed;
    public float maxTurn;
	public float health;

	public float bulletDamage;
	public float bulletSpeed;
	public GameObject bulletPreFab;

    public GameObject LThruster;
    public GameObject RThruster;

	private float currentSpeed = 0;
	private float currentTurn = 0;

	public Text playerHealthText;
	
	void Awake(){}
    

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(health <= 0) SceneManager.LoadScene ("GameOver");
		
		
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		currentSpeed += v * Time.fixedDeltaTime * accel;
		currentSpeed -= decel * Time.fixedDeltaTime;

		if (currentSpeed > topSpeed)currentSpeed = topSpeed;
		if(currentSpeed < 0) currentSpeed = 0;
		Debug.Log ("Speed: " + currentSpeed + " Turn: " + currentTurn);

		currentTurn += h * Time.fixedDeltaTime * turnSpeed;
		if (currentTurn > 0) currentTurn -= decel * Time.fixedDeltaTime;
		else if (currentTurn < 0) currentTurn += decel * Time.fixedDeltaTime;

		if (currentTurn > maxTurn ) currentTurn = maxTurn;
		if (currentTurn < -maxTurn) currentTurn = -maxTurn;

		thrust(currentSpeed);
		turn(currentTurn);

		if (Input.GetKeyDown (KeyCode.Space)) {
			fire ();
		}

        handleThrusterEffect();
		updatePlayerHeathText();

    }


	private void thrust(float amount)
	{
		transform.position += transform.forward * Time.fixedDeltaTime * -amount;
	}

	private void turn(float amount)
	{
		float yaw = currentTurn * Time.fixedDeltaTime;
		transform.Rotate (0, yaw, 0);
	}

	public void fire()
	{
		GameObject bullet = Instantiate (bulletPreFab, transform.position, transform.rotation);
		bullet.GetComponent<Rigidbody> ().velocity = (bullet.transform.forward * -bulletSpeed )+ GetComponent<Rigidbody>().velocity; 
		bullet.GetComponent<BulletData>().damage = bulletDamage;
		bullet.GetComponent<BulletData>().parentShip = "Player";
		Destroy (bullet, 2.0f);
	}

    public void handleThrusterEffect()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (v == 0 && h == 0)
        {
            LThruster.GetComponent<ParticleSystem>().Stop();
            RThruster.GetComponent<ParticleSystem>().Stop();
        }
        else if (v > 0 && h == 0)
        {
            LThruster.GetComponent<ParticleSystem>().Play();
            RThruster.GetComponent<ParticleSystem>().Play();
        }
        else if (v > 0 && h < 0)
        {
            LThruster.GetComponent<ParticleSystem>().Stop();
            RThruster.GetComponent<ParticleSystem>().Play();
        }
        else if (v > 0 && h > 0)
        {
            LThruster.GetComponent<ParticleSystem>().Play();
            RThruster.GetComponent<ParticleSystem>().Stop();
           // print(h);
        }
        else if (v == 0 && h > 0)
        {
            LThruster.GetComponent<ParticleSystem>().Play();
            RThruster.GetComponent<ParticleSystem>().Stop();
           // print(h);
        }
        else if (v == 0 && h < 0)
        {
            LThruster.GetComponent<ParticleSystem>().Stop();
            RThruster.GetComponent<ParticleSystem>().Play();
           // print(h);
        }
    }


	void OnTriggerEnter(Collider collision)
	{
		if(collision.gameObject.name.Contains("Bullet") && collision.gameObject.GetComponent<BulletData>().parentShip != "Player")
		{
			float damage = collision.gameObject.GetComponent<BulletData>().damage;
			collision.gameObject.GetComponent<BulletData>().updateParentDamageDealt();
			health -= damage;
			Destroy(collision.gameObject);
		}

		if(collision.gameObject.name.Contains("Enemy(Clone)"))
		{
			float damage = collision.gameObject.GetComponent<EnemyBrain>().health;
			health -= damage;
			
			collision.gameObject.GetComponent<EnemyBrain>().health = 0;
		}
	}

	 void updatePlayerHeathText()
	 {
		 playerHealthText.text = "Heath: " + health;
	 }

}
