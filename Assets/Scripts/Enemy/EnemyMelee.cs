using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
	public bool hasHit;
	// Use this for initialization
	void Start ()
	{
		hasHit = false;
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.tag == "Player" && !hasHit)
		{
			hasHit = true;
			other.GetComponent<Player>().status.health.Reduce(20);
		}
	}
}
