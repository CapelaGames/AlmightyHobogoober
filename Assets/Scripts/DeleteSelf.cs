using UnityEngine;
using System.Collections;

public class DeleteSelf : MonoBehaviour 
{
	public float time = 5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Time.time >= time)
		{
			Destroy(gameObject);
		}
	}
}
