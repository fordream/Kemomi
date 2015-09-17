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
			inventory.Add (new EmptyItem());
		}
        itemDB = GameObject.FindGameObjectWithTag("ItemDB").GetComponent<ItemDB>();
		AddItem (0);
		AddItem (1);
		AddItem (1);
		AddItem (2);
		AddItem (2);
		AddItem (2);

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
				if (selectedInventoryIndex != -1) GUI.DrawTexture (selectedInventoryRect, Resources.Load<Texture2D> ("ItemIcons/selected"));

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
							tapItem(i);
						}
						break;
					}
				}
			}
		}
	}

	void tapItem(int tappedInventoryIndex){
		if (inventory[tappedInventoryIndex] is ItemWithDirection) {
			
			// 既に同じアイテムが使用可能になっていたら・・・
			if (tappedInventoryIndex == selectedInventoryIndex) {
				selectedInventoryIndex = -1; // 使用可能アイテムを解除する。
				// ここに光らせたアイコンを戻す処理を入れてください
			} else {
				selectedInventoryIndex = tappedInventoryIndex;
				// ここに選択したアイテムのアイコンを光らせる処理を入れてください
			}
			
		} else if (inventory[tappedInventoryIndex] is OneTouchItem) {
			// ワンタッチアイテムを使用し、正常に消費されたら、インベントリから除去する
			if (((OneTouchItem)inventory[tappedInventoryIndex]).Effect()) {
				RemoveItem(tappedInventoryIndex);
				selectedInventoryIndex = -1; // 使用可能アイテムを解除する。
			}
		}
	}

	void RemoveItem(int inventoryIndex){
		inventory[inventoryIndex] = new EmptyItem();
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
