using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {

	float alphaFadeValue = 1.0f;

	int step = 0;
	float time;
	float lastStepTimestamp;

	public GameObject splashImage;
	public GameObject mainMenu;

	// Update is called once per frame
	void Update ()
	{
		time += Time.deltaTime;
		switch(step)
		{
			case 0:
				NextStepAfter(2.0f);
				break;
			case 1:
				FadeScreen(true, 1.0f, 0.0f, 0.0f, 0.0f);
				NextStepAfter(1.0f);
				break;
			case 2:
				FadeScreen(false, 1.0f, 0.0f, 0.0f, 0.0f);
				NextStepAfter(1.0f);
				break;
			case 3:
				splashImage.SetActive(false);
				mainMenu.SetActive(true);
				FadeScreen(true, 2.0f, 256.0f, 256.0f, 256.0f);
				NextStepAfter(2.0f);
				break;
			case 4:
				gameObject.SetActive(false);
				break;
		}
	}

	void FadeScreen(bool fadeIn, float second, float red, float green, float blue)
	{
		alphaFadeValue += Mathf.Clamp(Time.deltaTime / second, 0.0f, 1.0f) * (fadeIn ? -1 : 1);
		GetComponent<Image>().color = new Color(red, green, blue, alphaFadeValue);
	}

	void NextStepAfter(float second)
	{
		if(time - lastStepTimestamp >= second)
		{
			step++;
			lastStepTimestamp = time;
		}
	}
}
