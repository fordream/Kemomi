using UnityEngine;
using System.Collections;

// Floorシーンを制御するクラス
public class FloorControll : MonoBehaviour {

    // ゲーム状態
    private GameStatus gameStatus;

    // ゲームオブジェクトを初期化する
    void Start() {

        // ゲームオブジェクトからゲーム状態クラスのインスタンスを取得する
        gameStatus = GameObject.Find("GameStatus").GetComponent<GameStatus>();

        Debug.Log("FloorStarted. FloorLevel: " + gameStatus.FloorLevel);

        generateMap();
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

    // マップ生成
    private void generateMap() {
        //var mapGenerator = new MapGenerator();
        var map = MapUtility.MapGenerator.Generate();

        for (int x = 0; x < map.GetLength(0); x++) {
            for (int y = 0; y < map.GetLength(1); y++) {
                if (map[x, y] == MapUtility.MapGenerator.CHIP_WALL) {
                    var prefab = (GameObject)Resources.Load("Prefabs/Wall");
                    var scriptRenderer = prefab.GetComponent<SpriteRenderer>();
                    var wallWidth = scriptRenderer.bounds.size.x * 0.8f;
                    var wallHeight = scriptRenderer.bounds.size.y * 0.8f;

                    Instantiate(prefab, new Vector3(wallWidth * x, wallHeight * y, 0), Quaternion.identity);
                }

                if (map[x, y] == 3) {
                    var prefab = (GameObject)Resources.Load("Prefabs/Room");
                    var scriptRenderer = prefab.GetComponent<SpriteRenderer>();
                    var wallWidth = scriptRenderer.bounds.size.x * 0.8f;
                    var wallHeight = scriptRenderer.bounds.size.y * 0.8f;

                    Instantiate(prefab, new Vector3(wallWidth * x, wallHeight * y, 0), Quaternion.identity);
                }
            }
        }
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
