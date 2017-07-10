using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum WeaponState
{
    Ready = 0,
    Cooldown,
    Reloading,
    Overheat
}

public enum WeaponType
{
	AK47 = 0,
	ParticleCannon,
	None
}

public class PlayerWeapon : MonoBehaviour
{
	public Player self;

	//Type
	public WeaponType type;

	//Condition
	public Resource bullet;

	public WeaponState state;

    //Stats
    public float accuracy;
    public float critChance;

	private float cdDuration;
    private float cdTimer;

	private float reloadDuration;
	private float reloadTimer;

	private float overheatDuration;
	private float overheatTimer;

	//Bullet
	public List<GameObject> bulletGOs;
	public List<float> bulletSpeed;

	// Use this for initialization
	void Start ()
	{
		Setup(type);
	}

	public void Setup(WeaponType type)
	{
		this.type = type;
		switch(type)
		{
			case WeaponType.AK47:
				bullet.max = 6;
				cdDuration = 0.1f;
				reloadDuration = 2f;
				overheatDuration = 0f;
				break;
			case WeaponType.ParticleCannon:
				bullet.max = 200;
				cdDuration = 0.01f;
				reloadDuration = 0.02f;
				overheatDuration = 2f;
				break;
		}
		bullet.value = bullet.max;
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch (state)
		{
			case WeaponState.Cooldown:
				cdTimer += Time.deltaTime;
				if(cdTimer >= cdDuration)
				{
					CDDone();
				}
				break;
			case WeaponState.Reloading:
				reloadTimer += Time.deltaTime;
				if(reloadTimer >= reloadDuration)
				{
					ReloadDone();
				}
				break;
			case WeaponState.Overheat:
				overheatTimer += Time.deltaTime;
				if(overheatTimer >= overheatDuration)
				{
					OverheatDone();
				}
				break;
		}
	}

    public void Shoot()
	{
		if(type != WeaponType.None)
		{
			CheckBullet();

			if(state == WeaponState.Ready || (type == WeaponType.ParticleCannon && state == WeaponState.Reloading))
	        {
	            Vector2 fire_at_cursor;
	            fire_at_cursor = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
	            fire_at_cursor.Normalize();

				GameObject newBullet = Instantiate(bulletGOs[(int)type], transform.position, Quaternion.identity);
				newBullet.GetComponent<Rigidbody2D>().velocity = fire_at_cursor * bulletSpeed[(int)type];
	            newBullet.transform.LookAt(Vector3.forward + newBullet.transform.position, fire_at_cursor);

	            Bullet buletScript = newBullet.GetComponent<Bullet>();
	            buletScript.crit = Random.Range(0, 100) < critChance;
	            
				bullet.Reduce(1);

				cdTimer = 0;
	            state = WeaponState.Cooldown;
			}
		}
    }

	void CDDone()
	{
		cdTimer = 0;
		switch(type)
		{
			case WeaponType.AK47:
				state = WeaponState.Ready;
				break;
			case WeaponType.ParticleCannon:
				if(bullet.value < bullet.max) state = WeaponState.Reloading;
				else state = WeaponState.Ready;
				break;
		}
	}

	public void Reload()
	{
		switch(type)
		{
			case WeaponType.AK47:
				if(state != WeaponState.Reloading)
				{
					reloadTimer = 0;
					state = WeaponState.Reloading;
				}
				break;
			case WeaponType.ParticleCannon:
				break;
		}
	}

	void ReloadDone()
	{
		reloadTimer = 0;
		switch(type)
		{
			case WeaponType.AK47:
				bullet.value = bullet.max;
				state = WeaponState.Ready;
				break;
			case WeaponType.ParticleCannon:
				if(bullet.value < bullet.max)
				{
					bullet.Extend(1);
					state = WeaponState.Reloading;
				}
				else
				{
					state = WeaponState.Ready;
				}
				break;
		}
	}

	void OverheatDone()
	{
		switch(type)
		{
			case WeaponType.ParticleCannon:
				overheatTimer = 0;
				state = WeaponState.Reloading;
				break;
		}
	}

	public void CheckBullet()
	{
		switch(type)
		{
			case WeaponType.AK47:
				if(state != WeaponState.Reloading && bullet.value <= 0)
				{
					bullet.value = 0;
                    reloadTimer = 0;
					state = WeaponState.Reloading;
				}
				break;
			case WeaponType.ParticleCannon:
				if(bullet.value <= 0)
				{
					bullet.value = 0;
                    overheatTimer = 0;
					state = WeaponState.Overheat;
				}
				break;
		}
	}
}
