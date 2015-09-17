using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDB : MonoBehaviour{
	//全アイテムのリスト
	public List<Item> items = new List<Item>();

	void Awake(){
		// string name, int id, string desc, string itemIconPath
		items.Add(new Shuriken("手裏剣", 0, "", "shuriken"));
		items.Add(new HPPortion("HP回復薬", 1, "", "houtai"));
		items.Add(new Taiatari("体当たり", 2, "", "dashboots"));
		
	}
}