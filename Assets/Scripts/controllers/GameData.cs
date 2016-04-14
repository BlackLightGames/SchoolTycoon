using UnityEngine;
using System.Collections;

public class GameData : MonoBehaviour {

    public static GameData instance;
    public float money;

	// Use this for initialization
	void Start () {

        if (instance != null) {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        money = 1000;
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("`") && UnityEngine.Debug.isDebugBuild)
        {
            money += 1000;
        }
    }
}
