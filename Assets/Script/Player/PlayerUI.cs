using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [System.NonSerialized]
	public Player self;

    //UIs
    public Slider healthBar;
    public List<Image> hungerBar;

    //Reference sprites
    public List<Sprite> hungerSprites;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateHunger();
        UpdateHealth();
	}

    void UpdateHealth()
    {
        healthBar.value = self.status.health.GetPercent();
    }

    void UpdateHunger()
    {
        for(int i = 0; i < self.status.hunger.max / 2; i++)
        {
            int calculation = self.status.hunger.value - (2 * i);

            if(calculation >= 2) hungerBar[i].sprite = hungerSprites[0];
            else if(calculation >= 1) hungerBar[i].sprite = hungerSprites[1];
            else hungerBar[i].sprite = hungerSprites[2];
        }
    }
}
