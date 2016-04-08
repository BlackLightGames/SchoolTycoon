using UnityEngine;
using System.Collections;

public class WorldController : MonoBehaviour {

    World world;
    public static WorldController instance;

	// Use this for initialization
	void Start () {
        instance = this;
        world = new World();
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
