using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    private float timeRemaining = 30F;
    private float timeToMatching;
    private const float MatchingTime = 5F;

    private GameObject planeR;
    private GameObject planeL;
    private Airplane rightPlane;
    private Airplane leftPlane;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Matching started");
        planeR = GameObject.Find("PlaneR");
        planeL = GameObject.Find("PlaneL");

        rightPlane = planeR.GetComponent <Airplane> ();
        leftPlane = planeL.GetComponent <Airplane> ();

        timeToMatching = MatchingTime;

    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0) {
            //Debug.Log("Remaining time"+timeRemaining);
            timeRemaining-=Time.deltaTime;
        } 
        if(timeRemaining <= 0) { 
             GameOver(); 
        }

         //Debug.Log("RightPlane position:" + planeR.transform.position[1]);
         //Debug.Log("LeftPlane position:" + planeL.transform.position[1]);

         if((planeR.transform.position[1] - planeL.transform.position[1]) < 0.5){
            timeToMatching-=Time.deltaTime; 
            if(timeToMatching <= 0){
                GameOver();
            }
        }
        else{
            timeToMatching = MatchingTime;
        }

    }

    void GameOver(){
        //Time.timeScale = 0;
        Debug.Log("Game over");
        ApplicationController.GetInstance().OnMatchingFinish();
        //Application.Quit();
    }
}
