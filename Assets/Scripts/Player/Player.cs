using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	//System
	public SpriteRenderer renderer;
	public BoxCollider2D collider;
	public Rigidbody2D rigidbody;
	public Animator animator;

	//Developer
	public PlayerControls controls;
	public PlayerStatus status;
    public PlayerInventory inventory;
    public PlayerCamera camera;
    public PlayerUI ui;

	public List<PlayerWeapon> ownedWeapons;

	//Weapon Prefab
	public GameObject weaponPrefab;

	//Initialization
	void Awake ()
	{
		renderer = GetComponent<SpriteRenderer>();
		collider = GetComponent<BoxCollider2D>();
		rigidbody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();

		controls = GetComponent<PlayerControls>();
		status = GetComponent<PlayerStatus>();
        inventory = GetComponent<PlayerInventory>();
        //camera = GetComponentInChildren<PlayerCamera>();
        ui = GetComponent<PlayerUI>();

		if(controls != null) controls.self = this;
        if(status != null) status.self = this;
        if(inventory != null) inventory.self = this;
        if(camera != null) camera.self = this;
        if(ui != null) ui.self = this;
	}
}
