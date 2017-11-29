using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameInfo.time_start ();
		time_text ();
	}
	
	// Update is called once per frame
	void Update () {
		time_text ();
	}

	void time_text(){
		GetComponent<Text> ().text = "Time : " + GameInfo.game_time.ToString ("N2");
	}
}
