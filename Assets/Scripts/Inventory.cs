using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	public int inventorySize;
	public GUISkin skin;
    public List<Item> inventory = new List<Item>();
	public int selectedInventoryIndex;

    private ItemDB itemDB;
	private bool showInventory;

	private Vector2 inputBeganPos;
	private int inputBeganInventoryIndex;
	private Vector2 inputPrevPos;
	private int inputPrevInventoryIndex;
	private Vector2 inputEndPos;
	private int inputEndInventoryIndex;

	
	void Start () {
		showInventory = true;
		for (int i=0; i<inventorySize; i++){
			inventory.Add (new Item());
		}
        itemDB = GameObject.FindGameObjectWithTag("ItemDB").GetComponent<ItemDB>();
		AddItem (0);
		AddItem (1);
		AddItem (1);
		AddItem (2);
		AddItem (3);
		AddItem (4);
		AddItem (5);
		AddItem (6);
		AddItem (7);
		AddItem (8);
		AddItem (8);
		AddItem (9);
		RemoveItem (0);
	}

	void Update () {
	}

	void OnGUI(){
		GUI.skin = skin;

		Touch[] touches = Input.touches;
		if (showInventory){
			DrawInventory (touches);
		}
	}

	void DrawInventory(Touch[] touches){
		for (int i=0; i<inventorySize; i++) {
			
			Rect inventoryRect = new Rect (Screen.width - 32 - 42 * i, Screen.height - 32, 32, 32);
			Rect selectedInventoryRect = new Rect (Screen.width - 32 - 42 * selectedInventoryIndex, Screen.height - 32, 32, 32);

			string inventoryCount = "";
			if (inventory [i].itemCount > 0)
				inventoryCount = inventory [i].itemCount.ToString ();
			GUI.Box (inventoryRect, inventoryCount, skin.GetStyle ("slot"));

			if (inventory [i].itemName != null) {
				GUI.DrawTexture (inventoryRect, inventory [i].itemIcon);
				GUI.DrawTexture (selectedInventoryRect, Resources.Load<Texture2D> ("ItemIcons/selected"));

				foreach (var touch in Input.touches) { // input

					Vector2 touchPos = new Vector2 (touch.position.x, Screen.height - touch.position.y);

					switch (touch.phase) {
					case TouchPhase.Began:
						inputBeganPos = touchPos;

						inputPrevPos = touchPos;
						break;
						
					case TouchPhase.Moved:
						inputPrevPos = touchPos;
						break;
						
					case TouchPhase.Ended:
						inputEndPos = touchPos;
						if (inventoryRect.Contains (inputEndPos)) {
							selectedInventoryIndex = i;
						}
						break;
					}
				}
			}
		}
	}

	void RemoveItem(int id){
		for (int i=0; i<inventory.Count; i++) {
			if (inventory[i].itemID == id){
				inventory[i] = new Item();
				break;
			}
		}
	}

	void AddItem(int id){
		for (int i=0; i<inventory.Count; i++) {
			if (inventory[i].itemID == id) {
				inventory[i].itemCount++;
				return;
			}
		}
		for (int i=0; i<inventory.Count; i++) {
			if (inventory[i].itemName == null){
				for (int j=0; j<itemDB.items.Count; j++) {
					if (itemDB.items[j].itemID == id) {
						inventory[i] = itemDB.items[j];
						inventory[i].itemCount++;
						break;
					}
				}
				break;
			}
		}
	}

	bool InventoryContains(int id)
	{
		bool result = false;
		for (int i=0; i<inventory.Count; i++){
			result = inventory[i].itemID == id;
			if(result){
				break;
			}
		}
		return result;
	}
}
