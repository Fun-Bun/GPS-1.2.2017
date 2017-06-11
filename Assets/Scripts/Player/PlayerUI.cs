using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [System.NonSerialized]
	public Player self;

    //UIs
    public Image healthBarUp;
    public Image healthBarDown;
    public float healthBarMax;

    public Image hungerBar;
    public float hungerBarMax;

    public Image ammoBar;
    public float ammoBarMax;

    public Image selectedWeapon;

    public Sprite emptySprite;
    public List<Sprite> weaponSprites;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateHunger();
        UpdateHealth();
        UpdateAmmo();
        UpdateWeaponImage();
	}

    void UpdateHealth()
    {
        healthBarUp.fillAmount = healthBarDown.fillAmount = self.status.health.GetPercent() * healthBarMax;
    }

    void UpdateHunger()
    {
        hungerBar.fillAmount = self.status.hunger.GetPercent() * hungerBarMax;

        /*
        for(int i = 0; i < self.status.hunger.max / 2; i++)
        {
            int calculation = self.status.hunger.value - (2 * i);

            if(calculation >= 2) hungerBar[i].sprite = hungerSprites[0];
            else if(calculation >= 1) hungerBar[i].sprite = hungerSprites[1];
            else hungerBar[i].sprite = hungerSprites[2];
        }
        */
    }

	void UpdateAmmo()
	{
        if(self.ownedWeapons.Capacity > 0)
            ammoBar.fillAmount = ((float)self.ownedWeapons[0].bulletCount / (float)self.ownedWeapons[0].bulletMax) * ammoBarMax;
        else
            ammoBar.fillAmount = 0f;
	}

    void UpdateWeaponImage()
    {
        if(self.ownedWeapons.Capacity > 0)
            selectedWeapon.sprite = weaponSprites[(int)self.ownedWeapons[0].type];
        else
            selectedWeapon.sprite = null;
    }
}
