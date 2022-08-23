using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Manager : MonoBehaviour
{

	public List<GameObject> LevelList;
	public int ScoreLimit,index;
    public static bool IsLevel;
    public GameObject BackBoard;
    
    void Start()
    {
        IsLevel = false;
    	index = 0;
    	SetActiveFalse();
    	ScoreLimit = Random.Range(14,25);
    	Debug.Log("Current Score Limit Start"+ScoreLimit.ToString());
        
    }

    void Update()
    {
        int Score = BasketDection.Score;
        if(Score > ScoreLimit)
        {
            IsLevel = true;
        	LevelList[index].SetActive(false);
        	index = Random.Range(0,2);
        	LevelList[index].SetActive(true);
        	ScoreLimit +=Random.Range(5,10);
        	Debug.Log("Current Score Limit "+ScoreLimit.ToString());
        }
        
        if(GamePlayController.LifeCountStatus<=-1)
           SetActiveFalse();
        
    }
    void SetActiveFalse()
    {
    	foreach (GameObject go in LevelList)
        {
        	go.SetActive(false);
        }
    }
}
