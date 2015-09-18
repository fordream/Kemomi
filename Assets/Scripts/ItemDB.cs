using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDB : MonoBehaviour{
	//全アイテムのリスト
	public List<Item> items = new List<Item>();

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
}