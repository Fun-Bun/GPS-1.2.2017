using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	//System
	public SpriteRenderer renderer;
	public BoxCollider2D collider;
	public Rigidbody2D rigidbody;

	//Developer
	public PlayerControls controls;
	public PlayerStatus status;
    public PlayerInventory inventory;
    public PlayerCamera camera;
    public PlayerUI ui;

    public PlayerWeapon[] weapons;

	//Initialization
	void Awake ()
	{
		renderer = GetComponent<SpriteRenderer>();
		collider = GetComponent<BoxCollider2D>();
		rigidbody = GetComponent<Rigidbody2D>();

		controls = GetComponent<PlayerControls>();
		status = GetComponent<PlayerStatus>();
        inventory = GetComponent<PlayerInventory>();
        //camera = GetComponentInChildren<PlayerCamera>();
        ui = GetComponent<PlayerUI>();
        weapons = GetComponentsInChildren<PlayerWeapon>();

		if(controls != null) controls.self = this;
        if(status != null) status.self = this;
        if(inventory != null) inventory.self = this;
        if(camera != null) camera.self = this;
        if(ui != null) ui.self = this;
        foreach(PlayerWeapon pw in weapons) pw.self = this;
	}
}
