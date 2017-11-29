using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjInfo : MonoBehaviour {
	//自分の座標を持つ
	public int x;
	public int y;
	public int z;

	private GameObject manager{
		get{
			return GameObject.Find ("Manager");
		}
	}

	private Material BannerMat{
		get{
			return (Material)Resources.Load("Materials/flag6");
		}
	}
	private Material QuestionMat{
		get{
			return (Material)Resources.Load("Materials/question6");
		}
	}
	private Material DefaultMat{
		get{
			return (Material)Resources.Load("Materials/default");
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void init(int _x, int _y, int _z){
		x = _x;
		y = _y;
		z = _z;
	}
	public void cover_delete(){
		// 旗が立てられていた場合は開けない
		if(Mine.field[x][y][z].second == (int)StateEnum.SecondState.FLAGED){
			return;
		}
		if (GameInfo.first_open == false) {
			//ここ　つまり最初の開封後に数字と爆弾を生成する
			manager.GetComponent<MineFieldManager> ().rand_bomb_set (x,y,z);
			manager.GetComponent<GameFieldCreator>().create_field_to_game ();
			GameInfo.first_open = true;
		}
		manager.GetComponent<MineFieldManager> ().open(x,y,z);
	}

	public void banner_up(){
		//開いてなかったら旗を立てる　旗が立っていれば？マーク　？マークからは何もない状態に戻す
		if(Mine.field[x][y][z].second == (int)StateEnum.SecondState.NOT_OPEND){
			Mine.field [x] [y] [z].second = (int)StateEnum.SecondState.FLAGED;
			manager.GetComponent<MineFieldManager> ().flag_points.Add (new Triple (x, y, z));
			manager.GetComponent<MineFieldManager> ().check_clear ();
			GetComponent<MeshRenderer> ().material = BannerMat;
		}else if(Mine.field[x][y][z].second == (int)StateEnum.SecondState.FLAGED){
			Mine.field [x] [y] [z].second = (int)StateEnum.SecondState.QUESTION;
			manager.GetComponent<MineFieldManager> ().flag_points.Remove(new Triple (x, y, z));
			GetComponent<MeshRenderer> ().material = QuestionMat;
		}else if(Mine.field[x][y][z].second == (int)StateEnum.SecondState.QUESTION){
			Mine.field [x] [y] [z].second = (int)StateEnum.SecondState.NOT_OPEND;
			GetComponent<MeshRenderer> ().material = DefaultMat;
		}

	}
}
