using UnityEngine;
using System.Collections;

public class World {

    Tile[,] tiles;
    public int width, height;

    public World(int width = 100, int height = 100){
        this.width = width;
        this.height = height;
        this.tiles = new Tile[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x, y] = new Tile(x, y, this);
            }
        }
    }

    public Tile getTileAt(int x, int y) {
        if (x < 0 || x >= width || y < 0 || y >= height) {
            Debug.LogError("tile (" + x + "," + y + ") is out of bounds.");
            return null;
        }
        return tiles[x, y];

    }

    public void RandomizeWorld()
    {
        foreach (Tile tile in tiles) {
            Type rand = GetRandomEnum<Type>();
            tile.Type = rand;
        }
    }

    static T GetRandomEnum<T>()
    {
        System.Array A = System.Enum.GetValues(typeof(T));
        T V = (T)A.GetValue(UnityEngine.Random.Range(0, A.Length));
        return V;
    }

    public void newWorld() {
        foreach (Tile tile in tiles)
        {
            Type type = Type.Sky;
            tile.Index = 0;
            if (tile.y < 3)
            {
                type = Type.Dirt;
            }
            else if (tile.y == 3)
            {
                type = Type.Grass;
            }
            tile.Type = type;
        }
    }

}
