using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour 
{
	public GameObject PlayerObjectPrefab;

	public Dictionary<string,GameObject> ConnectedPlayerList;
		
	public GameObject[] FollowersList;

	SpawnPoints spawnPoints;
	Vector3[] points;

	public GameObject BloodAnimation;


	// Use this for initialization
	void Start () 
	{
		if(PlayerObjectPrefab == null)
		{
			Debug.LogError("ERROR NO ATTACHED PLAYEROBJECTPREFAB");
		}
		spawnPoints = (SpawnPoints) FindObjectOfType<SpawnPoints>();
		if(spawnPoints == null)
		{
			Debug.LogError("ERROR CANT FIND SpawnPoints");
		}
		points = spawnPoints.GetCoordinates();

		ConnectedPlayerList = new Dictionary<string, GameObject>();
		FollowersList = new GameObject[points.Length];

		SpawnAI();

		//TODO TEMP

		if(FollowersList.Length != ConnectedPlayerList.Count)
		{
			int AiID;
			do
			{
				AiID = Random.Range (0,FollowersList.Length - 1);
			}while(!FollowersList[AiID].GetComponent<Player>().isAI);

			ReplaceAI(AiID,"Player1");
		}

		if(FollowersList.Length != ConnectedPlayerList.Count)
		{
			int AiID;
			do
			{
				AiID = Random.Range (0,FollowersList.Length - 1);
			}while(!FollowersList[AiID].GetComponent<Player>().isAI);
			
			ReplaceAI(AiID,"Player2");
		}

		if(FollowersList.Length != ConnectedPlayerList.Count)
		{
			int AiID;
			do
			{
				AiID = Random.Range (0,FollowersList.Length - 1);
			}while(!FollowersList[AiID].GetComponent<Player>().isAI);
			
			ReplaceAI(AiID,"Player3");
		}
		/*
		GameObject playerObject = (GameObject)Instantiate(PlayerObjectPrefab);
		Player player = playerObject.GetComponent<Player>();
		player.SetupPlayer("Player1");
		ConnectedPlayerList.Add("Player1",playerObject);

		playerObject = (GameObject)Instantiate(PlayerObjectPrefab);
		player = playerObject.GetComponent<Player>();
		player.SetupPlayer("Player2");
		ConnectedPlayerList.Add("Player2",playerObject);

		playerObject = (GameObject)Instantiate(PlayerObjectPrefab);
		player = playerObject.GetComponent<Player>();
		player.SetupPlayer("Player3");
		ConnectedPlayerList.Add("Player3",playerObject);
		*/

		/*
		player = ScriptableObject.CreateInstance<Player>();
		player.SetupPlayer("Player2");
		ConnectedPlayerList.Add("Player2",player);
		player = ScriptableObject.CreateInstance<Player>();
		player.SetupPlayer("Player3");
		ConnectedPlayerList.Add("Player3",player);
*/
		//---------
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		int numberHeretics = ConnectedPlayerList.Count;
		int numberHereticsDead = 0;
		foreach(KeyValuePair<string,GameObject> val in ConnectedPlayerList) {
			Player player = val.Value.GetComponent<Player>();
			if (player.dead) {
				numberHereticsDead++;
				//Debug.Log("player Dead!");
			}
		}
		if (numberHeretics == numberHereticsDead) {
			FindObjectOfType<ScoreManager>().GodWin();
		}
	}

	//TODO ConnectPlayer
	void ConnectPlayer()
	{

	}

	void SpawnAI()
	{


		int id = 0;
		foreach(Vector3 point in points)
		{
			GameObject playerObject = (GameObject)Instantiate(PlayerObjectPrefab);
			Player player = playerObject.GetComponent<Player>();
			player.gameObject.transform.position = point;

			FollowersList[id] = playerObject;
			id++;
		}
	}
	void ReplaceAI(int AiID, string playerID)
	{
		GameObject newPlayer = FollowersList[AiID];

		newPlayer.GetComponent<Player>().SetupPlayer(playerID);
		ConnectedPlayerList.Add(playerID,newPlayer);

		//FollowersList.Remove(AiID);
	}

	public void SendMoveToPlayer(string PlayerId, bool isLeft, bool isDown)
	{
		GameObject playerObject = ConnectedPlayerList[PlayerId];
		Player player = playerObject.GetComponent<Player>();

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
