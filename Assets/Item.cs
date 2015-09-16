using UnityEngine;
using System.Collections;


//別のスクリプトで
//private ItemDataBase database;
//参照して
//database.items[i].itemName
//みたいにとってくる

[System.Serializable]
public class Item 
{
	public string itemName;        //名前
	public int itemID;             //アイテムID
	public string itemDesc;        //アイテムの説明文
	public Texture2D itemIcon;     //アイコン

 
	public Item(string name, int id, string desc, string itemIconPath)
	{
		itemName = name;
		itemID = id;
		itemIcon = Resources.Load<Texture2D>("ItemIcons/" + itemIconPath);
		itemDesc = desc;
	}

}