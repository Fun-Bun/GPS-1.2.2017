using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Resource
{
    public int value;
    public int max;


    public float GetPercent()
    {
        return (float)value / (float)max;
    }


    public int Extend(int amount)
    {
        value += amount;
        if(value > max)
        {
            int remainder = value - max;
            value = max;
            return remainder;
        }
        else return 0;
    }

    public int Reduce(int amount)
    {
        value -= amount;
        if(value < 0)
        {
            int remainder = -value;
            value = 0;
            return remainder;
        }
        else return 0;
    }
}

[System.Serializable]
public class Depletable
{
    public Resource resource;

    public int depleteAmount;
    public float depleteDuration;
    public float depleteRate;
    private float depleteTimer;

    public void Update(float time)
    {
        depleteTimer += time * depleteRate;
        if(depleteTimer >= depleteDuration)
        {
            resource.Reduce(depleteAmount);
            depleteTimer = 0f;
        }
    }
}

public class PlayerStatus : MonoBehaviour
{
    [System.NonSerialized]
	public Player self;

    //Stats
    public Resource health;
    public Resource hunger;
    public Depletable hungerDeplete;

	//Movement
	public float movementSpeed;
	public float jumpHeight;
	
    void Awake()
    {
        hungerDeplete.resource = hunger;
    }

	// Update is called once per frame
	void Update ()
	{
        hungerDeplete.Update(Time.deltaTime);
        CheckDeath();
	}

    void CheckDeath()
    {
        if(health.value <= 0f)
        {
            Debug.Log("Player is dead.");
            Destroy(gameObject);
        }
    }
}
