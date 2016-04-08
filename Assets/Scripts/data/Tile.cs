using UnityEngine;
using System.Collections;
using System;

public enum Type{ Grass, Sky, Lobby, ElementryClassRoom }

public class Tile {

    public int x;
    public int y;
    public World world;
    Action<Tile> onTileChanged;

    private Type type;
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
