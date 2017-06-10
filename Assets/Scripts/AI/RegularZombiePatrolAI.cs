using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularZombiePatrolAI : MonoBehaviour {

	public enum rAIState
	{
		idle = 0,
		triggered,
		attack
	};

	public Transform[]patrolPoints;
	Transform currentPatrolPoint;
	public int currentPatrolIndex;
	public rAIState rState;
	public bool inVicinity;
	public float rSpeed;
	public float rbuffer;
	public Transform target;
	public float triggerRange;

	// Use this for initialization
	void Start () 
	{
		currentPatrolIndex = 0;
		currentPatrolPoint = patrolPoints[currentPatrolIndex];
	}
	
	// Update is called once per frame
	void Update () 
	{
		Debug.Log(rState.ToString());

		//this.transform.Translate(Vector2.left * Time.deltaTime * rSpeed);

		float distanceToTarget = Vector3.Distance(transform.position, target.position);
		if(distanceToTarget < triggerRange)
		{
			inVicinity = true;
		}

		else 
		{
			inVicinity = false;
		}

		switch(rState)
		{
			case rAIState.idle:
			default:
				if(inVicinity == true)
				{
					rState = rAIState.triggered;
				}

				else
				{
					if(this.transform.position.x + rbuffer < currentPatrolPoint.transform.position.x)
					{
						this.transform.Translate(Time.deltaTime * rSpeed * Vector3.right);
					}

					else if(this.transform.position.x + rbuffer > currentPatrolPoint.transform.position.x)
					{
						this.transform.Translate(Time.deltaTime * rSpeed * Vector3.left);
					}

					if(Vector3.Distance(transform.position,currentPatrolPoint.position) < .1f) // Checking arrival on patrol point
					{
						if(currentPatrolIndex + 1 < patrolPoints.Length) // patrol point reached, move to next point
						{
							currentPatrolIndex ++;
						}

						else
						{
							currentPatrolIndex = 0; // Go to first point
						}

						currentPatrolPoint = patrolPoints[currentPatrolIndex];
					}
				}
			break;

			case rAIState.triggered:
			if(inVicinity == false)
			{
				rState = rAIState.idle;
			}

			else
			{
				if(this.transform.position.x < target.transform.position.x)
				{
					this.transform.Translate(Time.deltaTime * rSpeed * Vector3.right);
				}
		
				else if(this.transform.position.x >target.transform.position.x)
				{
					this.transform.Translate(Time.deltaTime * rSpeed * Vector3.left);
				}

				else
				{
					rState = rAIState.attack;
				}
			}
			break;
		}
	}
}
