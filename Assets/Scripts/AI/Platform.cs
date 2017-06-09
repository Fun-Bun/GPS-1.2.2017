using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

	public GameObject jumpStart1;
	public GameObject jumpEnd1;
	public GameObject jumpStart2;
	public GameObject jumpEnd2;

	public List<GameObject> whoStepOnMe;
		
	void OnTriggerStay2D(Collider2D other)
	{
		if(other.tag == "Player" || other.tag == "Enemy")
		{
			if(!whoStepOnMe.Contains(other.gameObject))
			{
				whoStepOnMe.Add(other.gameObject);
				if(other.tag == "Player")
					other.GetComponent<Player>().controls.platform = this;
				else if(other.tag == "Enemy")
					other.GetComponent<ZombieAI>().platform = this;
			}
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == "Player" || other.tag == "Enemy")
		{
			if(whoStepOnMe.Contains(other.gameObject))
			{
				whoStepOnMe.Remove(other.gameObject);
				if(other.tag == "Player")
					other.GetComponent<Player>().controls.platform = null;
				else if(other.tag == "Enemy")
					other.GetComponent<ZombieAI>().platform = null;
			}
		}
	}
}
