using UnityEngine;
using System.Collections.Generic;

public class Room {

    public static Dictionary<string, Room> roomPrefabs = new Dictionary<string, Room>();

    public int width;
    public int height;
    public Type tileType;
    public int buildTime;

    public Room(int width, int height, Type tileType, int buildTime = 1) {
        this.width = width;
        this.height = height;
        this.tileType = tileType;
        this.buildTime = buildTime;
    }

    public static void setupRoomPrefabs() {
        roomPrefabs.Add("ElementryClassRoom",new Room(3, 1, Type.ElementryClassRoom));
    }

}
