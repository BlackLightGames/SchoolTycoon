using UnityEngine;
using System.Collections.Generic;

public class SpriteController : MonoBehaviour {

    Dictionary<Tile, GameObject> gameObjects = new Dictionary<Tile, GameObject>();
    Dictionary<string, Sprite> tileSprites = new Dictionary<string, Sprite>();
    bool initFinished = false;

	// Use this for initialization
	void Start () {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Tiles/");
        foreach (Sprite sprite in sprites) {
            tileSprites.Add(sprite.name, sprite);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!initFinished && WorldController.instance != null)
        {
            initFinished = true;
            for (int x = 0; x < WorldController.instance.world.width; x++)
            {
                for (int y = 0; y < WorldController.instance.world.height; y++)
                {
                    GameObject tile_go = new GameObject();
                    tile_go.name = "Tile_" + x + "_" + y;
                    tile_go.transform.position = new Vector3(x, y, 0);
                    tile_go.transform.SetParent(this.transform, true);
                    tile_go.AddComponent<SpriteRenderer>();
                    WorldController.instance.world.getTileAt(x, y).RegisterOnTileChanged(onTileChanged);
                    gameObjects.Add(WorldController.instance.world.getTileAt(x, y), tile_go);
                }
            }
            WorldController.instance.world.newWorld();
        }
	}

    void onTileChanged(Tile tile) {
        GameObject tile_go = gameObjects[tile];
        SpriteRenderer sr = tile_go.GetComponent<SpriteRenderer>();
        if (tileSprites.ContainsKey(tile.Type.ToString() + "_" + tile.Index))
        {
            sr.sprite = tileSprites[tile.Type.ToString() + "_" + tile.Index];
        }
        else {
            sr.sprite = tileSprites["Missing"];
        }
    }
}
