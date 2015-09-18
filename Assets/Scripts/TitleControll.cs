using UnityEngine;
using System.Collections;

// Titleシーンを制御するクラス
public class TitleControll : MonoBehaviour {

    // ゲームスタートボタンを押した時の処理
    public void GameStartButton() {
        // Gameシーンへ遷移する
        Application.LoadLevel("Game");
    }
}
