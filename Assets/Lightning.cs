using UnityEngine;
using System.Collections;

public class Lightning : MonoBehaviour 
{   
    public float minimumInterval = 5000f;
    public float maxDuration = 1000f;
    public float introTime = 8000f;
    public float fadeTime = 200f;
    public float chance = 0.1f;
	public float ThunderPreLighting= 0.5f;//seconds

    Light light;

    bool active;
    bool fullLight;

    float count;
    float fadeCount;


    // Use this for initialization
    void Start () 
    {
        active = false;
        fullLight = false;
        light = gameObject.GetComponent<Light>();
        
        count = 0;
        fadeCount = 0;   
    }
    
    // Update is called once per frame
    void Update () 
    {
        count += Time.deltaTime * 1000;

        if (active) //if lights on
        {
            //Fade In
            if (!fullLight) 
            {
                fadeCount += Time.deltaTime * Random.value * 1000;

                float amplitude = Mathf.Sin( fadeCount ) * 0.9F + 0.1F;
                light.intensity = amplitude;

                if (fadeCount >= fadeTime) //
                {
                    fullLight = true;
                    light.intensity = 1;
                    count = 0f; //start counter again, for full light
                }
            } else {
                if (count >= maxDuration)
                {
                    count = 0f;
                    active = false;
                    fullLight = false;

                    light.enabled = active;
                    Debug.Log("Lights off");
                }
            }
        } else { //if lights off
            if (count >= minimumInterval) 
            {
                if (Random.value < chance) 
                { //10% chance

					if(isLightingGoing == false)
					{
						isLightingGoing = true;
						StartCoroutine("StartLighting");
					}
                }
            }
        }

        if (Time.time >= introTime)
        {
            count = 0f;
            light.enabled = active;
        }
    }

	bool isLightingGoing = false;
	IEnumerator StartLighting()
	{
		GetComponent<AudioSource>().Play();

		yield return new WaitForSeconds(ThunderPreLighting);

		active = true;
		fullLight = false;
		fadeCount = 0f;
		
		light.enabled = active;
		//lightDuration = Random.Range(0.5f, maxDuration);
		Debug.Log("Lights on");
		isLightingGoing = false;
	}
}
