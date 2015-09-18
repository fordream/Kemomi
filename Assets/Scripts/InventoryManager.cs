using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour {
	public int inventorySize; //スロット数
	public GUISkin skin; //スロットのスキン
    public List<Slot> inventory = new List<Slot>();
	public List<Item> items = new List<Item>(); //全アイテムのリスト
	public int selectedInventoryIndex;
	public int movingInventoryIndex;
	public int movingFingerId;
	public bool isShowingInventory; //trueのときインベントリを表示
	

	private Vector2 inputBeganPos;
	private int inputBeganInventoryIndex;
	private Vector2 inputPrevPos;
	private int inputPrevInventoryIndex;
	private Vector2 inputEndPos;
	private int inputEndInventoryIndex;

	private const float SCREENSCALE = 2;

	void Awake(){
		// string name, int id, string desc, string itemIconPath
		items.Add(new EmptyItem("", 0, "", ""));
		items.Add(new Shuriken("手裏剣", 1, "", "Shuriken"));
		items.Add(new HPPortion("HP回復薬", 2, "", "HPPortion"));
		items.Add(new Taiatari("体当たり", 3, "", "Taiatari"));
		items.Add(new Ofuda("お札", 4, "", "Ofuda"));
		items.Add(new SPMugenPortion("SP無限薬", 5, "", "SPMugenPortion"));
		items.Add(new FukkatsuSou("復活草", 6, "", "FukkatsuSou"));
		items.Add(new RandomMapIdou("ランダムマップ移動", 7, "", "RandomMapIdou"));
		items.Add(new UchiageHanabi("打ち上げ花火", 8, "", "UchiageHanabi"));
		items.Add(new Shougekiha("衝撃波", 9, "", "Shougekiha"));
		items.Add(new Kaitengiri("回転斬り", 10, "", "Kaitengiri"));
	}

	void Start () {

		isShowingInventory = true;

		for (int i=0; i<inventorySize; i++){
			inventory.Add(new Slot(i, SCREENSCALE));
		}
	

		selectedInventoryIndex = -1;
		movingInventoryIndex = -1;
		movingFingerId = -1;
		inputBeganInventoryIndex = -1;
		inputPrevInventoryIndex = -1;
		inputEndInventoryIndex = -1;


		//デバッグ用
		obtainItem (1);obtainItem (1);obtainItem (1);obtainItem (1);obtainItem (1);
		obtainItem (1);obtainItem (1);obtainItem (1);obtainItem (1);obtainItem (1);
		obtainItem (1);obtainItem (1);obtainItem (1);obtainItem (1);obtainItem (1);
		obtainItem (1);obtainItem (1);obtainItem (1);obtainItem (1);obtainItem (1);
		obtainItem (1);obtainItem (1);obtainItem (1);obtainItem (1);obtainItem (1);
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

		if (isShowingInventory){
			Touch[] touches = Input.touches;

			drawInventory ();
			inputOnInventory(touches);
		}
	}

	void drawInventory(){ //インベントリを描画 ボックスとアイテム数とアイテムアイコンと選択の光を描画
		for (int i=0; i<inventorySize; i++) {

			// ボックスと個数
			string slotCountString = "";
			print("inventory["+i+"]"+inventory[i].itemCount);
			if (inventory[i].itemCount > 1) slotCountString = inventory[i].itemCount.ToString ();
			GUI.Box (inventory[i].slotRect, slotCountString, skin.GetStyle ("slot"));

			// アイテムアイコン
			if (inventory[i].item.itemName != null && i != movingInventoryIndex) GUI.DrawTexture (inventory[i].slotRect, inventory[i].item.itemIcon);

			//選択されているアイテムの光
			if (selectedInventoryIndex != -1) GUI.DrawTexture (inventory[selectedInventoryIndex].slotRect, Resources.Load<Texture2D> ("ItemIcons/selected")); 

		}
	}

	void inputOnInventory(Touch[] touches){

		foreach (var touch in Input.touches) { 
			Vector2 touchPos = new Vector2 (touch.position.x, Screen.height - touch.position.y);
//			print (touchPos);
			// ドラッグ状態の時はその指1本しかトラックしない
			if ( !(movingFingerId == -1 || movingFingerId == touch.fingerId) ) break;
			switch (touch.phase) {
			//タッチ開始時
			case TouchPhase.Began:
				inputBeganPos = touchPos;
				inputPrevPos = touchPos;
				for(int i=0; i<inventorySize; i++){
					if (inventory[i].slotRect.Contains (inputBeganPos)) {
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
					if (inventory[i].slotRect.Contains (inputPrevPos)) {
						inputPrevInventoryIndex = i;
//						print ("inventory["+i+"]moving");
					}
				}

				// 指をスロットからはみ出すように動かした場合はドラッグ状態に移行してそれ以外の指をブロック
				if (inputBeganInventoryIndex != -1 && !inventory[inputBeganInventoryIndex].slotRect.Contains(touchPos)){
					movingFingerId = touch.fingerId;
					movingInventoryIndex = inputBeganInventoryIndex;
				}

				break;
			
			//タッチ終了時
			case TouchPhase.Ended:
				inputEndPos = touchPos; 
				for(int i=0; i<inventorySize; i++){
					if (inventory[i].slotRect.Contains (inputEndPos)) {
						inputPrevInventoryIndex = i;
						inputEndInventoryIndex = i;
//						print ("inventory["+i+"]end");
					}
				}
				
				//タップ時 タッチ開始スロットとタッチ終了スロットが両方存在して同じ時
				if (inputBeganInventoryIndex == inputEndInventoryIndex && inputEndInventoryIndex != -1) {
					tapItem (inputEndInventoryIndex);
				}//入れ替え時
				
				else if (inputBeganInventoryIndex != -1 && inputEndInventoryIndex != -1) {
					swapItem (inputBeganInventoryIndex, inputEndInventoryIndex);
				}//スワイプで捨てるとき 
				else if (inputBeganInventoryIndex != -1 && inputEndPos.y < Screen.height / 1.8){
					removeItem(inputBeganInventoryIndex);
				}

				// FIXME ↓ beganindexがなんかおかしいのに動いてる
				// print ("beganindex =" + inputBeganInventoryIndex);
				// print ("endindex =" + inputEndInventoryIndex);

				//タッチ終了時にリセット
				
				inputBeganInventoryIndex = -1;
				inputPrevInventoryIndex = -1;
				inputEndInventoryIndex = -1; 
				movingInventoryIndex = -1;
				movingFingerId = -1;
				
				break;
			}

				// 入力が必要な描画なので仕方なくdrawInventoryでなくここに書いてる
				if (movingFingerId != -1) GUI.DrawTexture(new Rect(touchPos.x - 32, touchPos.y - 32, SCREENSCALE*32, SCREENSCALE*32), inventory[inputBeganInventoryIndex].item.itemIcon);
			
		}
	}

	//アイテムタップ時の処理
	void tapItem(int tappedInventoryIndex){
		// 選択したあとフリックして撃つアイテムの場合
		if (inventory[tappedInventoryIndex].item is ItemWithDirection) {
			
			//選択されているアイテムを再びタップした場合選択解除
			if (tappedInventoryIndex == selectedInventoryIndex) {
				selectedInventoryIndex = -1;
			} 
			else //選択 
			{ 
				selectedInventoryIndex = tappedInventoryIndex;
			}
		// ワンタッチアイテムの場合
		}else if(inventory[tappedInventoryIndex].item is OneTouchItem) {
			// ワンタッチアイテムを使用し、正常に消費されたら選択解除
			if (((OneTouchItem)inventory[tappedInventoryIndex].item).Effect()) {
				consumeItem(tappedInventoryIndex);
				selectedInventoryIndex = -1;
			}
		}
	}

	//スロットのスワップ
	// HACK: Slotをスワップするとなぜか動かないのでアイテムと個数をばらばらにスワップ
	void swapItem(int from, int to){
		Item tmp = inventory[from].item;
		int tmpCount = inventory [from].itemCount;

		inventory[from].item = inventory[to].item;
		inventory[from].itemCount = inventory[to].itemCount;
		inventory[to].item = tmp;
		inventory[to].itemCount = tmpCount;

		selectedInventoryIndex = -1;
	}

	//アイテム消費 個数を1へらす 残り1個のときは空にする
	void consumeItem(int inventoryIndex){
		if (inventory [inventoryIndex].itemCount > 1)
			inventory [inventoryIndex].itemCount--;
		else
			inventory [inventoryIndex] = new Slot(inventoryIndex, SCREENSCALE);
	}

	//アイテムをスワイプして捨てる
	void removeItem(int inventoryIndex){
		inventory[inventoryIndex] = new Slot(inventoryIndex, SCREENSCALE);
		selectedInventoryIndex = -1;
	}

	//アイテム入手 いっぱいだった時や登録されてないアイテムだった場合はfalse
	bool obtainItem(int id){
		//すでに持っていてスタックできるアイテムの場合は個数を増やす
		for (int i=0; i<inventory.Count; i++) { 
			if (inventory[i].item.itemID == id && inventory[i].itemCount < inventory[i].item.MaxStack) {
				inventory[i].itemCount++;
				return true;
			}
		}
		//スタックできないアイテムは登録
		for (int i=0; i<inventory.Count; i++) { 
			if (inventory[i].item.itemName == null){
				for (int j=0; j<items.Count; j++) {
					if (items[j].itemID == id) {
						inventory[i].item = items[j];
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
			result = inventory[i].item.itemID == id;
			if(result){
				break;
			}
		}
		return result;
	}
}
