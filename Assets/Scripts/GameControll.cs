using UnityEngine;
using System.Collections;

public class GameControll : MonoBehaviour {

    void Start() {
        Debug.Log("Game Started.");
        Application.LoadLevel("Floor");
    }
}
