using UnityEngine;
using System.Collections;

public class PriestPlayer : MonoBehaviour 
{
	//public GameObject ground;

	public float MoveSpeed = 0.05f;

	Vector3 MousePosition;
	Ray MouseRay;


	// Use this for initialization
	void Start () 
	{
		/*if(ground == null)
		{
			Debug.LogError("ERROR NO GROUND IN PriestPlayer");
		}*/
	}
	
	// Update is called once per frame
	void Update () 
	{
		int floorLayerMask = 1<<8;
		//floorLayerMask = ~floorLayerMask;
		MousePosition = Input.mousePosition;
		MouseRay = Camera.main.ScreenPointToRay(MousePosition);
		RaycastHit MouseRayHit;
		if(Physics.Raycast(MouseRay, out MouseRayHit, 1000, floorLayerMask))
		{
			Debug.DrawLine(MouseRay.origin, MouseRayHit.point);
			Vector3 newPosition = new Vector3(MouseRayHit.point.x,0,MouseRayHit.point.z);
			transform.position = Vector3.Lerp(transform.position,newPosition, MoveSpeed);



		}

		int characterLayerMask = 1<<9;

		if(Physics.Raycast(MouseRay, out MouseRayHit, 1000, characterLayerMask))
		{
			if (Input.GetMouseButtonDown(0))
			{
				MouseRayHit.transform.gameObject.GetComponent<Player>().KillPlayer();
			}

			bool found = false;
			foreach(Transform child in MouseRayHit.transform)
			{
				if(child.gameObject.tag == "Circle")
				{
					child.GetComponent<CircleManager>().ishighlighted = true;
					found = true;
				}
			}

			if(found == false)
			{
				Debug.Log("NOT FOUND");
			}


			//MouseRayHit.transform.gameObject.
		}
	}
}
