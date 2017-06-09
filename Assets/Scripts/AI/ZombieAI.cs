using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour {
	public enum AIState
	{
		walking = 0,
		jumping,
		landing,
		dropping,
		attacking
	};

	public float buffer;
	public float speed;
	public float jumpHeight;

	Player player;
	public Platform platform;

	public Platform targetPlatform;
	public GameObject targetStart;
	public GameObject targetEnd;

	public AIState state;
	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindWithTag ("Player").GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Debug.Log(state.ToString());
		switch(state)
		{
			case AIState.dropping:
				if(platform != null && Mathf.Abs(transform.position.x - targetStart.transform.position.x) < buffer)
				{
					state = AIState.walking;
				}
				else
				{
					if(this.transform.position.x + buffer < targetStart.transform.position.x)
					{
						//Move Right
						this.transform.Translate(Time.deltaTime * speed * Vector3.right);
					}
					else if(targetStart.transform.position.x < this.transform.position.x - buffer)
					{
						//Move Left
						this.transform.Translate(Time.deltaTime * speed * Vector3.left);
					}
				}
			break;
			case AIState.jumping:
				if(this.transform.position.x + buffer < targetStart.transform.position.x)
				{
					//Move Right
					this.transform.Translate(Time.deltaTime * speed * Vector3.right);
				}
				else if(targetStart.transform.position.x < this.transform.position.x - buffer)
				{
					//Move Left
					this.transform.Translate(Time.deltaTime * speed * Vector3.left);
				}
				else
				{
					//Reached, jump now!!
					GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
					state = AIState.landing;
				}
			break;

			case AIState.landing:
				if(this.transform.position.x + buffer < targetEnd.transform.position.x)
				{
					//Move Right
					this.transform.Translate(Time.deltaTime * speed * Vector3.right);
				}
				else if(targetEnd.transform.position.x < this.transform.position.x - buffer)
				{
					//Move Left
					this.transform.Translate(Time.deltaTime * speed * Vector3.left);
				}
				else
				{
					//Landing successful
					state = AIState.walking;
				}
			break;

			case AIState.walking:
			default:
				if(platform != null && platform.whoStepOnMe.Contains(player.gameObject))
				{
					//Enter Walking State
					state = AIState.walking;
					if(this.transform.position.x + buffer < player.transform.position.x)
					{
						//Move Right
						this.transform.Translate(Time.deltaTime * speed * Vector3.right);
					}
					else if(player.transform.position.x < this.transform.position.x - buffer)
					{
						//Move Left
						this.transform.Translate(Time.deltaTime * speed * Vector3.left);
					}
					else
					{
						//Attack (Wait first)
						state = AIState.attacking;
					}
				}
				else
				{
					if(platform != null && player.controls.platform != null)
					{
						if(platform.gameObject.transform.position.y < player.controls.platform.gameObject.transform.position.y)
						{
							//Find player's platform
							targetPlatform = player.controls.platform;

							//Enter Jumping State
							state = AIState.jumping;
						}
						else
						{
							//Find self's platform
							targetPlatform = platform;
							
							//Enter Dropping State
							state = AIState.dropping;
						}

						float distance1 = Mathf.Abs(targetPlatform.jumpStart1.transform.position.x - this.transform.position.x);
						float distance2 = Mathf.Abs(targetPlatform.jumpStart2.transform.position.x - this.transform.position.x);

						//Find nearest target
						if(distance1 <= distance2)
						{
							targetStart = targetPlatform.jumpStart1;
							targetEnd = targetPlatform.jumpEnd1;
						}
						else
						{
							targetStart = targetPlatform.jumpStart2;
							targetEnd = targetPlatform.jumpEnd2;
						}
					}
					else
					{
						if(this.transform.position.x + buffer < player.transform.position.x)
						{
							//Move Right
							this.transform.Translate(Time.deltaTime * speed * Vector3.right);
						}
						else if(player.transform.position.x < this.transform.position.x - buffer)
						{
							//Move Left
							this.transform.Translate(Time.deltaTime * speed * Vector3.left);
						}
						else
						{
							//Attack (Wait first)
							state = AIState.attacking;
						}
					}
				}
			break;
		}
	}
}