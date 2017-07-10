using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [System.NonSerialized]
	public Player self;

    //UIs
	public Image[] healthImages;

	public GameObject ammoBar;
	public Image[] ammoImages;

    public Image selectedWeapon;
	
	public List<Sprite> weaponSprites;
	public List<Sprite> healthSprites;
	public List<Sprite> ammoSprites;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		UpdateHealth();
		UpdateWeaponImage();
        UpdateAmmo();
	}

    public void UpdateHealth()
    {
        for(int i = 0; i < self.status.health.max / 2; i++)
        {
			int cal = self.status.health.value - (2 * i);

			if(cal >= 2) healthImages[i].sprite = healthSprites[2];
			else if(cal >= 1) healthImages[i].sprite = healthSprites[1];
			else healthImages[i].sprite = healthSprites[0];
        }
    }

	void UpdateAmmo()
	{
		if(self.ownedWeapons.Capacity > 0)
		{
			ammoBar.SetActive(true);
			for(int i = 0; i < self.ownedWeapons[0].bullet.max; i++)
			{
				int cal = self.ownedWeapons[0].bullet.value - i;

				if(cal >= 1) ammoImages[i].sprite = ammoSprites[1];
				else ammoImages[i].sprite = ammoSprites[0];
			}
		}
	}

    void UpdateWeaponImage()
    {
        if(self.ownedWeapons.Capacity > 0)
            selectedWeapon.sprite = weaponSprites[(int)self.ownedWeapons[0].type];
        else
            selectedWeapon.sprite = null;
    }
}
