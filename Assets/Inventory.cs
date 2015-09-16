using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	public int slotsX, slotsY;
	public GUISkin skin;
    public List<Item> inventory = new List<Item>();
	public List<Item> slots = new List<Item> ();
    private ItemDB database;
	private bool showInventory;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < (slotsX * slotsY); i++){
			slots.Add(new Item());
		}
        database = GameObject.FindGameObjectWithTag("ItemDB").GetComponent<ItemDB>();
		inventory.Add (database.items [0]);
		inventory.Add (database.items [1]);
		inventory.Add (database.items [2]);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Inventory")) {
			showInventory = !showInventory;
		}
		showInventory = true;
	}

	void OnGUI(){
		GUI.skin = skin;

		if (showInventory){
			DrawInventory ();
		}
//		for (int i = 0; i < inventory.Count; i++){
//			GUI.Label (new Rect(10, i*20, 200, 50), inventory[i].itemName);
//		}
	}


	void DrawInventory(){
		for (int x = 0; x < slotsX; x++){
			for(int y = 0; y < slotsY; y++){
				GUI.Box (new Rect(x*20, y*20, 20, 20), y.ToString(), skin.GetStyle("slot"));
			}
		}
	}
}
