using UnityEngine;
using System.Collections;

public class GameStatus : MonoBehaviour {

    public int FloorLevel { get; set; }

    public int PlayTime { get; set; }

    public bool GameOver { get; set; }

    void Awake() {
        DontDestroyOnLoad(this);
        Initialize();
    }

    public void Initialize() {
        FloorLevel = 1;
        PlayTime = 0;
    }
}
