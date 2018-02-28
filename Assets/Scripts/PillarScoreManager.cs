using UnityEngine;
using System.Collections;

//NOT NEEDED
public class PillarScoreManager : MonoBehaviour 
{
	ScoreManager scoreManager;
	// Use this for initialization

	public float MinSize;
	public float MaxSize;




	void Start () 
	{
		scoreManager = (ScoreManager) FindObjectOfType<ScoreManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//scoreManager
	}
}
