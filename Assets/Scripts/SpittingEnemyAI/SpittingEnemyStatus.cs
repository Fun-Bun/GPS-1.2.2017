using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpittingEnemyStatus : MonoBehaviour {

    [System.NonSerialized]
    public SpittingEnemy self;
   
    public float HP;
    public GameObject damageIndicator;
    public float AttackStats = 1;

    //Movement variables
    public float movementSpeed;
    public float jumpHeight;

	// Update is called once per frame
	void Update () 
    {
        if(HP <= 0) Destroy(gameObject);
	}

    public void Hurt(bool isCrit)
    {
        float finalDmg = AttackStats * (isCrit ? 3 : 1);
        HP -= finalDmg;

        FloatingText(finalDmg.ToString(), (int)finalDmg, (isCrit ? Color.red : Color.yellow));
    }

    public void FloatingText(string text, int damage = 1, Color? textColor = null)
    {
        GameObject textGO = Instantiate(damageIndicator);
        textGO.transform.SetParent(GameObject.Find("Canvas").transform);

        Text temptext = textGO.GetComponent<Text>();
        temptext.resizeTextForBestFit = true;
        temptext.alignment = TextAnchor.MiddleCenter;
        temptext.text = text;
        temptext.color = textColor ?? Color.white;

        RectTransform tempRect = textGO.GetComponent<RectTransform>();
        tempRect.transform.position = transform.position + (Vector3.up * 0.4f);
        tempRect.transform.localScale = damageIndicator.transform.localScale / 3 * damage;

        textGO.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 2, ForceMode2D.Impulse);

        Destroy(textGO, 0.3f);
    }
}
