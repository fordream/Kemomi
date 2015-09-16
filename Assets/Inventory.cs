using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	public int slotsX, slotsY;
    public List<Item> inventory = new List<Item>();
	public List<Item> slots = new List<Item> ();
    private ItemDB database;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < (slotsX * slotsY){
			slots.Add(new Item());
		}
        database = GameObject.FindGameObjectWithTag("ItemDB").GetComponent<ItemDB>();
		inventory.Add (database.items [0]);
		inventory.Add (database.items [1]);
		inventory.Add (database.items [2]);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		for (int i = 0; i < inventory.Count; i++){
			GUI.Label (new Rect(10, i*20, 200, 50), inventory[i].itemName);
		}
	}

	void DrawInventory(){
	}
}
