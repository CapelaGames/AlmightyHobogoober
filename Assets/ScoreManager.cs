using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class ScoreManager : MonoBehaviour 
{

	SymbolGenerator symbol;
	PlayerManager pm;

	int playerCount;
	int followerCount;

	float timeleft; 
	float badScore;
	//for score shake
	float shakeSize;

	bool prevLeft;
	bool prevRight;

	GameObject scoreObject;
	Text scoreText;
	float origScorePos;

	public float TimeLimit; //Seconds

	public float InnocentPenalty = 30f; //Percent 30f = 30%
	// public int minutes;
	// public int seconds;
	int minutes;
	int seconds;

	bool hasWinner = false;

	public bool roundStarted;


	public float HereticFailPoints = 6f;
	public float DelayTillGameStart = 7f; //seconds

	AudioSource gameAudioMusic;


	// Use this for initialization
	void Start()
	{
		roundStarted = false;

		badScore = 0f;
		shakeSize = 0f;

		minutes = 0;
		seconds = 0;

		prevLeft = false;
		prevRight = false;

		symbol = FindObjectOfType<SymbolGenerator>();
		pm = FindObjectOfType<PlayerManager>();

		TimeLimit = symbol.totalSongLength/1000;
		timeleft = TimeLimit;

		scoreObject = GameObject.FindWithTag("BadScore");
		scoreText = scoreObject.GetComponent<Text>();
		origScorePos = scoreObject.transform.position.y;

		gameAudioMusic = GameObject.FindGameObjectWithTag("LevelMusic").GetComponent<AudioSource>();

		StartCoroutine(StartDoingCoroutine());
	}

	
	IEnumerator StartDoingCoroutine () 
	{
		yield return new WaitForSeconds( DelayTillGameStart );   //Wait

		roundStarted = true;
		gameAudioMusic.Play();

		Debug.Log("start game");

	}

	bool isWaitingOnWin = false;
	 IEnumerator waitTillWin()
	{
		isWaitingOnWin = true;
		yield return new WaitForSeconds(3f);
		isWaitingOnWin = false;
		this.GodWin();


	}
	// Update is called once per frame
	void Update () 
	{	
		if (roundStarted) {
			playerCount = pm.ConnectedPlayerList.Count;
			followerCount = pm.FollowersList.Length;

			bool right = symbol.rightUp; //instructor has right hand up
			bool left = symbol.leftUp; //instructor has left hand up

			bool resetArmMoved = false; // Set this to only check arm moved if the priest has changed directions. Else it will trigger a false positive, when armMoved is true, but then different after priest changes.

			if (prevLeft != left || prevRight != right) //if true, then priest has moved
			{	
				resetArmMoved = true;
				prevLeft = left;
				prevRight = right;
			}

			if (symbol.musicFreq != 0) {
				timeleft -= symbol.musicDeltaTime / symbol.musicFreq;
			}

			foreach(GameObject playerObject in pm.FollowersList) 
			{
				Player player = playerObject.GetComponent<Player>();

				if (resetArmMoved)
				{
					player.armMoved = false;
				}

				if (player.armMoved && (left != player.IsLeftArmRaised && right != player.IsRightArmRaised)) //if a players key was pressed (arm moved), and one of the arms is wrong, score
				{
					player.armMoved = false;
					this.ScoreBad(HereticFailPoints / playerCount);
					//Debug.Log("bad score: " + badScore);
				}
			}

			if (timeleft <= 0) {
				timeleft = 0;
				if(isWaitingOnWin == false)
				{
					GetComponent<AudioSource>().Play();
					StartCoroutine("waitTillWin");
				}
			}

		}



		//draw time
		string minutes = Mathf.Floor(timeleft / 60).ToString("00");
		string seconds = (timeleft % 60).ToString("00");
		Text timeText = GameObject.FindWithTag("TimeLimit").GetComponent<Text>();
		timeText.text = minutes + ":" + seconds;

		//calc score shake
		shakeSize += (0f - shakeSize) * 0.1f;
		float shakeShift = 0f;
		if (shakeSize > 0f) {
			shakeShift = Mathf.Sin(Time.time * 50) * shakeSize;
		} else {
			shakeSize = 0f;
		}

		if (badScore >= 100) {
			badScore = 100;
			shakeSize = 10;
			this.PlayersWin();
		}

		//draw score
		scoreText.text = Mathf.Floor(badScore * 10)/10 + "%";
		
		if (shakeShift != 0f) 
		{
			float newX = scoreObject.transform.position.x;// + shakeShift;
			float newY = origScorePos + shakeShift;
			float newZ = scoreObject.transform.position.z;
			scoreText.transform.position = new Vector3(newX, newY, newZ);
			scoreText.transform.Rotate(Vector3.forward * shakeShift * 2, Space.World);
			scoreText.transform.localScale = new Vector3(scoreText.transform.localScale.x + shakeShift/20, scoreText.transform.localScale.x + shakeShift/20, scoreText.transform.localScale.x + shakeShift/20);		

		}

		//always ease back
		float targetScale = scoreText.transform.localScale.z + ((1f - scoreText.transform.localScale.z) * 0.5f);
		scoreText.transform.localScale = new Vector3(targetScale, targetScale, targetScale);

		Vector3 targetRotate = new Vector3(scoreText.transform.rotation.x, scoreText.transform.rotation.y, (0 - scoreText.transform.rotation.z) * 0.1f);
		scoreText.transform.Rotate(targetRotate, Space.World);
		

	}

	void ScoreBad(float v) 
	{
		badScore += v;
		if (shakeSize < 1) {
			shakeSize += 3;
		} else {
			shakeSize += 3;	
		}
	}


	public void PlayersWin() {
		Application.LoadLevel("WinHeretics");
	}
	public void GodWin() {
		Application.LoadLevel("WinHobo");
	}

	public void PunishGod() {
		shakeSize += 20;
		badScore += InnocentPenalty;
	}


}
