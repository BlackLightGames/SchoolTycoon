using UnityEngine;
using System.Collections;

public class WorldController : MonoBehaviour {

    public World world;
    public static WorldController instance;

	// Use this for initialization
	void Start () {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Room.setupRoomPrefabs();
        instance = this;
        world = new World();
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
