using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockOnlyIfClear : MonoBehaviour
{
    public DoorScript door;
    public List<GameObject> enemies;
	
	// Update is called once per frame
	void Update ()
    {
        if(enemies.Count > 0)
        {
            door.enabled = false;
            foreach(GameObject go in enemies)
            {
                if(go == null) enemies.Remove(go);
            }
        }
        else
        {
            door.enabled = true;
        }
	}
}
