using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFieldCreator : MonoBehaviour {
	//GameObjectやGetComponentが絡む場合、startでなく、呼ばれた時点で初期化する記述にしてる
	//startだと他のスクリプトのstartから参照する際に順序の問題でnullが入ることがある
	//privateのプロパティでもこのような記述にしている　呼ばれなければそれはそれでいいので
	private GameObject _text_org;
	private GameObject TextOrg{
		get{
			if(_text_org == null){
				_text_org = (GameObject)Resources.Load("prefab/text");
			}
			return _text_org;
		}
	}
	private GameObject _mine_org;
	private GameObject MineOrg{
		get{
			if(_mine_org == null){
				_mine_org = (GameObject)Resources.Load("prefab/mine_cube");
			}
			return _mine_org;
		}
	}

	private GameObject _cover_org;
	private GameObject CoverOrg{
		get{
			if(_cover_org== null){
				_cover_org = (GameObject)Resources.Load("prefab/cover");
			}
			return _cover_org;
		}
	}

	private MineFieldManager _mine_manager;
	private MineFieldManager MineManager{
		get{
			if(_mine_manager == null){
				_mine_manager = GetComponent<MineFieldManager> ();
			}
			return _mine_manager;
		}
	}

	public static GameObject[,,] cover_objs;
	public static GameObject[,,] info_objs;

	private GameObject _stage;
	public GameObject Stage{
		get{
			if(_stage == null){
				_stage = new GameObject ();
				_stage.name = "stage";
			}
			return _stage;
		}
	}
	// Use this for initialization
	void Start () {
		init ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void init(){
		cover_objs = new GameObject[GameInfo.field_width, GameInfo.field_width, GameInfo.field_width];
		info_objs = new GameObject[GameInfo.field_width, GameInfo.field_width, GameInfo.field_width];
		create_covers ();
		//NewGameの際 first_openにfalseが入っていないことがあったので一応ここでもfalseにしておく
		GameInfo.first_open = false;
	}
	public void create_field_to_game(){
		var info = new GameObject ();
		info.name = "info";
		info.transform.SetParent (Stage.transform);
		for(var x = 0; x < GameInfo.field_width; x++){
			for(var y = 0; y < GameInfo.field_width; y++){
				for(var z = 0; z < GameInfo.field_width; z++){
					create_obj (info.transform, x, y, z);
				}
			}
		}
		// ここで位置を修正してもうまく動かないので、0.001秒後に修正
		StartCoroutine (info_pos_fix(info.transform));
	}
	IEnumerator info_pos_fix(Transform info){
		yield return new WaitForSeconds(0.001f);
		info.localPosition = Vector3.zero;
		info.localEulerAngles = Vector3.zero;
	}
	void create_obj(Transform trans,int x, int y, int z){
		var pos = new Vector3 (x ,y , z);
		var info_cover = new GameObject ();
		info_cover.name = "X=" + x + "_Y=" + y + "_Z=" + z;
		info_cover.transform.SetParent (trans.transform);
		if(Mine.field[x][y][z].first == MineManager.BOMB){
			var obj = (GameObject)Instantiate (MineOrg,  MineOrg.transform.position + pos, MineOrg.transform.rotation);
			obj.name = "mine";
			obj.transform.SetParent (info_cover.transform);
			info_objs [x,y,z] = info_cover;
		}else{
			var obj = (GameObject)Instantiate (TextOrg, TextOrg.transform.position + pos, TextOrg.transform.rotation);
			obj.GetComponent<TextMesh> ().text = Mine.field[x][y][z].first.ToString();
			obj.name = "text";
			obj.transform.SetParent (info_cover.transform);
			info_objs [x,y,z] = info_cover;
			//文字はCubeを透過するためスケールを0にしておく
			info_cover.transform.localScale = Vector3.zero;
		}

	}

	void create_covers(){
		var cover_parent = new GameObject ();
		cover_parent.name = "covers";
		cover_parent.transform.SetParent (Stage.transform);
		for(var x = 0; x < GameInfo.field_width; x++){
			for(var y = 0; y < GameInfo.field_width; y++){
				for(var z = 0; z < GameInfo.field_width; z++){
					var pos = new Vector3 (x, y, z);
					var obj = (GameObject)Instantiate (CoverOrg, pos, CoverOrg.transform.rotation);
					obj.name = "COVER_X=" + x + "_Y=" + y + "_Z=" + z;
					obj.transform.SetParent (cover_parent.transform);
					obj.GetComponent<ObjInfo> ().init (x,y,z);
					cover_objs [x,y,z] = obj;
				}
			}
		}
	}
}
