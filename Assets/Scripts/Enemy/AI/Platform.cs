using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

	public GameObject jumpStart1;
	public GameObject jumpEnd1;
	public GameObject jumpStart2;
	public GameObject jumpEnd2;

	public List<GameObject> whoStepOnMe;
    
    public float halfLength;
	
    void Start()
    {
        halfLength = GetComponent<BoxCollider2D>().bounds.extents.x;
    }

	void OnTriggerStay2D(Collider2D other)
	{
        if(other.tag == "Target" || other.tag == "Enemy")
		{
			if(!whoStepOnMe.Contains(other.gameObject))
			{
				whoStepOnMe.Add(other.gameObject);
                if(other.tag == "Target")
                    other.GetComponent<TargetScript>().platform = this;
				else if(other.tag == "Enemy")
					other.GetComponent<EnemyControllerAI>().platform = this;
			}
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
        if(other.tag == "Target" || other.tag == "Enemy")
		{
			if(whoStepOnMe.Contains(other.gameObject))
			{
				whoStepOnMe.Remove(other.gameObject);
                if(other.tag == "Target")
                    other.GetComponent<TargetScript>().platform = null;
                else if(other.tag == "Enemy")
					other.GetComponent<EnemyControllerAI>().platform = null;
			}
		}
	}
}
