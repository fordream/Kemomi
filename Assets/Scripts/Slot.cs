using UnityEngine;
using System.Collections;

public class Slot {
	public Item item;
	public int itemCount;
	public Rect slotRect;
	
	public Slot(int inventoryIndex, float SCREENSCALE){
		item = new EmptyItem ();
		itemCount = 1;
		slotRect = new Rect(Screen.width - SCREENSCALE*32 - SCREENSCALE*42 * inventoryIndex, Screen.height - SCREENSCALE*32, SCREENSCALE*32, SCREENSCALE*32);
	}
}
