using UnityEngine;
using System.Collections;

public class SymbolGenerator : MonoBehaviour 
{
	float counter; 

	float interval1;
	float length1;

	float interval2;
	float length2;

	float interval3;
	float length3;

	float interval4;
	float length4;

	float interval5;
	float length5;

	float interval6;
	float length6;

	public float interval;

	public float totalSongLength;


	bool[][] patterns;
	string[] poses;

	public bool leftUp;
	public bool rightUp;

	AudioSource gameAudioMusic;
	float LastMusicTime = 0.0f;

	public float musicDeltaTime;
	public float musicFreq;

	ScoreManager scoreManager;


	void Awake () 
	{
		scoreManager = (ScoreManager) FindObjectOfType<ScoreManager>();

		gameAudioMusic = GameObject.FindGameObjectWithTag("LevelMusic").GetComponent<AudioSource>();


		interval1 = 60*3*1000/65;
		length1 = interval1*4;

		interval2 = 60*3*1000/70;
		length2 = interval2*4;

		interval3 = 60*3*1000/78;
		length3 = interval3*4;

		interval4 = 60*3*1000/85;
		length4 = interval4*4;

		interval5 = 60*3*1000/92;
		length5 = interval5*4;

		interval6 = 60*3*1000/100;
		length6 = interval6*8;

		interval = interval1;

		totalSongLength = length1 + length2 + length3 + length4 + length5 + length6;


		musicDeltaTime = 0;
		musicFreq = 0;
	}

	// Use this for initialization
	void Start () 
	{
		counter = 0.0f; 

		leftUp = false;
		rightUp = false;

		patterns = new bool[][] { 
			new bool[] {false, false}, 
			new bool[] {false, true}, 
			new bool[] {true, false}, 
			new bool[] {true, true} 
		};

		poses = new string[]{
			"down_down",
			"down_up",
			"up_down",
			"up_up"
		};
	}


	// Update is called once per frame
	void Update () 
	{	

		if (scoreManager.roundStarted) {

			//to sink stuff to audio http://answers.unity3d.com/questions/228888/perfect-synch-with-music.html

			musicDeltaTime = (gameAudioMusic.timeSamples - LastMusicTime);
			musicFreq = gameAudioMusic.clip.frequency;
			counter += musicDeltaTime * 1000 / musicFreq;
			     
			if (counter >= interval)
			{
				counter = 0;
				generateSymbol();
			} 

			if (gameAudioMusic.timeSamples >= (length1 + length2 + length3 + length4 + length5) / 1000 * gameAudioMusic.clip.frequency) 
			{
				// Debug.Log("interval");
				interval = interval6;
			}
			else if (gameAudioMusic.timeSamples >= (length1 + length2 + length3 + length4) / 1000 * gameAudioMusic.clip.frequency) 
			{
				// Debug.Log("interval");
				interval = interval5;
			}
			else if (gameAudioMusic.timeSamples >= (length1 + length2 + length3) / 1000 * gameAudioMusic.clip.frequency) 
			{
				// Debug.Log("interval");
				interval = interval4;
			}
			else if (gameAudioMusic.timeSamples >= (length1 + length2) / 1000 * gameAudioMusic.clip.frequency) 
			{
				// Debug.Log("interval");
				interval = interval3;
			}
			else if (gameAudioMusic.timeSamples >= (length1) / 1000 * gameAudioMusic.clip.frequency) 
			{
				// Debug.Log("interval");
				interval = interval2;
			}

			LastMusicTime = gameAudioMusic.timeSamples;
		}
	}

	void generateSymbol()
	{
		int randIndex = Mathf.RoundToInt(Random.value * 3.0f);
		leftUp = patterns[randIndex][0];
		rightUp = patterns[randIndex][1];

		//send randIndex to priest
		//send left and right to AI
		//Debug.Log("Left up: " + leftUp + "; Right up: " + rightUp );

		//play priest's movement
		Animation anim = this.GetComponentInChildren<Animation>();
		anim.Play(poses[randIndex]);
	}

}
