using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
	public float speed;
	public float jumpHeight;
	float distToGround;
	bool grounded;
	public LayerMask groundLayer;
	
	bool hasDoubleJumped;

	void Start ()
	{
		distToGround = GetComponent<BoxCollider2D>().bounds.extents.y;
	}

	void Update ()
	{
		grounded = Physics2D.OverlapCircle ((Vector2)transform.position + (Vector2.down * distToGround), 0.01f, groundLayer);
		if(grounded) hasDoubleJumped = false;

		if(Input.GetAxis("Horizontal") != 0f)
		{
			transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime);
			GetComponent<SpriteRenderer>().flipX = Input.GetAxis("Horizontal") > 0f;
		}
		if(Input.GetButtonDown("Jump"))
		{
			bool canJump = false;

			if(grounded)
			{
				canJump = true;
			}
			else if(!hasDoubleJumped)
			{
				hasDoubleJumped = true;
				float speedX = GetComponent<Rigidbody2D>().velocity.x;
				GetComponent<Rigidbody2D>().velocity = new Vector2(speedX, 0.0f);
				canJump = true;
			}

			if(canJump)
			{
				GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
			}
		}

		GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal") * speed));
		GetComponent<Animator>().SetBool("Grounded", grounded);
	}
}
