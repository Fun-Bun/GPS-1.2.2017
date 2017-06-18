using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

	public float CurrentHealth;
	public float MaxHealth;
	public int CurrentHunger;
	public int MaxHunger;
	private float exhaustion;
	public float hungerRate;
	public float hungerDuration;

	public Slider healthBar;
	public List<Image> hungerBar;

	public List<Sprite> hungerSprites;

	// Use this for initialization
	void Start () 
	{
		MaxHealth = 100f;
		MaxHunger = 6;
		//to reset health to full
		CurrentHealth = MaxHealth;
		CurrentHunger = MaxHunger;

		healthBar.value = CalculateHealth();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.X))
			DealDamage(20);
		if(CurrentHunger > 0)
		{
			exhaustion += Time.deltaTime * hungerRate;
			if(exhaustion > hungerDuration)
			{
				exhaustion = 0;
				CurrentHunger -= 1;
			}
		}
		UpdateHunger();
	}

	public void DealDamage(float damageValue)
	{
		//Deduct the damage dealt from the character's health
		CurrentHealth -= damageValue;
		healthBar.value = CalculateHealth();
		//if the character is out of health, die!
		if(CurrentHealth <= 0)
			Die();
	}

	float CalculateHealth()
	{
		return CurrentHealth / MaxHealth;
	}

	void Die()
	{
		CurrentHealth = 0;
		Destroy(gameObject);
		Debug.Log("You're Dead");
	}

	void UpdateHunger()
	{
		for(int i = 0; i < MaxHunger / 2; i++)
		{
			int calculation = CurrentHunger - (2 * i);
			if(calculation >= 2)
				hungerBar[i].sprite = hungerSprites[0];
			else if(calculation >= 1)
				hungerBar[i].sprite = hungerSprites[1];
			else
				hungerBar[i].sprite = hungerSprites[2];
		}
	}
}
