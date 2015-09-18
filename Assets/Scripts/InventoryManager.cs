using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour {
	public int inventorySize; //スロット数
	public GUISkin skin; //スロットのスキン
    public List<Item> inventory = new List<Item>();
	public int selectedInventoryIndex;
	public List<Rect> inventoryRects = new List<Rect>(); //各スロットの四角
	public bool showInventory; //trueのときインベントリを表示

    private ItemDB itemDB; //全アイテムのリスト

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

			//インベントリのレイアウト
			inventoryRects.Add(new Rect(Screen.width - 2*32 - 2*42 * i, Screen.height - 2*32, 2*32, 2*32));
		}
        itemDB = GameObject.FindGameObjectWithTag("ItemDB").GetComponent<ItemDB>();
		selectedInventoryIndex = -1;
		inputBeganInventoryIndex = -1;
		inputPrevInventoryIndex = -1;
		inputEndInventoryIndex = -1;

		//デバッグ用
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

	//GUIイベントが発生するたびに呼ばれる
	void OnGUI(){
		GUI.skin = skin;
		if (showInventory){
			Touch[] touches = Input.touches;
			inputOnInventory(touches);
			drawInventory ();
		}
	}

	void drawInventory(){ //インベントリを描画 ボックスとアイテム数とアイテムアイコンと選択の光を描画
		for (int i=0; i<inventorySize; i++) {

			// ボックスと個数
			string inventoryCount = "";
			if (inventory[i].itemCount > 1) inventoryCount = inventory[i].itemCount.ToString ();
			GUI.Box (inventoryRects[i], inventoryCount, skin.GetStyle ("slot")); 

			// アイテムアイコン
			if (inventory[i].itemName != null) GUI.DrawTexture (inventoryRects[i], inventory[i].itemIcon);

			//選択されているアイテムの光
			if (selectedInventoryIndex != -1) GUI.DrawTexture (inventoryRects[selectedInventoryIndex], Resources.Load<Texture2D> ("ItemIcons/selected")); 

		}
	}

	void inputOnInventory(Touch[] touches){

		foreach (var touch in Input.touches) { // 1箇所以上タッチされている時
			Vector2 touchPos = new Vector2 (touch.position.x, Screen.height - touch.position.y);
//			print (touchPos);
			switch (touch.phase) {
			//タッチ開始時
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
			
			//タッチしたまま移動時
			case TouchPhase.Moved:
				inputPrevPos = touchPos;
				for(int i=0; i<inventorySize; i++){
					if (inventoryRects[i].Contains (inputPrevPos)) {
						inputPrevInventoryIndex = i;
//						print ("inventory["+i+"]moving");
					}
				}
					if (inputPrevInventoryIndex != -1 && !inventoryRects[inputPrevInventoryIndex].Contains(touchPos)){
						
					}
				break;
			
			//タッチ終了時
			case TouchPhase.Ended:
				inputEndPos = touchPos;
				for(int i=0; i<inventorySize; i++){
					if (inventoryRects[i].Contains (inputEndPos)) {
						inputPrevInventoryIndex = i;
						inputEndInventoryIndex = i;
//						print ("inventory["+i+"]end");
					}
				}
				
				//タップ時 タッチ開始スロットとタッチ終了スロットが両方存在して同じ時
				if (inputBeganInventoryIndex == inputEndInventoryIndex && inputEndInventoryIndex != -1) {
					tapItem (inputEndInventoryIndex);
				}//入れ替え時 タッチ開始スロットとタッチ終了スロットが両方存在して違うとき
				
				else if (inputBeganInventoryIndex != -1 && inputEndInventoryIndex != -1) {
					switchItemByInventoryIndex (inputBeganInventoryIndex, inputEndInventoryIndex);
				}//スワイプで捨てるとき タッチ開始スロットが存在してタッチ終了点は上のほうとき
				else if (inputBeganInventoryIndex != -1 && inputEndPos.y < Screen.height / 1.5){
					removeItemByInventoryIndex(inputBeganInventoryIndex);
				}

				// FIXME ↓ beganindexがなんかおかしいのに動いてる
				// print ("beganindex =" + inputBeganInventoryIndex);
				// print ("endindex =" + inputEndInventoryIndex);

				//タッチ終了時にリセット
				inputBeganInventoryIndex = -1;
				inputPrevInventoryIndex = -1;
				inputEndInventoryIndex = -1; 
				
				break;
			}
		}
	}

	//アイテムタップ時の処理
	void tapItem(int tappedInventoryIndex){
		// 選択したあとフリックして撃つアイテムの場合
		if (inventory[tappedInventoryIndex] is ItemWithDirection) {
			
			//選択されているアイテムを再びタップした場合選択解除
			if (tappedInventoryIndex == selectedInventoryIndex) {
				selectedInventoryIndex = -1;
			} 
			else //選択 
			{ 
				selectedInventoryIndex = tappedInventoryIndex;
			}
		// ワンタッチアイテムの場合
		}else if(inventory[tappedInventoryIndex] is OneTouchItem) {
			// ワンタッチアイテムを使用し、正常に消費されたら選択解除
			if (((OneTouchItem)inventory[tappedInventoryIndex]).Effect()) {
				consumeItemByItemId(inventory[tappedInventoryIndex].itemID);
				selectedInventoryIndex = -1;
			}
		}
	}

	//スロットのスワップ
	void switchItemByInventoryIndex(int from, int to){
		Item tmp = inventory[from];
		inventory [from] = inventory [to];
		inventory [to] = tmp;
		selectedInventoryIndex = -1;
	}

	//アイテム消費 個数を1へらす 残り1個のときは空にする
	void consumeItemByItemId(int id){
		for (int i=0; i<inventory.Count; i++) {
			if (inventory[i].itemID == id){
				if(inventory[i].itemCount > 1) inventory[i].itemCount--;
				else inventory[i] = new EmptyItem();
			}
		}
	}

	//アイテムをスワイプして捨てる
	void removeItemByInventoryIndex(int inventoryIndex){
		inventory[inventoryIndex] = new EmptyItem();
		selectedInventoryIndex = -1;
	}

	//アイテム入手 いっぱいだった時や登録されてないアイテムだった場合はfalse
	bool obtainItem(int id){
		//すでに持っていてスタックできるアイテムの場合は個数を増やす
		for (int i=0; i<inventory.Count; i++) { 
			if (inventory[i].itemID == id && inventory[i].itemCount < inventory[i].MaxStack) {
				inventory[i].itemCount++;
				return true;
			}
		}
		//スタックできないアイテムは登録
		for (int i=0; i<inventory.Count; i++) { 
			if (inventory[i].itemName == null){
				for (int j=0; j<itemDB.items.Count; j++) {
					if (itemDB.items[j].itemID == id) {
						inventory[i] = itemDB.items[j];
						inventory[i].itemCount = 1;
						return true;
					}
				}
				break;
			}
		}
		return false;
	}

	//idのアイテムを持ってるか判断
	bool inventoryContains(int id){
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
