using UnityEngine;
using System.Collections.Generic;

public class Room {

    public static Dictionary<string, int> rooms = new Dictionary<string, int>();

    public static Dictionary<string, Room> roomPrefabs = new Dictionary<string, Room>();

    public int width;
    public int height;
    public Type tileType;
    public int buildTime;
    public int cost;
    public int incomeperMin;
    public string name;

    public Room(string name, int width, int height, Type tileType, int cost, int incomeperMin, int buildTime = 1) {
        this.name = name;
        this.width = width;
        this.height = height;
        this.tileType = tileType;
        this.buildTime = buildTime;
        this.cost = cost;
        this.incomeperMin = incomeperMin;
    }

    public static void setupRoomPrefabs() {
        roomPrefabs.Add("ElementryClassRoom",new Room("ElementryClassRoom", 3, 1, Type.ElementryClassRoom, 100, 20));
    }

}
