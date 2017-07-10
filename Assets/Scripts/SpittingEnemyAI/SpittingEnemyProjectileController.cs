using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpittingEnemyProjectileController : MonoBehaviour 
{
    public float speed;
    Player player;
    public int hpReduced;
    private Rigidbody2D rigidbody;

	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        rigidbody = GetComponent<Rigidbody2D>();

        if(player.transform.position.x < transform.position.x)
        {
            speed = -speed;
        }

        Destroy(gameObject, 2.5f);
	}

    void Update()
    {
        rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<Player>().status.health.Reduce(hpReduced);
            Destroy(gameObject);
        }
    }
}
