using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class MineFieldManager : MonoBehaviour {
	/*マインスイーパーのフィールドを作るクラス
	 * GameInfoのfield_widthとbomb_countを参照して、フィールドを作る
	 */ 
	// bombは-1
	public int BOMB{
		get{
			return -1;
		}
	}
	private Mine mine;

	private GameFieldCreator _gfc;
	private GameFieldCreator Creator{
		get{
			if(_gfc == null){
				_gfc = GetComponent<GameFieldCreator> ();
			}
			return _gfc;
		}
	}
	private GameObject _ui_buttons;
	private GameObject UIButtons{
		get{
			if(_ui_buttons == null){
				_ui_buttons = GameObject.Find ("buttons");
			}
			return _ui_buttons;
		}
	}
	private GameObject _clear;
	private GameObject Clear{
		get{
			if(_clear == null){
				_clear = GameObject.Find ("clear");
			}
			return _clear;
		}
	}

	private SEManager _sem;
	private SEManager SEM{
		get{
			if(_sem == null){
				_sem = GameObject.Find ("SEManager").GetComponent <SEManager>();
			}
			return _sem;
		}
	}

	public List<Triple> bomb_points;
	//フラグ管理ではなく旗の位置を収納する
	public List<Triple> flag_points;
	public List<Triple> will_open;

	void Start(){
		mine = new Mine (GameInfo.field_width,GameInfo.field_width,GameInfo.field_width);
		bomb_points = new List<Triple> (){};
		flag_points = new List<Triple> (){};
		will_open   = new List<Triple> (){};
		UIButtons.transform.localScale = Vector3.zero;
		Clear.transform.localScale = Vector3.zero;
	}
		

	public void rand_bomb_set(int x, int y, int z){
		var except_point = new Triple (x,y,z);
		bomb_points.Add (except_point);
		//初手で一個開けるため、ボムの最大数はFieldWidth^3-1
		var bomb_max = Math.Pow (GameInfo.field_width,3) - 1;
		if(bomb_max <= GameInfo.bomb_count){
			throw new Exception();
		}
		var count = 0;
		var rnd = new System.Random();
		while(count != GameInfo.bomb_count){
			//3次元ポイントの乱数で作成
			var t = new Triple (rnd.Next (0, GameInfo.field_width), rnd.Next (0, GameInfo.field_width), rnd.Next (0, GameInfo.field_width));
			//except_pointに同じ値がなければ追加、あれば再度乱数を生成する
			if (bomb_points.IndexOf (t) == BOMB) {
				bomb_set (t.first,t.second,t.third);
				bomb_points.Add (t);
				count++;
			}
		}
		// 0には最初に消したブロックの位置が入っているのでそれを削除
		bomb_points.Remove (bomb_points[0]);



	}
	// bombのある場所の周りにインクリすることでfieldに数字が入る
	void bomb_set(int x, int y, int z){
		// -1,0.+1の範囲にインクリする
		for(var range_x = -1; range_x < 2;range_x++){
			for(var range_y = -1;range_y < 2; range_y++){
				for(var range_z = -1; range_z < 2 ;range_z++){
					//indexが-1の場合 continue
					if( x + range_x == -1 || y + range_y == -1 || z + range_z == -1 ){
						continue;
					}
					//indexがarray sizeを超える場合 continue
					if( x + range_x >= GameInfo.field_width || y + range_y >= GameInfo.field_width || z + range_z >= GameInfo.field_width ){
						continue;
					}

					//rangeすべてが0の場合bombをセット
					if(range_x == 0 && range_y == 0 && range_z == 0){
						Mine.field[x][y][z].first = BOMB;
						continue;
					}
						
					//ボムがすでに設置されている場合を除いてインクリ
					if(Mine.field[x + range_x][y + range_y][z + range_z].first != BOMB){
						Mine.field[x + range_x][y + range_y][z + range_z].first++;
					}
				}
			}
		}
	}

	public bool check_clear(){
		// 爆弾の数と旗の数が一致していなければそもそもリターンさせる
		if(bomb_points.Count != flag_points.Count){
			return false;
		}
		// flagの位置情報の全てbombに存在する場合　最後のtrueを返す
		foreach(var triple in bomb_points){
			if(flag_points.IndexOf(triple) == -1){
				return false;
			}
		}
		SEM.se_play ((int)SEManager.SENUM.CLEAR);
		GameInfo.game_end ();
		GameInfo.game_clear ();
		UIButtons.transform.localScale = Vector3.one;
		Clear.transform.localScale = Vector3.one;
		return true;
	}

	//6方向は0があるかチェック。27-1は必ず開ける　0の場合は全方向を開けるため
	public void search_zero(int x, int y, int z){
		// -1,0.+1の範囲にインクリする
		for(var range_x = -1; range_x < 2;range_x++){
			for(var range_y = -1;range_y < 2; range_y++){
				for(var range_z = -1; range_z < 2 ;range_z++){
					//indexが-1の場合 continue
					if( x + range_x == -1 || y + range_y == -1 || z + range_z == -1 ){
						continue;
					}
					//indexがarray sizeを超える場合 continue
					if( x + range_x >= GameInfo.field_width || y + range_y >= GameInfo.field_width || z + range_z >= GameInfo.field_width ){
						continue;
					}

					//すでに開いている、旗が立っている場合はcontinueする
					if(Mine.field[x + range_x][y + range_y][z + range_z].second == (int)StateEnum.SecondState.OPEND ||
						Mine.field[x + range_x][y + range_y][z + range_z].second == (int)StateEnum.SecondState.FLAGED ){
						continue;
					}
					//問題なさそうなら開けるリストに入れる
					will_open.Add ( new Triple(x + range_x, y + range_y, z + range_z) );
					// 6方向は　(0.-1,0) (1,0,0)など1要素だけが0でないので、以下のような判別式で場合分けできる
					if (Mathf.Abs (range_x) > Mathf.Abs (range_y) + Mathf.Abs (range_z) ||
						Mathf.Abs (range_y) > Mathf.Abs (range_x) + Mathf.Abs (range_z) ||
						Mathf.Abs (range_z) > Mathf.Abs (range_x) + Mathf.Abs (range_y) 
					) {
						if (Mine.field [x + range_x] [y + range_y] [z + range_z].first == 0) {
							//再帰で探す　永遠に探し続けないように、0にはOPENDをつける
							Mine.field [x + range_x] [y + range_y] [z + range_z].second = (int)StateEnum.SecondState.OPEND;
							search_zero (x + range_x, y + range_y, z + range_z);
						}
					}
				}
			}
		}
	}

	public void open(int x, int y, int z){
		will_open.Add (new Triple(x,y,z));
		//開けた場所が0の場合、連鎖的に開くため、search_zeroを使う
		if (Mine.field [x] [y] [z].first == 0) {
			Mine.field [x] [y] [z].second = (int)StateEnum.SecondState.OPEND;
			search_zero (x,y,z);
		}
		list_open ();
	}

	//will openをopenする
	void list_open(){
		var open = will_open.uniq ();
		foreach(var t in will_open){
			Mine.field [t.first] [t.second] [t.third].second = (int)StateEnum.SecondState.OPEND;
			// 開けた後の文字が0以外の場合は大きさを戻す 0は表示させないので戻さない
			if(Mine.field [t.first] [t.second] [t.third].first != 0){
				GameFieldCreator.info_objs [t.first, t.second, t.third].transform.localScale = Vector3.one;
			}
			GameObject.Destroy (GameFieldCreator.cover_objs [t.first, t.second, t.third]);
			if(Mine.field [t.first] [t.second] [t.third].first == BOMB){
				game_explode ();
			}
		}
		will_open.Clear ();
	}

	void game_explode(){
		GameInfo.game_end ();
		SEM.se_play ((int)SEManager.SENUM.EXPLODE);
		bomb_open();
		//リスタート　ステージセレクトボタンの表示
		UIButtons.transform.localScale = Vector3.one;
	}
	//ミスした場合に、爆弾の場所を開示する
	void bomb_open(){
		foreach(var t in bomb_points){
			if(GameFieldCreator.cover_objs [t.first, t.second, t.third]){
				GameObject.Destroy (GameFieldCreator.cover_objs [t.first, t.second, t.third]);
			}
		}
	}
}


