using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour {
	//firstがフィールドの幅,secondが機雷の数
	private List<Pair> Difficulty{
		get{
			return new List<Pair> () {
				new Pair (5, 4),
				new Pair (7, 12),
				new Pair (10,25),
			};
		}
	}
	public void go_select(){
		SceneManager.LoadScene("start");
	}

	public void go_stage(int i = 0){
		//restartの場合は数値を読み込ませない
		if(i != 0){
			GameInfo.field_width = Difficulty[i-1].first;
			GameInfo.bomb_count = Difficulty[i-1].second;
		}
		GameInfo.game_stop = false;
		GameInfo.first_open = false;
		SceneManager.LoadScene("stage");
	}
}
