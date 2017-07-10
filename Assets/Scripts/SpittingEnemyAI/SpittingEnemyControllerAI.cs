using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class SpittingEnemyControllerAI : MonoBehaviour 
{
    [System.NonSerialized]
    public SpittingEnemy self;

    public enum sZoState    
    {
        idle = 0,
        jumping,
        landing,
        dropping,
        walking,
        spitting
    };

    public Transform[]patrolPoints;
    Transform currentPatrolPoint;
    public int currentPatrolIndex;

    public float buffer;
    public float speed;
    public float jumpForce;

    public float movingDirection;

    Player player;
    public Platform platform;

    public Platform targetPlatform;
    public GameObject targetStart;
    public GameObject targetEnd;

    public GameObject targetGO;
    public SpittingEnemyTargetScript target;
    public bool startTransform;
    public bool hasTransformed;
    public float timer;
    public bool inVicinity;
    public float triggerRange;

    public float idleTimer;
    public float idleDuration;

    //Shooting variables
    public Transform launchPoint;
    public GameObject enemyProjectile;
    public bool inSpitRange;
    public float spitRange;
    public float projectileCD;
    public float shotCounter;

    public sZoState state;

	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        GameObject newTarget = Instantiate(targetGO, this.transform.position,Quaternion.identity);
        target = newTarget.GetComponent<SpittingEnemyTargetScript>();
        target.master = this;
        target.targetOffset = GetComponent<BoxCollider2D>().bounds.extents.y;

        startTransform = false;
        hasTransformed = false;

        shotCounter = projectileCD;
	}
	
	// Update is called once per frame
	void Update () 
    {
        Debug.Log("Current State : " + state.ToString());
        shotCounter -= Time.deltaTime;

        if(player != null)
        {
            float distanceToTarget = Vector2.Distance((Vector2)this.transform.position, (Vector2)player.gameObject.transform.position);
            inVicinity = distanceToTarget <= triggerRange;
            float distanceToSpit = Vector2.Distance((Vector2)this.transform.position, (Vector2)player.gameObject.transform.position);
            inSpitRange = distanceToSpit <= spitRange;

            if(inVicinity)
            {
                target.SetPosition(player.gameObject);
                if(!startTransform)
                {
                    startTransform = true;
                }

                if(startTransform && !hasTransformed)
                {
                    //input transform animation;
                    timer += Time.deltaTime;
                    if(timer >= 1.5f)
                    {
                        hasTransformed = true;
                        speed *= 5f;
                    }
                    return;
                }
            }

            if(inSpitRange && shotCounter <= 0)
            {
                shotCounter = 0;
                timer = 0f;
                state = sZoState.spitting;
            }
        }

        switch(state)
        {
            case sZoState.dropping:
                if(platform != null && Mathf.Abs(transform.position.x - targetStart.transform.position.x) < buffer)
                {
                    state = sZoState.walking;
                }

                else
                {
                    if(this.transform.position.x + buffer < targetStart.transform.position.x)
                    {
                        //Move Right
                        this.transform.Translate(Time.deltaTime * speed * Vector3.right);
                        this.transform.localScale = new Vector3(-0.65f,0.65f,1);

                    }
                    else if(targetStart.transform.position.x < this.transform.position.x - buffer)
                    {
                        //Move Left
                        this.transform.Translate(Time.deltaTime * speed * Vector3.left);
                        this.transform.localScale = new Vector3(0.65f,0.65f,1);
                    }
                }
                break;

            case sZoState.jumping:
                if(this.transform.position.x + buffer < targetStart.transform.position.x)
                {
                    //Move Right
                    this.transform.Translate(Time.deltaTime * speed * Vector3.right);
                    this.transform.localScale = new Vector3(-0.65f,0.65f,1);
                }
                else if(targetStart.transform.position.x < this.transform.position.x - buffer)
                {
                    //Move Left
                    this.transform.Translate(Time.deltaTime * speed * Vector3.left);
                    this.transform.localScale = new Vector3(0.65f,0.65f,1);
                }

                else
                {
                    //targetStart reached! Jump!
                    GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                    //if(hasTransformed) self.animator.Play("Enemy_JumpLeft");
                    //else self.animator.Play("Enemy_PreWalkLeft");

                    state = sZoState.landing;
                }
                break;

            case sZoState.landing:
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
                    state = sZoState.walking;
                }
                break;

            case sZoState.walking:
                if(platform != null && platform.whoStepOnMe.Contains(target.gameObject))
                {
                    state = sZoState.walking;

                    if(this.transform.position.x + buffer < target.gameObject.transform.position.x)
                    {
                        this.transform.Translate(Time.deltaTime* speed * Vector3.right);
                        this.transform.localScale = new Vector3(-0.65f,0.65f,1);
                    }

                    else if(this.transform.position.x + buffer > target.gameObject.transform.position.x)
                    {
                        this.transform.Translate(Time.deltaTime * speed * Vector3.left);
                        this.transform.localScale = new Vector3(0.65f,0.65f,1);
                    }

                    else
                    {
                        if(inSpitRange)
                        {
                            shotCounter = 0f;
                            timer = 0f;
                            state = sZoState.spitting;
                        }

                        else
                            state = sZoState.idle;
                    }
                }

                else
                {
                    if(platform != null && target.platform != null)
                    {
                        if(platform.gameObject.transform.position.y < target.platform.gameObject.transform.position.y)
                        {
                            //Set player's current platform as target
                            targetPlatform = target.platform;

                            //Enter jump state
                            state = sZoState.jumping;
                        }

                        else
                        {
                            targetPlatform = platform;

                            state = sZoState.dropping;
                        }

                        float distance1 = Mathf.Abs(targetPlatform.jumpStart1.transform.position.x - this.transform.position.x);
                        float distance2 = Mathf.Abs(targetPlatform.jumpStart2.transform.position.x - this.transform.position.x);

                        //find nearest jumpStart/jumpEnd waypoints
                        if(distance1 <= distance2)
                        {
                            //Walk towards 1st waypoints
                            targetStart = targetPlatform.jumpStart1;
                            targetEnd = targetPlatform.jumpEnd1;
                        }

                        else
                        {
                            //Walk towards 2nd waypoints
                            targetStart = targetPlatform.jumpStart2;
                            targetEnd = targetPlatform.jumpEnd2;
                        }
                    }

                    else
                    {
                        if(this.transform.position.x + buffer < target.gameObject.transform.position.x)
                        {
                            this.transform.Translate(Time.deltaTime * speed * Vector3.right);
                            this.transform.localScale = new Vector3(-0.65f,0.65f,1);
                        }

                        else if(this.transform.position.x + buffer > target.gameObject.transform.position.x)
                        {
                            this.transform.Translate(Time.deltaTime * speed * Vector3.left);
                            this.transform.localScale = new Vector3(0.65f,0.65f,1);
                        }

                        else
                        {
                            //Spit projectile
                            if(inSpitRange)
                            {
                                shotCounter = 0f;
                                timer = 0f;
                                state = sZoState.spitting;
                                //self.melee.hasHit = false;
                            }

                            else
                            {
                                state = sZoState.idle;
                            }
                        }
                    }
                }
                break;

            case sZoState.spitting:
                timer += Time.deltaTime;
                Debug.Log("I AM SPITTING NOW");
                if(shotCounter <= 0)
                {
                    Instantiate(enemyProjectile, launchPoint.position, launchPoint.rotation);
                    shotCounter = projectileCD;

                    //self.animator.Play("SpitAnimation");
                }

                else if(timer >= 0.7f)
                {
                    timer = 0f;
                    state = sZoState.walking;
                }
                break;
            
            case sZoState.idle:
            default:
                /*if(hasTransformed)
                {
                    self.animator.Play("Animation");
                }

                else
                {
                    self.animator.Play("Animation");
                }*/

                if(inVicinity)
                {
                    state = sZoState.walking;
                }

                else if(inSpitRange)
                {
                    shotCounter = 0;
                    state = sZoState.spitting;
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

                            state = sZoState.walking;
                        }
                    }
                }
                break;   

        }
	}
}
