using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour {
	public static bool first_open = false;
	public static float game_time = 0f;
	public static bool time_counting = false;
	public static int field_width = 0;
	public static int bomb_count = 0;
	public static bool game_stop = false;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this);
	}
	
	// Update is called once per frame
	void Update () {
		if(time_counting){
			game_time += Time.deltaTime;
		}
	}

	public static void time_start(){
		game_time = 0f;
		time_counting = true;
	}

	public static void game_end(){
		time_counting = false;
	}
	public static void game_clear(){
		game_stop = true;
	}
}
