﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GAmanager : MonoBehaviour {

    int populationSize=40;
    string path = "D:/Uni/IGGI/courses/Goldsmiths module 1/IGGI_Game/Assets/test.txt";
    StreamWriter writer;

    //create a new game object to store the list of enemy clones
    public GameObject enemyManager;
    public List<GameObject> enemyClones;

    //stupid way of making the testGA run one sigle 
    //time after the list of enemyclones has been created (that's why it can not be placed in Start() )
    //creates a bool variable that is set to true in start when the game starts. it's set to false when the function is first call
    //and in LateUpdate it's used in if statement to call/not call the testGA function
    public bool callFunction;
        
        // Use this for initialization
    void Start () {
        callFunction = true;
        writer = System.IO.File.AppendText(path);
       // writer = new StreamWriter(path, true);
    }
	
	// Update is called once per frame
	void LateUpdate () {
        enemyClones = enemyManager.GetComponent<EnemyManager>().enemies;
        if (callFunction) testGA();
    }

    private void OnDestroy()
    {
        writer.Close();
    }


    public void testGA()
    {
        callFunction = false;
        
        //Debug.Log("Start the testGA");
        //create population
        GApopulation testPopulation = new GApopulation(enemyClones); //this will also create a random population of enemies;
        Debug.Log("Count: "+testPopulation.getDictionary().Count);
        Debug.Log("clonesCount: "+ enemyClones.Count);

        Debug.Log("enemy0s: "+ enemyClones[0].GetComponent<EnemyBrain>().speed);
        Debug.Log("enemy0spop: " + testPopulation.getDictionary()[0].getSpeed());

        Debug.Log("enemy1s: "+ enemyClones[1].GetComponent<EnemyBrain>().speed);
        Debug.Log("enemy1spop: " + testPopulation.getDictionary()[1].getSpeed());

        //Debug.Log("enemy0p: " + enemyClones[0].GetComponent<EnemyBrain>().playerFleeBuffer);
        Debug.Log("enemy0ppop: " + testPopulation.getDictionary()[0].getPlayerFleeBuffer());

        //Debug.Log("enemy1p: " + enemyClones[1].GetComponent<EnemyBrain>().playerFleeBuffer);
        Debug.Log("enemy1ppop: " + testPopulation.getDictionary()[1].getPlayerFleeBuffer());




        ////PrintFitness(testPopulation);
        //Debug.Log("3");

        int totalGenerations = 30;
        for (int i= 0; i < totalGenerations;i++)
        {
            testPopulation.nextGeneration();
        }
        //Debug.Log("aenemy0s: " + enemyClones[0].GetComponent<EnemyBrain>().speed);
        Debug.Log("aenemy0spop: " + testPopulation.getDictionary()[0].getSpeed());

        //Debug.Log("aenemy1s: " + enemyClones[1].GetComponent<EnemyBrain>().speed);
        Debug.Log("aenemy1spop: " + testPopulation.getDictionary()[1].getSpeed());

        //Debug.Log("aenemy0p: " + enemyClones[0].GetComponent<EnemyBrain>().playerFleeBuffer);
        Debug.Log("aenemy0ppop: " + testPopulation.getDictionary()[0].getPlayerFleeBuffer());

        //Debug.Log("aenemy1p: " + enemyClones[1].GetComponent<EnemyBrain>().playerFleeBuffer);
        Debug.Log("aenemy1ppop: " + testPopulation.getDictionary()[1].getPlayerFleeBuffer());

        ///testPopulation.nextGeneration();
        ////PrintFitness(testPopulation);


        //testPopulation.nextGeneration();
        //PrintFitness(testPopulation);



        //next generation
        //next generation
    }

    //for test only
    public void PrintFitness(GApopulation p)
    {
        //Debug.Log("PrintCalled1");
        for (int index = 0; index < p.getDictionary().Count; index++)
        {
            Debug.Log(index + " gene: " );
            PrintCollectionToFile(p.getDictionary()[index].GetGene());

            //print(System.String.Join("; ", p.getDictionary()[index].GetGene()));
            //Debug.Log(index + " fitness: " + p.getDictionary()[index].getFitness()+"dic size:"+ p.getDictionary().Count);
            //Debug.Log("");
        }
    }

    public void PrintCollectionToFile<T>(IEnumerable<T> col)
    {
        string currentInfo="next: ";

        int index = 0;
        foreach (var item in col)
        {
            Debug.Log(index+"-"+item); // Replace this with your version of printing
            //File.WriteAllText(path, index + item.ToString());
            currentInfo = currentInfo + index +":"+ item.ToString()+ "; ";
            index++;
        }

        
        writer.WriteLine(currentInfo);
        
    }
}
