using UnityEngine;
using System.Collections;

public class IncomeManager : MonoBehaviour {

    float incomeTimer = 0;
    float incomeCooldown = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (incomeTimer >= incomeCooldown)
        {
            incomeTimer = 0;
            foreach (string key in Room.rooms.Keys) {
                float amount = Room.rooms[key];
                float amountPerRoom = Room.roomPrefabs[key].incomeperMin;
                GameData.instance.money += (amount * amountPerRoom)/60;
                Debug.Log("updated income");
            }
        }
        else {
            incomeTimer += Time.deltaTime;
        }
	}
}
