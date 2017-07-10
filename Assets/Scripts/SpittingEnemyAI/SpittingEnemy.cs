using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpittingEnemy : MonoBehaviour 
{
    public SpriteRenderer renderer;
    public BoxCollider2D collider;
    public Rigidbody2D rigidbody;
    public Animator animator;

    public SpittingEnemyControllerAI controls;
    public SpittingEnemyStatus status;

    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        controls = GetComponent<SpittingEnemyControllerAI>();
        status = GetComponent<SpittingEnemyStatus>();

        if(controls != null)
        {
            controls.self = this;
        }

        if(status != null)
        {
            status.self = this;
        }
    }
}
