﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDB : MonoBehaviour
{
	//全アイテムのリスト
	public List<Item> items = new List<Item>();
	
	void Awake()
	{
		// string name, int id, string desc, string itemIconPath
		items.Add(new Item("手裏剣", 0, "投げてダメージを与える", "shuriken"));
		items.Add(new Item("包帯", 1, "回復する", "houtai"));
		items.Add(new Item("ダッシュブーツ", 2, "足が速くなる", "dashboots"));

		
	}
}