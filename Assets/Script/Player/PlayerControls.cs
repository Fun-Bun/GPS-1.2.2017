using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [System.NonSerialized]
	public Player self;

	//Movement
	float distToGround;
	bool grounded;
	bool hasDoubleJumped;

	public LayerMask groundLayer;

    //Interact
    bool interacting;

    //Shoot
    bool hasGun;

	//Animation
	//...
	
	void Awake ()
	{
		distToGround = self.collider.bounds.extents.y;
		grounded = false;
		hasDoubleJumped = false;
        hasGun = false;
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
            if(hasGun)
            {
                self.weapons[0].Shoot();
            }
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
		GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal") * self.status.movementSpeed));
		GetComponent<Animator>().SetBool("Grounded", grounded);
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
                switch(drop.type)
                {
                    //Debug - Directly use gun
                    case ItemType.Gun:
                        hasGun = true;
                        break;
                        //Debug - Directly eat the food
                    case ItemType.Food:
                        self.status.health.Extend(self.status.health.max);
                        self.status.hunger.Extend(self.status.hunger.max);
                        break;
                    default:
                        self.inventory.AddItem(drop.type, drop.amount);
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
