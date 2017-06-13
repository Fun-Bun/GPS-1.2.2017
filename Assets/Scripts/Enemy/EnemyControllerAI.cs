using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerAI : MonoBehaviour {

	[System.NonSerialized]
	public Enemy self;

	public enum AIState
	{
		idle = 0,
		jumping,
		landing,
		dropping,
		walking,
		attacking
	};

	public Transform[]patrolPoints;
	Transform currentPatrolPoint;
	public int currentPatrolIndex;

	public float buffer;
	public float speed;
	public float jumpHeight;

	public float movingDirection;

	Player player;
	public Platform platform;

	public Platform targetPlatform;
	public GameObject targetStart;
	public GameObject targetEnd;

	public bool inVicinity;
	public GameObject targetGO;
	public TargetScript target;
	public float triggerRange;
	public bool startTransform;
	public bool hasTransformed;
	public float timer;

	public float idleTimer;
	public float idleDuration;

	public AIState state;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindWithTag ("Player").GetComponent<Player> ();
		GameObject newTarget = Instantiate(targetGO, this.transform.position, Quaternion.identity);
		target = newTarget.GetComponent<TargetScript>();
		target.master = this;
		target.targetOffset = GetComponent<BoxCollider2D>().bounds.extents.y;
		target.SetPosition(this.transform.position);

		startTransform = false;
		hasTransformed = false;
		//for patrolling AI
		/*
		currentPatrolIndex = 0;
		currentPatrolPoint = patrolPoints[currentPatrolIndex];
        */      
	}

	// Update is called once per frame
	void Update ()
    {
        Debug.Log(state.ToString());
        Debug.Log(self.animator.GetCurrentAnimatorStateInfo(0).nameHash.ToString());

        self.animator.SetBool("IsMoving", state == AIState.walking || state == AIState.jumping || state == AIState.dropping);
        self.animator.SetFloat("VSpeed", self.rigidbody.velocity.y);
        self.animator.SetBool("Midair", platform == null);
        self.animator.SetBool("IsFlip", self.renderer.flipX);

		if(player != null)
		{
			float distanceToTarget = Vector2.Distance((Vector2)this.transform.position, (Vector2)player.gameObject.transform.position);
			inVicinity = distanceToTarget <= triggerRange;
			if(inVicinity)
			{
				target.SetPosition(player.gameObject);
				if(!startTransform) startTransform = true;
			}

			if(startTransform && !hasTransformed)
			{
				//if(self.renderer.flipX) self.animator.Play("Enemy_TransformRight");
				//else self.animator.Play("Enemy_TransformLeft");
                self.animator.Play("Enemy_TransformLeft");

				timer += Time.deltaTime;
				if(timer >= 1.5f)
				{
					hasTransformed = true;
					speed *= 5f;
				}
				return;
			}
        }

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
                    self.renderer.flipX = true;
				}
				else if(targetStart.transform.position.x < this.transform.position.x - buffer)
				{
					//Move Left
                    this.transform.Translate(Time.deltaTime * speed * Vector3.left);
                    self.renderer.flipX = false;
                }
                    /*
                if(hasTransformed)
                {
                    if(platform == null) self.animator.Play("Enemy_DropLeft");
                    else self.animator.Play("Enemy_WalkLeft");
                }
                else self.animator.Play("Enemy_PreWalkLeft");*/
			}
			break;
		case AIState.jumping:
			if(this.transform.position.x + buffer < targetStart.transform.position.x)
			{
				//Move Right
				this.transform.Translate(Time.deltaTime * speed * Vector3.right);
                self.renderer.flipX = true;
				//if(hasTransformed) self.animator.Play("Enemy_WalkLeft");
				//else self.animator.Play("Enemy_PreWalkLeft");
			}
			else if(targetStart.transform.position.x < this.transform.position.x - buffer)
			{
                //Move Left
                this.transform.Translate(Time.deltaTime * speed * Vector3.left);
                self.renderer.flipX = false;
				//if(hasTransformed) self.animator.Play("Enemy_WalkLeft");
				//else self.animator.Play("Enemy_PreWalkLeft");
			}
			else
			{
				//Reached, jump now!!
				GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
                /*
				if(self.renderer.flipX)
				{
					if(hasTransformed) self.animator.Play("Enemy_JumpRight");
					else self.animator.Play("Enemy_PreWalkRight");
				}
				else
				{*/
					if(hasTransformed) self.animator.Play("Enemy_JumpLeft");
					else self.animator.Play("Enemy_PreWalkLeft");
				//}
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
			if(platform != null && platform.whoStepOnMe.Contains(target.gameObject))
			{
				//Enter Walking State
				state = AIState.walking;
				if(this.transform.position.x + buffer < target.gameObject.transform.position.x)
				{
					//Move Right
					this.transform.Translate(Time.deltaTime * speed * Vector3.right);
                    self.renderer.flipX = true;
					//if(hasTransformed) self.animator.Play("Enemy_WalkRight");
					//else self.animator.Play("Enemy_PreWalkRight");
				}
				else if(target.gameObject.transform.position.x < this.transform.position.x - buffer)
				{
					//Move Left
                    this.transform.Translate(Time.deltaTime * speed * Vector3.left);
                    self.renderer.flipX = false;
					//if(hasTransformed) self.animator.Play("Enemy_WalkLeft");
					//else self.animator.Play("Enemy_PreWalkLeft");
				}
				else
				{
					//Attack
					if(inVicinity)
					{
						state = AIState.attacking;
						self.melee.hasHit = false;
						timer = 0f;
					}
					else
						state = AIState.idle;
				}
			}
			else
			{
				if(platform != null && target.platform != null)
				{
					if(platform.gameObject.transform.position.y < target.platform.gameObject.transform.position.y)
					{
						//Find player's platform
						targetPlatform = target.platform;

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
					if(this.transform.position.x + buffer < target.gameObject.transform.position.x)
					{
						//Move Right
                        this.transform.Translate(Time.deltaTime * speed * Vector3.right);
                        self.renderer.flipX = true;
						//if(hasTransformed) self.animator.Play("Enemy_WalkRight");
						//else self.animator.Play("Enemy_PreWalkRight");
					}
					else if(target.gameObject.transform.position.x < this.transform.position.x - buffer)
					{
						//Move Left
                        this.transform.Translate(Time.deltaTime * speed * Vector3.left);
                        self.renderer.flipX = false;
						//if(hasTransformed) self.animator.Play("Enemy_WalkLeft");
						//else self.animator.Play("Enemy_PreWalkLeft");
					}
					else
					{
						//Attack
						if(inVicinity)
						{
							state = AIState.attacking;
							self.melee.hasHit = false;
							timer = 0f;
						}
						else
							state = AIState.idle;
					}
				}
			}
			break;
		case AIState.attacking:
			timer += Time.deltaTime;

			//if(self.renderer.flipX) self.animator.Play("Enemy_AttackRight");
			//else self.animator.Play("Enemy_AttackLeft");
            self.animator.Play("Enemy_AttackLeft");
            
			if(timer >= 0.7f)
			{
				timer = 0f;
				state = AIState.walking;
			}
			break;
		case AIState.idle:
		default:
			if(hasTransformed)
			{
                //if(self.renderer.flipX) self.animator.Play("Enemy_IdleRight");
                //else self.animator.Play("Enemy_IdleLeft");
                self.animator.Play("Enemy_IdleLeft");
			}
			else
			{
                //if(self.renderer.flipX) self.animator.Play("Enemy_PreIdleRight");
                //else self.animator.Play("Enemy_PreIdleLeft");
                self.animator.Play("Enemy_PreIdleLeft");
			}
			if(inVicinity)
			{
				state = AIState.walking;
			}
			else
			{
				idleTimer += Time.deltaTime;
				if(idleTimer >= idleDuration)
				{
					idleTimer = 0;
					if(platform != null)
					{
						float randX = Random.Range(platform.transform.position.x - (platform.halfLength - buffer), platform.transform.position.x + (platform.halfLength - buffer));
						target.SetPosition(new Vector3(randX, this.transform.position.y, this.transform.position.z));

						state = AIState.walking;
					}
				}

				/*
					if(this.transform.position.x + buffer < currentPatrolPoint.transform.position.x)
					{
						this.transform.Translate(Time.deltaTime * speed * Vector3.right);
					}

					else if(this.transform.position.x + buffer > currentPatrolPoint.transform.position.x)
					{
						this.transform.Translate(Time.deltaTime * speed * Vector3.left);
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
					*/
			}
			break;
        }
	}

}
