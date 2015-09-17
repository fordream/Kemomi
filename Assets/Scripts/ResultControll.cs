using UnityEngine;
using System.Collections;

// Resultシーンを制御するクラス
public class ResultControll : MonoBehaviour {

    // 確認ボタンが押下された時の処理
    // ゲーム状態を破棄し、Titleシーンへ遷移する
    public void ConfirmButton() {
        // TODO 結果表示
        Destroy(GameObject.Find("GameStatus"));
        Application.LoadLevel("Title");
    }
}
