using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class energyStar : MonoBehaviour {
    public float maxEnergy = 100;
    public float currentEnergy = 100;
    public float startingLightIntensity = 8;
    public float startingLightRange = 30;
    public GameObject lightGroup;
    private Renderer myRend;
    public Light[] lights;
    
	// Use this for initialization
	void Awake () {
        lights = GetComponentsInChildren<Light>();
        myRend = GetComponent<Renderer>();

	}
	public void drainEnergy(float drainAmount)
    {
        currentEnergy -= drainAmount;
        myRend.material.SetTextureOffset("_MainTex", new Vector2(0,(myRend.material.GetTextureOffset("_MainTex").y + drainAmount * 0.0025f)));

        foreach (Light light in lights)
        {
            if(currentEnergy >= 0)
            {
                light.intensity = startingLightIntensity * (currentEnergy / maxEnergy);
                light.range = startingLightRange * (currentEnergy / maxEnergy) + 5;
            }
            else
            {
                light.intensity = 0;
                light.range = 0;
            }
            
        }

    }
	// Update is called once per frame
	void Update () {
	
	}
}
