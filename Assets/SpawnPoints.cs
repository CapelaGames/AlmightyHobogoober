using UnityEngine;
using System.Collections;

// SpawnPoints
// Class to get positions of all SpawnPoints (objects in a group "SpawnPoints")
// Returns all grid points as a Vector3 Array
public class SpawnPoints : MonoBehaviour 
{
    Vector3[] grid;

    // Use this for initialization
    void Awake () 
    {
        Transform spawners = GameObject.Find("SpawnPoints").GetComponent<Transform>();

        var numPoints = spawners.childCount;
        grid = new Vector3[numPoints];
        
        for(int i=0; i<numPoints; i++)
        {
            grid[i] = spawners.GetChild(i).position;
            //Debug.Log("x: " + grid[i].x + "; z: " + grid[i].z);
        }
    }
    
    // Update is called once per frame
    void Update () 
    {

    }


    public Vector3[] GetCoordinates()
    {
        return grid;
    }

}
