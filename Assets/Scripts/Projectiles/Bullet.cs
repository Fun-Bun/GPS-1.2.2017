using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool crit;

    // Use this for initialization
    void Start ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D other) //Commonly, we use "other" as like to show that this collider is the another object we bump into
    {
		if(other.gameObject.tag == "Enemy")
        {
			EnemyStatus enemyScript = other.gameObject.GetComponent<EnemyStatus>();

			enemyScript.Hurt(crit);
			
			Destroy(gameObject);
        }
		else if(other.gameObject.tag == "Deadly")
		{
			Destroy(other.gameObject);
			Destroy(gameObject);
		}
		else if(other.gameObject.layer == 1 << LayerMask.NameToLayer("SolidTiles"))
		{
			Destroy(gameObject);
		}
    }
}
