using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMine : MonoBehaviour {
	public Vector3 default_pos;
	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (GameInfo.field_width / 2f , GameInfo.field_width / 2f , GameInfo.field_width * -1.5f);
		default_pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
