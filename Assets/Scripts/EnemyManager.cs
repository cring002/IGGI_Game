﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour {
    //public PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public GameObject player;
    public GameObject enemy;                // The enemy prefab to be spawned.
	//public float spawnTime = 3f;            // How long between each spawn.
	public int waveSize;					// how many enemies are spawning each wave
    public List<GameObject> enemies=new List<GameObject>();        //dinamic list of enemies
    
    //check health. make the enemy die;

    void Start ()
	{
        //GameObject privateEnemy = enemy;
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        //InvokeRepeating ("Spawn", privateEnemy, spawnTime, 0);//spawnTime);
        Spawn(enemy);
	}

    private void Update()
    {
        //print("enemies.Count:" + enemies.Count);
        //check the enemy health and if it's lower than 0, distroy the player
       if (enemies.Count > 0)
        {
            for(int enemyIndex=enemies.Count-1; enemyIndex>=0; enemyIndex--)
            {
                //print("enemyIndex:" + enemyIndex + " enemies[enemyIndex].GetComponent<EnemyBrain>().health:" + enemies[enemyIndex].GetComponent<EnemyBrain>().health);
                if (enemies[enemyIndex].GetComponent<EnemyBrain>().health <=0)
                {
					enemies[enemyIndex].GetComponent<Rigidbody>().useGravity = true;
                    //Destroy(enemies[enemyIndex]);
                    //enemies.RemoveAt(enemyIndex);
                    //print("destroied an enemy");
                }
				if(enemies[enemyIndex].transform.position.y < -200)
				{
					Destroy(enemies[enemyIndex]);
                    enemies.RemoveAt(enemyIndex);
				}
            }
        } else {
			SceneManager.LoadScene ("NextWave");
		}
    }

    void Spawn (GameObject privateEnemy)
	{
		for (int i = 0; i < waveSize; i++) { 
            // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            GameObject newEnemy = Instantiate(privateEnemy, GenerateRandomTransform(), privateEnemy.GetComponent<Rigidbody>().rotation);
			newEnemy.GetComponent<EnemyBrain>().player = this.player;
			newEnemy.GetComponent<EnemyBrain>().otherEnemies = this.enemies;
            enemies.Add(newEnemy); //adding all enemies created to the list
		}
	}
       
	Vector3 GenerateRandomTransform(){
        Vector3 pos;
        float x = Random.Range(player.transform.position.x-2000, player.transform.position.x + 2000);
        float y = 0f;
        float z = Random.Range(player.transform.position.z - 2000, player.transform.position.z + 2000);
        pos = new Vector3(x, y, z);
        transform.position = pos;

        return pos;
    }

}



