using UnityEngine;
using System.Collections;

// ゲームの状態を管理するクラス
// Gameシーンで生成され、全てのFloorシーンで共有される
public class GameStatus : MonoBehaviour {

    // 現在の階数
    public int FloorLevel { get; set; };

    // プレイ時間
    public int PlayTime { get; set; };

    // ゲームオーバーフラグ
    public bool GameOver { get; set; };

    // ゲームオブジェクトを初期化する
    void Start() {
        // 複数シーンでゲームオブジェクトを共有するための設定
        DontDestroyOnLoad(this);

        // ゲームの初期状態を設定する
        FloorLevel = 1;
        PlayTime = 1;
        GameOver = false;

    }

}
