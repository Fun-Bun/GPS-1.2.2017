using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour {

    public Transform[]platformPoints;
    Transform currentPlatformPoint;
    public int currentPlatformIndex;
    public float pSpeed;
    public float pbuffer;

	// Use this for initialization
	void Start () {
        currentPlatformIndex = 0;
        currentPlatformPoint = platformPoints[currentPlatformIndex];
	}
	
	// Update is called once per frame
	void Update () {
        
        if(this.transform.position.x + pbuffer < currentPlatformPoint.transform.position.x)
        {
            this.transform.Translate(Time.deltaTime * pSpeed * Vector3.right);
        }

        else if(this.transform.position.x + pbuffer > currentPlatformPoint.transform.position.x)
        {
            this.transform.Translate(Time.deltaTime * pSpeed * Vector3.left);
        }

        if(Vector3.Distance(transform.position,currentPlatformPoint.position) < .1f) // Checking arrival on patrol point
        {
            if(currentPlatformIndex + 1 < platformPoints.Length) // patrol point reached, move to next point
            {
                currentPlatformIndex ++;
            }

            else
            {
                currentPlatformIndex = 0; // Go to first point
            }

            currentPlatformPoint = platformPoints[currentPlatformIndex];
        }
	}
}
