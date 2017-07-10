using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpittingEnemyTargetScript : MonoBehaviour 
{
    public SpittingEnemyControllerAI master;
    public Platform platform;

    public float targetOffset;

    public void SetPosition(GameObject go)
    {
        this.transform.position = go.transform.position - new Vector3(0, targetOffset + 0.05f, 0);
    }

    public void SetPosition(Vector3 pos)
    {
        this.transform.position = pos - new Vector3(0, targetOffset + 0.05f, 0);
    }
}
