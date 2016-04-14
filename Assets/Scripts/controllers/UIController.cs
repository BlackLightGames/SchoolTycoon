using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    Text moneyText;

	// Use this for initialization
	void Start () {
        moneyText = transform.FindChild("Money").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        moneyText.text = "$" + GameData.instance.money;
	}
}
