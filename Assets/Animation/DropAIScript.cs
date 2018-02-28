using UnityEngine;
using System.Collections;

public class DropAIScript : MonoBehaviour 
{

	ScoreManager scoreManager;

	public bool isDropped = true;
	bool isPlayed = false;

	bool isStandAfterPlayer = false;
	void Awake()
	{

	}

	// Use this for initialization
	void Start () 
	{
		scoreManager = (ScoreManager) FindObjectOfType<ScoreManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(isPlayed == false && isDropped == true)
		{

			if (scoreManager.roundStarted)  //wait for game to start
			{
				isPlayed = true;
				GetComponent<Animation>().Play("DroppedAi");
				// GetComponent<Animation>().Sample();
				// GetComponent<Animation>().Stop();

				//StartCoroutine("WaitForDrop");
				Debug.Log("dropping");

			} else {
				GetComponent<Animation>().Play("DroppedAi");
				GetComponent<Animation>().Sample();
				GetComponent<Animation>().Stop();
			}
		}

		if(isPlayed == true && isStandAfterPlayer == false)
		{
			StartCoroutine("WaitForStand");
			isStandAfterPlayer = true;
		}
	}

	IEnumerator WaitForStand()
	 {
	 	yield return new WaitForSeconds(1f);
		GetComponent<Animation>().Play("down_down");
	 }
}
