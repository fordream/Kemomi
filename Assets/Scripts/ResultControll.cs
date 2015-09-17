using UnityEngine;
using System.Collections;

public class ResultControll : MonoBehaviour {
    public void ConfirmButton() {

        // TODO 結果表示

        Destroy(GameObject.Find("GameStatus"));
        Application.LoadLevel("Title");
    }
}
