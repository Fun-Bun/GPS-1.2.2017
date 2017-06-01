using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool crit;
    public bool miss;

    // Use this for initialization
    void Start ()
    {
		Destroy(gameObject, 1.0f);
	}

    void OnTriggerEnter2D(Collider2D other) //Commonly, we use "other" as like to show that this collider is the another object we bump into
    {
		if(other.gameObject.tag == "Enemy")
        {
			Enemy enemyScript = other.gameObject.GetComponent<Enemy>();

			if(miss)
			    enemyScript.FloatingText("MISS");
			else
				enemyScript.Hurt(crit);
			
			Destroy(gameObject);
        }
		else if(other.gameObject.tag == "Deadly")
		{
			Destroy(other.gameObject);
			Destroy(gameObject);
		}
    }
}
