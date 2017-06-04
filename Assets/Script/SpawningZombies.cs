using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningZombies : MonoBehaviour {

	public Transform[] spawnPoints;
	public GameObject[] whatToSpawnPrefabs;
	public GameObject[] whatToSpawnClones;

	void OnTriggerEnter2D (Collider2D other)
	{
		if(other.tag == "Particle")
		{
			Destroy (gameObject, 0.02f);
		}

		whatToSpawnClones[0] = Instantiate(whatToSpawnPrefabs[0], spawnPoints[0].transform.position, Quaternion.Euler(0,0,0)) as GameObject;
		whatToSpawnClones[1] = Instantiate(whatToSpawnPrefabs[1], spawnPoints[1].transform.position, Quaternion.Euler(0,0,0)) as GameObject;
		whatToSpawnClones[2] = Instantiate(whatToSpawnPrefabs[2], spawnPoints[2].transform.position, Quaternion.Euler(0,0,0)) as GameObject;
		whatToSpawnClones[3] = Instantiate(whatToSpawnPrefabs[3], spawnPoints[3].transform.position, Quaternion.Euler(0,0,0)) as GameObject;
	}
}
