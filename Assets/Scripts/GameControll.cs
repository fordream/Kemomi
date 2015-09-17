using UnityEngine;
using System.Collections;

// Gameシーンを制御するクラス
public class GameControll : MonoBehaviour {

    // ゲームオブジェクトを初期化する
    void Start() {
        // 共有するゲームオブジェクトを発行した後、最初のFloorシーンに遷移する
        Debug.Log("Game Started.");
        Application.LoadLevel("Floor");
    }
}
