using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Troop
{
	public List<GameObject> prefab;
	public int spawnRatio;
}

public class SpawningZombies : MonoBehaviour
{
	public List<GameObject> spawnPointList;
	public List<Troop> spawnTroopList;
	public List<GameObject> spawnedClones;

	void Start()
	{
		int total = 0;
		foreach(Troop t in spawnTroopList)
		{
			total += t.spawnRatio;
		}

		int currentRatio = 0;
		int chosenSpawn = Random.Range(1, total);
		Debug.Log(chosenSpawn);

		foreach(Troop t in spawnTroopList)
		{
			currentRatio += t.spawnRatio;

			if(currentRatio > chosenSpawn)
			{
				for(int i = 0; i < t.prefab.Count; i++)
				{
					spawnedClones.Add(Instantiate(t.prefab[i], spawnPointList[Random.Range(0, spawnPointList.Count - 1)].transform.position, Quaternion.identity));
				}
				break;
			}
		}
	}
}
