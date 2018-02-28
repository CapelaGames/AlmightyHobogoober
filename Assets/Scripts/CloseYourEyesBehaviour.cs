using UnityEngine;
using System.Collections;

public class CloseYourEyesBehaviour : MonoBehaviour {
	
	void Start()
	{
		StartCoroutine(NextSceneCoroutine());
	}
	
	IEnumerator NextSceneCoroutine()
	{			
		yield return new WaitForSeconds(4);   //Wait
		Application.LoadLevel("TestScene");
	}
}
