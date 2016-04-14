using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MouseController : MonoBehaviour {

    Vector3 positionLastFrame;
    Room selectedRoom;
    int mode = 0; // 0 = none, 1 = room, 2 = tile, 3 = destruction
    Type tileType = Type.Foundation;
    int SelectedIndex = 0;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Cancel")) {
            deselect();
        }
        if (Input.GetMouseButtonDown(2) || Input.GetMouseButtonDown(1))
        {
            positionLastFrame = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }
        if (Input.GetMouseButton(2) || Input.GetMouseButton(1)) {
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.Translate(new Vector3(positionLastFrame.x - currentPosition.x, positionLastFrame.y - currentPosition.y, 0));
            positionLastFrame = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize-Input.mouseScrollDelta.y,1, 10);
        if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject()) {
            if (mode == 1)
            {
                Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                bool canPlace = true;
                for (int y = 0; y < selectedRoom.height; y++)
                {
                    for (int x = 0; x < selectedRoom.width; x++)
                    {
                        Tile t = WorldController.instance.world.getTileAt((int)(Mathf.FloorToInt(currentPosition.x + .5f) + x), (int)(Mathf.FloorToInt(currentPosition.y + .5f) - y));
                        if (t.Type != Type.Structure)
                        {
                            canPlace = false;
                            break;
                        }
                    }
                }
                if (canPlace)
                {
                    int index = 0;
                    for (int y = 0; y < selectedRoom.height; y++)
                    {
                        for (int x = 0; x < selectedRoom.width; x++)
                        {
                            Debug.Log("Tile " + x + " " + y);
                            Tile t = WorldController.instance.world.getTileAt((int)(Mathf.FloorToInt(currentPosition.x + .5f) + x), (int)(Mathf.FloorToInt(currentPosition.y + .5f) - y));
                            t.roomX = Mathf.FloorToInt(currentPosition.x + .5f);
                            t.roomY = Mathf.FloorToInt(currentPosition.y + .5f);
                            t.Type = selectedRoom.tileType;
                            t.Index = index;
                            index++;
                        }
                    }
                }
            }
            else if (mode == 2)
            {
                Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                bool canPlace = false;
                Tile t = WorldController.instance.world.getTileAt((int)(Mathf.FloorToInt(currentPosition.x + .5f)), (int)(Mathf.FloorToInt(currentPosition.y + .5f)));
                if (tileType == Type.Foundation && t.Type == Type.Grass)
                {
                    canPlace = true;
                }
                else if (tileType == Type.Structure)
                {
                    Tile tBellow = WorldController.instance.world.getTileAt((int)(Mathf.FloorToInt(currentPosition.x + .5f)), (int)(Mathf.FloorToInt(currentPosition.y + .5f) - 1));
                    if (tBellow != null && (tBellow.Type != Type.Grass && tBellow.Type != Type.Dirt && tBellow.Type != Type.Sky) && t.Type == Type.Sky)
                    {
                        canPlace = true;
                    }
                }
                if (canPlace)
                {
                    t.Type = tileType;
                    t.Index = SelectedIndex;
                }
            }
            else if (mode == 3) {
                Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Tile t = WorldController.instance.world.getTileAt((int)(Mathf.FloorToInt(currentPosition.x + .5f)), (int)(Mathf.FloorToInt(currentPosition.y + .5f)));
                Tile tAbove = WorldController.instance.world.getTileAt((int)(Mathf.FloorToInt(currentPosition.x + .5f)), (int)(Mathf.FloorToInt(currentPosition.y + .5f) + 1));
                if (t.Type == Type.Foundation)
                {
                    if (tAbove.Type == Type.Sky)
                    {
                        t.Type = Type.Grass;
                        t.Index = 0;
                    }
                }
                else if (t.Type == Type.Structure)
                {
                    if (tAbove.Type == Type.Sky)
                    {
                        t.Type = Type.Sky;
                        t.Index = 0;
                    }
                }
                else if (t.Type != Type.Dirt && t.Type != Type.Grass && t.Type != Type.Sky) {
                    bool isRoom = false;
                    Room room = null;
                    foreach(Room r in Room.roomPrefabs.Values){
                        if (r.tileType == t.Type) {
                            isRoom = true;
                            room = r;
                            break;
                        }
                    }
                    if (isRoom)
                    {
                        int startX = t.roomX;
                        int startY = t.roomY;
                        for (int y = 0; y < room.height; y++)
                        {
                            for (int x = 0; x < room.width; x++)
                            {
                                Tile tile = WorldController.instance.world.getTileAt(startX + x, startY - y);
                                tile.Type = Type.Structure;
                                tile.Index = 0;
                                tile.roomX = 0;
                                tile.roomY = 0;
                            }
                        }
                    }
                    else {
                        t.Type = Type.Structure;
                        t.Index = 0;
                    }
                }
            }
        }
	}

    public void selectRoom(string roomName) {
        mode = 1;
        selectedRoom = Room.roomPrefabs[roomName];
    }

    public void selectTile(string tileType) {
        mode = 2;
        this.tileType = (Type)System.Enum.Parse(typeof(Type), tileType);
        SelectedIndex = 0;
    }

    public void deselect() {
        mode = 0;
    }

    public void demolish() {
        mode = 3;
    }
}
