using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum WeaponState
{
    Ready = 0,
    Cooldown,
    Reloading,
    OutOfAmmo
}

public class PlayerWeapon : MonoBehaviour
{
	public Player self;

    //Bullet
    public GameObject bullet;
    public float bulletSpeed = 5f;

    //Stats
    public float accuracy = 70;
    public float critChance = 30;

    public float cooldown = 3f;
    private float cdTimer = 0;

    //Condition
    public WeaponState state;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(state == WeaponState.Cooldown)
        {
            cdTimer += Time.deltaTime;
            if(cdTimer >= cooldown)
            {
                cdTimer = 0;
                state = WeaponState.Ready;
            }
        }
	}

    public void Shoot()
    {
        if(state == WeaponState.Ready)
        {
            Vector2 fire_at_cursor;
            fire_at_cursor = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
            fire_at_cursor.Normalize();

            GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().velocity = fire_at_cursor * bulletSpeed;
            newBullet.transform.LookAt(Vector3.forward + newBullet.transform.position, fire_at_cursor);

            Bullet buletScript = newBullet.GetComponent<Bullet>();
            buletScript.miss = Random.Range(0, 100) > accuracy;
            if(!buletScript.miss) buletScript.crit = Random.Range(0, 100) < critChance;
            
            state = WeaponState.Cooldown;
        }
    }
}
