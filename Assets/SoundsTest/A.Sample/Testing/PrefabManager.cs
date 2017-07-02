using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PrefabManager : MonoBehaviour
{
    public GameObject SoundManagerPrefab;

    private static PrefabManager mInstance = null;

    public static PrefabManager Instance
    {
        get
        {
            if (mInstance == null)
            {
                GameObject TempObject = GameObject.FindWithTag("PrefabManager");
                if (TempObject == null)
                {
                    Debug.LogError("PrefabManagerScript DOES NOT EXIST IN THE SCENE!!!");

                }
                else
                {
                    mInstance = TempObject.GetComponent<PrefabManager>();
                }

            }
            return mInstance;
        }
    }
}