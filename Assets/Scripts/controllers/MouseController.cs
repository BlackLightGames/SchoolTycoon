using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour {

    Vector3 positionLastFrame;
    Room selectedRoom;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
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
        if (Input.GetMouseButtonUp(0)) {
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int index = 0;
            for (int y = 0; y < selectedRoom.height; y++) {
                for (int x = 0; x < selectedRoom.width; x++) {
                    Debug.Log("Tile " + x + " " + y);
                    Tile t = WorldController.instance.world.getTileAt((int)(Mathf.FloorToInt(currentPosition.x+.5f) + x), (int)(Mathf.FloorToInt(currentPosition.y+.5f) - y));
                    t.Type = selectedRoom.tileType;
                    t.Index = index;
                    index++;
                }
            }
        }
	}

    public void selectRoom(string roomName) {
        selectedRoom = Room.roomPrefabs[roomName];
    }
}
