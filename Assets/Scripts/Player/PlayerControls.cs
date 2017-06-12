using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [System.NonSerialized]
	public Player self;

	//Detection
	public Platform platform;

	//Movement
	float distToGround;
	bool grounded;
	bool hasDoubleJumped;

	public LayerMask groundLayer;

    //Interact
    bool interacting;

	//Animation
	//...

	void Awake()
	{
		grounded = false;
		hasDoubleJumped = false;
	}

	void Start ()
	{
		distToGround = self.collider.bounds.extents.y;
	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdatePhysics();

		//Move horizontally
		if(Input.GetAxis("Horizontal") != 0f)
		{
			transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * self.status.movementSpeed * Time.deltaTime);

			//Flip if value is negative (going left)
			self.renderer.flipX = Input.GetAxis("Horizontal") > 0f;
		}

		//Jump
		if(Input.GetButtonDown("Jump"))
		{
			//Temporary variable
			bool canJump = false;

			//Condition Check
			if(grounded)
			{
				canJump = true;
			}
			else if(!hasDoubleJumped)
			{
				hasDoubleJumped = true;
				canJump = true;
			}

			//If the condition is matched, initiate jump.
			if(canJump)
			{
				//Put vertical movement to rest
				self.rigidbody.velocity = new Vector2(self.rigidbody.velocity.x, 0.0f);

				//Add force to the player impulsively (to surpass the gravity force)
				self.rigidbody.AddForce(Vector2.up * self.status.jumpHeight, ForceMode2D.Impulse);
			}
		}

		//Descend (Missing)
		/*
		if(Input.GetButtonDown("Descend"))
		{
			
		}
		*/

        //Interact (Debug, add input at editor settings later on)
        if(Input.GetKeyDown(KeyCode.E))
            interacting = true;
        if(Input.GetKeyUp(KeyCode.E))
            interacting = false;

        //Shoot
        if(Input.GetButton("Fire1"))
        {
            if(self.ownedWeapons.Capacity > 0)
				self.ownedWeapons[0].Shoot();
        }

		//Reload
		if(Input.GetKeyDown(KeyCode.R))
		{
            if(self.ownedWeapons.Capacity > 0)
				self.ownedWeapons[0].Reload();
		}

        //Debug - Reduce health
        if(Input.GetKeyDown(KeyCode.X))
        {
            self.status.health.Reduce(20);
        }

		UpdateAnimation();
	}

	void UpdatePhysics()
	{
		grounded = Physics2D.OverlapCircle ((Vector2)transform.position + (Vector2.down * distToGround), 0.01f, groundLayer);
		if(grounded) hasDoubleJumped = false;
	}

	void UpdateAnimation()
	{
		self.animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal") * self.status.movementSpeed));
		self.animator.SetBool("Grounded", grounded);
	}

    void OnTriggerStay2D(Collider2D other)
    {
        /*
        if(interacting)
        {
        */
            //Interact with items
            if(other.tag == "Item")
            {
                DroppedItem drop = other.GetComponent<DroppedItem>();

				self.inventory.AddItem(drop.data.type, drop.data.amount);
				
                switch(drop.data.type)
                {
                    //Debug - Directly use Gun
                    case ItemType.Gun:
						GameObject newGun = Instantiate(self.weaponPrefab, this.transform);
						newGun.GetComponent<PlayerWeapon>().Setup(WeaponType.AK47);
						self.ownedWeapons.Add(newGun.GetComponent<PlayerWeapon>());
                        break;
					//Debug - Directly upgrade Gun to Particle Cannon
					case ItemType.Scrap:
                        if(self.ownedWeapons.Capacity > 0)
							self.ownedWeapons[0].Setup(WeaponType.ParticleCannon);
						break;
                    //Debug - Directly eat the food
                    case ItemType.Food:
                        self.status.health.Extend(self.status.health.max);
                        self.status.hunger.Extend(self.status.hunger.max);
                        break;
                }
                interacting = false;
                Destroy(other.gameObject);
            }
        /*
            //Other interactions
            else if(true)
            {

            }
        }
        */
    }
}
