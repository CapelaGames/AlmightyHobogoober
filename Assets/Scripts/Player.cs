using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	string PlayerID = "Default";
	public bool IsLeftArmRaised = false;
	public bool IsRightArmRaised = false;

	public bool isAI = true;

	public bool armMoved = false;

	GameObject Model;

	Animation playerAnimation;

	SymbolGenerator symbolGenerator;


	float AnimationWaitTime= 0.1f;

	//sound Variables
	AudioSource LeftSoundEffect;
	AudioSource RightSoundEffect;
	AudioSource BowSoundEffect;
	float timeOfLastPress = 0.0f;
	float timeAllowTillNextPress = 0.01f;

	public GameObject DeathAnimation;


	public bool dead = false; //used by player manager


	public void SetupPlayer(string playerID)
	{
		PlayerID = playerID;
		isAI = false;

		GetComponentInChildren<DropAIScript>().isDropped = false;

	
	
	}

	
	public void RaiseLeft()
	{
		//Debug.Log("PlayerID:" + PlayerID + " RaiseLeft");
		IsLeftArmRaised = true;
		UpdateAnimation();
		PlaySound();

		armMoved = true;
		timeOfLastPress = Time.time;
	}

	public void RaiseRight()
	{
		//Debug.Log("PlayerID:" + PlayerID + " RaiseRight");
		IsRightArmRaised = true;
		UpdateAnimation();
		PlaySound();

		armMoved = true;
		timeOfLastPress = Time.time;
	}

	public void LowerLeft() 
	{
		//Debug.Log("PlayerID:" + PlayerID + " LowerLeft");
		IsLeftArmRaised = false;
		UpdateAnimation();
		PlaySound();

		timeOfLastPress = Time.time;
	}


	public void LowerRight()
	{
		//Debug.Log("PlayerID:" + PlayerID + " LowerRight");
		IsRightArmRaised = false;
		UpdateAnimation();
		PlaySound();

		timeOfLastPress = Time.time;
	}

	IEnumerator DelayAnimation(float delayedTime)
	{
		yield return new WaitForSeconds(delayedTime);
		UpdateAnimation();
	}


	void Start()
	{
		playerAnimation = GetComponentInChildren<Animation>();

		symbolGenerator = (SymbolGenerator) FindObjectOfType<SymbolGenerator>();

		AudioSource[] soundEffects = GetComponents<AudioSource>();

		LeftSoundEffect = soundEffects[0];
		RightSoundEffect = soundEffects[1];
		BowSoundEffect = soundEffects[2];

		GameObject playerManObject = GameObject.FindWithTag("PlayerMan");

		if(playerManObject != null)
		{
			DeathAnimation =  playerManObject.GetComponent<PlayerManager>().BloodAnimation;
		}
	}

	void Update()
	{
		if(isAI)
		{
			bool isUpdateAnimation = false;
			if(IsLeftArmRaised != symbolGenerator.leftUp)
			{
				IsLeftArmRaised = symbolGenerator.leftUp;
				//UpdateAnimation();
				isUpdateAnimation = true;
			}

			if( IsRightArmRaised != symbolGenerator.rightUp)
			{
				IsRightArmRaised = symbolGenerator.rightUp;
				//UpdateAnimation();
				isUpdateAnimation = true;
			}

			if(isUpdateAnimation)
			{	
				float scale = (symbolGenerator.interval/3) / 1000; //scale is based on tempo. Delay should be 1 beat of a bar of 4
				float lowRange = 0.88f * scale;
				float hiRange = 1.12f * scale;
				float randomDelay = Random.Range(lowRange,hiRange);
				StartCoroutine(DelayAnimation(randomDelay));
			}
		}
	}

	void UpdateAnimation()
	{
		if(IsLeftArmRaised == false && IsRightArmRaised == false)
		{
			//if(!playerAnimation.IsPlaying("down_up"))
			{
				playerAnimation.Play ("down_down");//TODO please fix
				//Debug.Log("down_down");
				//Debug.Log(playerAnimation.name);
			}

		}
		else if(IsLeftArmRaised == true && IsRightArmRaised == true)
		{
			//if(!playerAnimation.IsPlaying("up_up")) //TODO please fix
			{
				playerAnimation.Play ("up_up");
				//Debug.Log("UP_UP");
				//Debug.Log(playerAnimation.name);
			}

		}
		else if(IsLeftArmRaised == false && IsRightArmRaised == true)
		{
			//if(!playerAnimation.IsPlaying("up_down"))
			{
				playerAnimation.Play ("down_up");//TODO please fix
				//Debug.Log("down_up");
				//Debug.Log(playerAnimation.name);
			}
		}
		else //true false
		{
			//if(!playerAnimation.IsPlaying("down_down"))
			{
				playerAnimation.Play ("up_down");//TODO please fix
//				Debug.Log("up_down");
				//Debug.Log(playerAnimation.name);
			}
		}
	}


	void PlaySound()
	{
		if(IsLeftArmRaised)
		{
			LeftSoundEffect.Play();
		}

		if(IsRightArmRaised)
		{
			BowSoundEffect.Play();
		}
	}


	public void KillPlayer()
	{
		gameObject.SetActive(false);

		dead = true;

		if(isAI)
		{
			FindObjectOfType<ScoreManager>().PunishGod();
			//sound
		} 
		else
		{
			//sound
		}

		Quaternion rotationdeath = transform.rotation;
		rotationdeath.y = Random.rotation.y;
		Instantiate(DeathAnimation,transform.position,rotationdeath);

	}
}
