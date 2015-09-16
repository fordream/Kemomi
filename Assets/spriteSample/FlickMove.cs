using UnityEngine;
using System.Collections;

public class FlickMove : MonoBehaviour {
	private Vector3 prevMousePos;

	// Use this for initialization
	void Start () {
		prevMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition );
		Debug.Log (mousePos);
		if ( Input.GetMouseButton(0) ) {
			// マウスの移動分だけ速度与える
			gameObject.GetComponent<Rigidbody2D>().velocity = (mousePos - prevMousePos) / Time.deltaTime;
		}
		
		prevMousePos = mousePos;
	}

}
