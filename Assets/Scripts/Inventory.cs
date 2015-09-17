using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	public int inventorySize;
	public GUISkin skin;
    public List<Item> inventory = new List<Item>();
	public int selectedInventoryIndex;
	public List<Rect> inventoryRects = new List<Rect>();
	
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
			inventory.Add(new EmptyItem());
			inventoryRects.Add(new Rect(Screen.width - 2*32 - 2*42 * i, Screen.height - 2*32, 2*32, 2*32));
		}
        itemDB = GameObject.FindGameObjectWithTag("ItemDB").GetComponent<ItemDB>();
		selectedInventoryIndex = -1;
		inputBeganInventoryIndex = -1;
		inputPrevInventoryIndex = -1;
		inputEndInventoryIndex = -1;
		obtainItem (1);
		obtainItem (1);
		obtainItem (1);
		obtainItem (1);
		obtainItem (2);
		obtainItem (2);
		obtainItem (3);

	}

	void Update () {
//		print("inventory[0]=" + inventory[0].itemName);
//		print("inventory[1]=" + inventory[1].itemName);
//		print("inventory[2]=" + inventory[2].itemName);
//		print("inventory[3]=" + inventory[3].itemName);
//		print("inventory[4]=" + inventory[4].itemName);
	}

	void OnGUI(){
		GUI.skin = skin;

		Touch[] touches = Input.touches;
		if (showInventory){
			inputOnInventory(touches);
			drawInventory ();
		}
	}

	void drawInventory(){
		for (int i=0; i<inventorySize; i++) {

			string inventoryCount = "";
			if (inventory [i].itemCount > 1) inventoryCount = inventory [i].itemCount.ToString ();
			GUI.Box (inventoryRects[i], inventoryCount, skin.GetStyle ("slot"));

			if (inventory [i].itemName != null) GUI.DrawTexture (inventoryRects[i], inventory [i].itemIcon);
			if (selectedInventoryIndex != -1) GUI.DrawTexture (inventoryRects[selectedInventoryIndex], Resources.Load<Texture2D> ("ItemIcons/selected"));

		}
	}

	void inputOnInventory(Touch[] touches){
		foreach (var touch in Input.touches) { // input
			Vector2 touchPos = new Vector2 (touch.position.x, Screen.height - touch.position.y);
//			print (touchPos);
			switch (touch.phase) {
			case TouchPhase.Began:
				inputBeganPos = touchPos;
				inputPrevPos = touchPos;
				for(int i=0; i<inventorySize; i++){
					if (inventoryRects[i].Contains (inputBeganPos)) {
						inputBeganInventoryIndex = i;
						inputPrevInventoryIndex = i;
//						print ("inventory["+i+"]began");
					}
				}
				break;
				
			case TouchPhase.Moved:
				inputPrevPos = touchPos;
				for(int i=0; i<inventorySize; i++){
					if (inventoryRects[i].Contains (inputPrevPos)) {
						inputPrevInventoryIndex = i;
//						print ("inventory["+i+"]moving");
					}
				}
				break;
				
			case TouchPhase.Ended:
				inputEndPos = touchPos;
				for(int i=0; i<inventorySize; i++){
					if (inventoryRects[i].Contains (inputEndPos)) {
						inputPrevInventoryIndex = i;
						inputEndInventoryIndex = i;
//						print ("inventory["+i+"]end");
					}
				}
				print ("beganindex =" + inputBeganInventoryIndex);
				if ( inputBeganInventoryIndex == inputEndInventoryIndex && inputEndInventoryIndex != -1) {
					tapItem (inputEndInventoryIndex);
				} 
				else if (inputBeganInventoryIndex != -1 && inputEndInventoryIndex != -1) {
					switchItemByInventoryIndex (inputBeganInventoryIndex, inputEndInventoryIndex);
				}
				else if (inputBeganInventoryIndex != -1 && inputEndPos.y < Screen.height / 1.5){
					removeItemByInventoryIndex(inputBeganInventoryIndex);
				}

				print ("endindex =" + inputEndInventoryIndex);

				inputBeganInventoryIndex = -1;
				inputPrevInventoryIndex = -1;
				inputEndInventoryIndex = -1; 
				
				break;
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
			// ワンタッチアイテムを使用し、正常に消費されたら,kazuwoherasu
			if (((OneTouchItem)inventory[tappedInventoryIndex]).Effect()) {
				consumeItemByItemId(inventory[tappedInventoryIndex].itemID);
				selectedInventoryIndex = -1; // 使用可能アイテムを解除する。
			}
		}
	}

	void switchItemByInventoryIndex(int from, int to){
		Item tmp = inventory[from];
		inventory [from] = inventory [to];
		inventory [to] = tmp;
		selectedInventoryIndex = -1;
	}

	void consumeItemByItemId(int id){
		for (int i=0; i<inventory.Count; i++) {
			if (inventory[i].itemID == id){
				if(inventory[i].itemCount > 1) inventory[i].itemCount--;
				else inventory[i] = new EmptyItem();
			}
		}
	}

	void removeItemByInventoryIndex(int inventoryIndex){
		inventory[inventoryIndex] = new EmptyItem();
		selectedInventoryIndex = -1;
	}

	void obtainItem(int id){
		for (int i=0; i<inventory.Count; i++) { //持ってるアイテムを増やす
			if (inventory[i].itemID == id && inventory[i].itemCount < inventory[i].MaxStack) {
				inventory[i].itemCount++;
				return;
			}
		}
		for (int i=0; i<inventory.Count; i++) { //初めて拾うアイテム
			if (inventory[i].itemName == null){
				for (int j=0; j<itemDB.items.Count; j++) {
					if (itemDB.items[j].itemID == id) {
						inventory[i] = itemDB.items[j];
						inventory[i].itemCount = 1;
						break;
					}
				}
				break;
			}
		}
	}

	bool inventoryContains(int id)
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
