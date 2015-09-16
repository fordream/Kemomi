using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
    public List<Item> inventory = new List<Item>();
    private ItemDB database;

	// Use this for initialization
	void Start () {
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
}
