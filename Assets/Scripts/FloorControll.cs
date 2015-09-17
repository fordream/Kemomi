using UnityEngine;
using System.Collections;

public class FloorControll : MonoBehaviour {

    private GameStatus gameStatus;

    // Use this for initialization
    void Start () {

        gameStatus = GameObject.Find("GameStatus").GetComponent<GameStatus>();
        Debug.Log("FloorStarted. FloorLevel: " + gameStatus.FloorLevel);

        // TODO マップを生成する
        // TODO 敵ユニットを配置する
        // TODO アイテムを配置する
        // TODO 階段を設置する
    }

    // Update is called once per frame
    void Update () {

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
