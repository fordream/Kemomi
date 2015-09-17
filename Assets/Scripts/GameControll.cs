using UnityEngine;
using System.Collections;

public class GameControll : MonoBehaviour {

// Use this for initialization

    private static bool created = false;

    void Awake() {

    }

    void Start() {

        Debug.Log(created ? "true" : "false");

        if (!created) {
            Debug.Log("test");
            Application.LoadLevel("Floor");
            created = true;
        }
    }

    // Update is called once per frame
    void Update () {


    }
}
