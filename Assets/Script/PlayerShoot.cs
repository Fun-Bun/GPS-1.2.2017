using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bullet;
    public float bulletSpeed = 5f;

	public float accuracy = 70;
	public float critChance = 30;

	public float cooldown = 3f;
	private float cdTimer = 0;

	bool hasGun;
	
    // Update is called once per frame
    void Update()
    {
		hasGun = GetComponent<InventorySystem>().itemCount[(int)ItemType.Gun] > 0;

		if(hasGun)
		{
			if(cdTimer <= 0)
	        {
				if(Input.GetButton("Fire1"))
				{
					Vector2 fire_at_cursor;
					fire_at_cursor = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
					fire_at_cursor.Normalize();

					GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
					newBullet.GetComponent<Rigidbody2D>().velocity = fire_at_cursor * bulletSpeed;
					newBullet.transform.LookAt(Vector3.forward + newBullet.transform.position,fire_at_cursor);
					//newBullet.transform.LookAt(transform.forward + (Vector3)fire_at_cursor, ;
					Bullet buletScript = newBullet.GetComponent<Bullet>();
					buletScript.miss = Random.Range(0, 100) > accuracy;
					if(!buletScript.miss) buletScript.crit = Random.Range(0, 100) < critChance;

					cdTimer = cooldown;
				}
	        }
			else
				cdTimer -= Time.deltaTime;
		}
    }
}
