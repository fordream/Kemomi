using UnityEngine;
using System.Collections;

// Floorシーンを制御するクラス
public class FloorControll : MonoBehaviour {

    // ゲーム状態
    private GameStatus gameStatus;

    // ゲームオブジェクトを初期化する
    void Start() {
        Debug.Log("FloorStarted. FloorLevel: " + gameStatus.FloorLevel);

        // ゲームオブジェクトからゲーム状態クラスのインスタンス
        gameStatus = GameObject.Find("GameStatus").GetComponent<GameStatus>();

        // TODO マップを生成する
        // TODO 敵ユニットを配置する
        // TODO アイテムを配置する
        // TODO 階段を設置する
    }

    // フレーム毎の更新処理
    void Update() {

        // ゲームオーバーになったらResultシーンへ遷移する
        if (gameStatus.GameOver) {
            Application.LoadLevel("Result");
        }

        // TODO 部屋に入った時の判定、処理

    }


    // ここから下はデバッグ用

    public void GameOverButton() {
        gameStatus.GameOver = true;
    }

    public void NextFloorButton() {
        gameStatus.FloorLevel++;

        Application.LoadLevel("Floor");
    }
}
