using UnityEngine;
using System.Collections;

// HACK クソコピペクラスなのでできたら修正

//アイテムの基底クラス
public abstract class Item 
{
	public string itemName;        //名前
	public int itemID;             //アイテムID
	public string itemDesc;        //アイテムの説明文
	public Texture2D itemIcon;     //アイコン

	public abstract int MaxStack { get; } //最大スタック数
	public abstract bool IsConsumable { get; }  //消費アイテムであるかどうか
	public abstract bool IsAvailable();
	
	//itemDBで呼ぶコンストラクタ
	public Item(string name, int id, string desc, string itemIconPath){
		itemName = name;
		itemID = id;
		itemIcon = Resources.Load<Texture2D>("ItemIcons/" + itemIconPath);
		itemDesc = desc;
	}
	public Item(){
	}
}


//空アイテム
public class EmptyItem : Item {
	public override int MaxStack { get{ return 1; } }
	public override bool IsConsumable { get{ return false; } }
	public override bool IsAvailable(){
		return false;
	}
	public EmptyItem(string name, int id, string desc, string itemIconPath):base(name,id,desc,itemIconPath){
	}
	public EmptyItem(){
	}
}


// タップで選択、その後方向指定のフリックで起動するタイプのアイテムのベースクラスです
public abstract class ItemWithDirection : Item {
	// 方向を引数にした、アイテムの使用処理です、処理完了（or中断）後、使用に成功したかどうかを返します
	public abstract bool Action(float direction);
	public ItemWithDirection(string name, int id, string desc, string itemIconPath):base(name,id,desc,itemIconPath){
	}
}

// タップした瞬間、効果を発揮するタイプのアイテムのベースクラスです
public abstract class OneTouchItem : Item {
	// 方向を引数にした、アイテムの使用処理です、処理完了（or中断）後、使用に成功したかどうかを返します
	public abstract bool Effect();
	public OneTouchItem(string name, int id, string desc, string itemIconPath):base(name,id,desc,itemIconPath){
	}
}

// 手裏剣（スタックできる投擲アイテム )
public class Shuriken : ItemWithDirection {
	public override int MaxStack { get { return 20; } }
	public override bool IsConsumable { get { return true; } }
	public override bool IsAvailable() {
		// ここに、ケモミが手裏剣を撃てなさそうなモーション中だったらfalseを返す処理を追加してください
		return true;
	}
	
	public override bool Action(float direction) {
		if (!IsAvailable()) {
			return false;
		}
		
		// ここで手裏剣オブジェクトの生成、投擲処理をしてください
		Debug.Log(direction + "の方向に手裏剣を投げました");
		
		return true;
	}

	public Shuriken(string name, int id, string desc, string itemIconPath):base(name,id,desc,itemIconPath){
	}
}

// お札
public class Ofuda : ItemWithDirection {
	public override int MaxStack { get { return 1; } }
	public override bool IsConsumable { get { return true; } }
	public override bool IsAvailable() {
		// ここに、ケモミがお札を撃てなさそうなモーション中だったらfalseを返す処理を追加してください
		return true;
	}
	
	public override bool Action(float direction) {
		if (!IsAvailable()) {
			return false;
		}
		
		// ここでお札処理をしてください
		Debug.Log(direction + "の方向にお札を投げました");
		
		return true;
	}
	
	public Ofuda(string name, int id, string desc, string itemIconPath):base(name,id,desc,itemIconPath){
	}
}

// 体当たり（ケモミちゃんが突進して、当たった敵を吹き飛ばす）
public class Taiatari : ItemWithDirection {
	public override int MaxStack { get { return 1; } }
	public override bool IsConsumable { get { return false; } }
	
	public override bool IsAvailable() {
		// ここに、ケモミがスキルを発動できなさそうなモーション中だったらfalseを返す処理を追加してください
		// ここに、ケモミのSPが30未満だったらfalseを返す処理を追加してください
		
		return true;
	}
	
	public override bool Action(float direction) {
		if (!IsAvailable()) {
			return false;
		}
		// ここでケモミちゃんに吹き飛ばし効果を持った体当たりをさせる処理をしてください
		Debug.Log(direction + "の方向に体当たりしました");
		return true;
	}
	
	public Taiatari(string name, int id, string desc, string itemIconPath):base(name,id,desc,itemIconPath){
	}
}

// 衝撃波
public class Shougekiha : ItemWithDirection {
	public override int MaxStack { get { return 1; } }
	public override bool IsConsumable { get { return false; } }
	
	public override bool IsAvailable() {
		// ここに、ケモミがスキルを発動できなさそうなモーション中だったらfalseを返す処理を追加してください
		// ここに、ケモミのSPが30未満だったらfalseを返す処理を追加してください
		
		return true;
	}
	
	public override bool Action(float direction) {
		if (!IsAvailable()) {
			return false;
		}
		// ここでケモミちゃんに衝撃波処理をしてください
		Debug.Log(direction + "の方向に衝撃波しました");
		return true;
	}
	
	public Shougekiha(string name, int id, string desc, string itemIconPath):base(name,id,desc,itemIconPath){
	}
}



// HP回復薬
public class HPPortion : OneTouchItem {
	public override int MaxStack { get { return 1; } }
	public override bool IsConsumable { get { return true; } }

	public override bool IsAvailable() {
		// ここに、ケモミがアイテムを使えなさそうなモーション中だったらfalseを返す処理を追加してください
		// return false;
		return true;
	}
	
	public override bool Effect() {
		if (!IsAvailable()) {
			return false;
		}
		Debug.Log("HP薬を飲みました");
		return true;
	}

	public HPPortion(string name, int id, string desc, string itemIconPath):base(name,id,desc,itemIconPath){
	}
}

// SP無限薬
public class SPMugenPortion : OneTouchItem {
	public override int MaxStack { get { return 1; } }
	public override bool IsConsumable { get { return true; } }
	
	public override bool IsAvailable() {
		// ここに、ケモミがアイテムを使えなさそうなモーション中だったらfalseを返す処理を追加してください
		// return false;
		return true;
	}
	
	public override bool Effect() {
		if (!IsAvailable()) {
			return false;
		}
		Debug.Log("SP無限薬を飲みました");
		return true;
	}
	
	public SPMugenPortion(string name, int id, string desc, string itemIconPath):base(name,id,desc,itemIconPath){
	}
}

// 復活草
public class FukkatsuSou : OneTouchItem {
	public override int MaxStack { get { return 1; } }
	public override bool IsConsumable { get { return true; } }
	
	public override bool IsAvailable() {
		// ここに、ケモミがアイテムを使えなさそうなモーション中だったらfalseを返す処理を追加してください
		// return false;
		return true;
	}
	
	public override bool Effect() {
		if (!IsAvailable()) {
			return false;
		}
		Debug.Log("復活草を飲みました");
		return true;
	}
	
	public FukkatsuSou(string name, int id, string desc, string itemIconPath):base(name,id,desc,itemIconPath){
	}
}

//ランダムマップ移動
public class RandomMapIdou : OneTouchItem {
	public override int MaxStack { get { return 1; } }
	public override bool IsConsumable { get { return true; } }
	
	public override bool IsAvailable() {
		// ここに、ケモミがアイテムを使えなさそうなモーション中だったらfalseを返す処理を追加してください
		// return false;
		return true;
	}
	
	public override bool Effect() {
		if (!IsAvailable()) {
			return false;
		}
		Debug.Log("復活草を飲みました");
		return true;
	}
	
	public RandomMapIdou(string name, int id, string desc, string itemIconPath):base(name,id,desc,itemIconPath){
	}
}

//打ち上げ花火
public class UchiageHanabi : OneTouchItem {
	public override int MaxStack { get { return 1; } }
	public override bool IsConsumable { get { return true; } }
	
	public override bool IsAvailable() {
		// ここに、ケモミがアイテムを使えなさそうなモーション中だったらfalseを返す処理を追加してください
		// return false;
		return true;
	}
	
	public override bool Effect() {
		if (!IsAvailable()) {
			return false;
		}
		Debug.Log("打ち上げ花火を飲みました");
		return true;
	}
	
	public UchiageHanabi(string name, int id, string desc, string itemIconPath):base(name,id,desc,itemIconPath){
	}
}

//回転斬り
public class Kaitengiri : OneTouchItem {
	public override int MaxStack { get { return 1; } }
	public override bool IsConsumable { get { return false; } }
	
	public override bool IsAvailable() {
		// ここに、ケモミがアイテムを使えなさそうなモーション中だったらfalseを返す処理を追加してください
		// return false;
		return true;
	}
	
	public override bool Effect() {
		if (!IsAvailable()) {
			return false;
		}
		Debug.Log("打ち上げ花火を飲みました");
		return true;
	}
	
	public Kaitengiri(string name, int id, string desc, string itemIconPath):base(name,id,desc,itemIconPath){
	}
}
