using UnityEngine;
using System.Collections;
using System;

public enum Type { Dirt, Grass, Sky, Lobby, ElementryClassRoom, Foundation, Structure, ElementryArtRoom, ElementryComputerLab, ElementryGym }

public class Tile {

    public int x;
    public int y;
    private int index = 0;
    public int Index {
        get {
            return index;
        }

        set {
            index = value;
            if (onTileChanged != null)
                onTileChanged(this);
        }
    }
    public World world;
    Action<Tile> onTileChanged;
    public int roomX;
    public int roomY;

    private Type type = Type.Sky;
    public Type Type {
        get {
            return type;
        }
        set {
            type = value;

            if (onTileChanged != null)
                onTileChanged(this);
        }
    }

    public Tile(int x, int y, World world) {
        this.x = x;
        this.y = y;
        this.world = world;
    }



    public void RegisterOnTileChanged(Action<Tile> cb) {
        onTileChanged += cb;
    }

    public void UnregisterOnTileChanged(Action<Tile> cb) {
        onTileChanged -= cb;
    }

}
