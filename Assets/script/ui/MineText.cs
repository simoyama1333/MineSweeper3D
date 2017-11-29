using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Text> ().text = "x" + GameInfo.bomb_count.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
