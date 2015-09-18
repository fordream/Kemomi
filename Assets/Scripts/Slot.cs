using UnityEngine;
using System.Collections;

public class Slot {
	public Item item;
	public int itemCount;
	public Rect slotRect;

	public Slot(){
		item = new EmptyItem ();
		itemCount = 0;
	}
}
