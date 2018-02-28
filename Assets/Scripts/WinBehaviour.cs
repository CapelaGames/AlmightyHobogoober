using UnityEngine;
using System.Collections;

public class WinBehaviour : MonoBehaviour {
	
	void Start()
	{
		StartCoroutine(NextSceneCoroutine());
	}
	
	IEnumerator NextSceneCoroutine()
	{			
		yield return new WaitForSeconds(7);   //Wait
		Application.LoadLevel("TitleScene");
	}
}
