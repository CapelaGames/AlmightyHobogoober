using UnityEngine;
using System.Collections;

public class ControlsScreenControls : MonoBehaviour 
{
	public Player Character1;
	public Player Character2;
	public Player Character3;

	// Use this for initialization
	void Start () 
	{
		ReplaceAI();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ReplaceAI()
	{
		Character1.SetupPlayer("Player1");
		Character2.SetupPlayer("Player2");
		Character3.SetupPlayer("Player3");
	}

	public void SendMoveToPlayer(string PlayerId, bool isLeft, bool isDown)
	{
		GameObject playerObject;
		Player player;

		if( PlayerId == "Player1")
		{
			player   = Character1;
		}
		else if( PlayerId =="Player2")
		{
			player   = Character2;
		}
		else //if( PlayerId =="Player3")
		{
			player = Character3;
		}
		
		if(isLeft)
		{
			if(isDown)
			{
				player.RaiseLeft();
			}
			else
			{
				player.LowerLeft();
			}
		}
		else
		{
			if(isDown)
			{
				player.RaiseRight();
			}
			else
			{
				player.LowerRight();
			}
		}
	}
}
