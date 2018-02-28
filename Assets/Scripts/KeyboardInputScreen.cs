using UnityEngine;
using System.Collections;

public class KeyboardInputScreen : MonoBehaviour 
{
	public ControlsScreenControls playerManager;
	// Use this for initialization
	void Start () 
	{
		if(playerManager == null)
		{
			Debug.LogError("NO PLAYER MANAGER ATTACHED TO KEYBOARDINPUT");
		}
	}

	void SendPlayerManagerKey(string PlayerID, bool isLeft, bool isDown)
	{
		playerManager.SendMoveToPlayer(PlayerID, isLeft, isDown);
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Player 1
		if(Input.GetKeyDown(KeyCode.Q))
		{
			//Debug.Log("Q Down");
			playerManager.SendMoveToPlayer("Player1", true, true);
		}

		if(Input.GetKeyDown(KeyCode.W))
		{
			//Debug.Log("W Down");
			playerManager.SendMoveToPlayer("Player1", false, true);
		}

		if(Input.GetKeyUp(KeyCode.Q))
		{
			//Debug.Log("Q Up");
			playerManager.SendMoveToPlayer("Player1", true, false);
		}
		
		if(Input.GetKeyUp(KeyCode.W))
		{
			//Debug.Log("W Up");
			playerManager.SendMoveToPlayer("Player1", false, false);
		}





		//Player 2
		if(Input.GetKeyDown(KeyCode.O))
		{
			//Debug.Log("O Down");
			playerManager.SendMoveToPlayer("Player2", true, true);
		}
		
		if(Input.GetKeyDown(KeyCode.P))
		{
			//Debug.Log("P Down");
			playerManager.SendMoveToPlayer("Player2", false, true);
		}

		if(Input.GetKeyUp(KeyCode.O))
		{
			//Debug.Log("O Down");
			playerManager.SendMoveToPlayer("Player2", true, false);
		}
		
		if(Input.GetKeyUp(KeyCode.P))
		{
			//Debug.Log("P Down");
			playerManager.SendMoveToPlayer("Player2", false, false);
		}




		//Player 3
		if(Input.GetKeyDown(KeyCode.V))
		{
			//Debug.Log("V Down");
			playerManager.SendMoveToPlayer("Player3", true, true);
		}
		
		if(Input.GetKeyDown(KeyCode.B))
		{
			//Debug.Log("B Down");
			playerManager.SendMoveToPlayer("Player3", false, true);
		}

		if(Input.GetKeyUp(KeyCode.V))
		{
			//Debug.Log("V Down");
			playerManager.SendMoveToPlayer("Player3", true, false);
		}
		
		if(Input.GetKeyUp(KeyCode.B))
		{
			//Debug.Log("B Down");
			playerManager.SendMoveToPlayer("Player3", false, false);
		}


	}

}
