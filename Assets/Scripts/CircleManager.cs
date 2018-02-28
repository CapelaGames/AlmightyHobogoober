using UnityEngine;
using System.Collections;

public class CircleManager : MonoBehaviour 
{
	public bool ishighlighted;

	public Material Highlighted;
	public Material NotHighlighted;

	Renderer rend;
	void Start()
	{
		rend = GetComponentInChildren<Renderer>();
		//Debug.Log(rend.name);
	}

	// Update is called once per frame
	void Update () 
	{
		if(ishighlighted)
		{
			//Debug.Log("high");
			rend.material = Highlighted;
			//rend.material.color = new Color(102,173,242);
			ishighlighted = false;
		}
		else
		{
			//Debug.Log("not high");
			rend.material = NotHighlighted;
			//rend.material.color = new Color(29,29,130);
		}
	}
}
