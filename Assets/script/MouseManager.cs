using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MouseManager : MonoBehaviour {
	private GameFieldCreator _gfc;
	private GameFieldCreator Creator{
		get{
			if(_gfc == null){
				_gfc = GameObject.Find ("Manager").GetComponent<GameFieldCreator> ();
			}
			return _gfc;
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
	private float wheel_move = 0f;
	private float max_wheel_move{
		get{
			return GameInfo.field_width * 2 / 3;
		}
	}
	private bool moving = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		mouse_wheel ();
		float mouse_x_delta = Input.GetAxis("Mouse X");
		float mouse_y_delta = Input.GetAxis("Mouse Y");
		var pos_center = new Vector3 (GameInfo.field_width / 2, GameInfo.field_width / 2, GameInfo.field_width / 2);
		if(Input.GetMouseButton(0) && (Mathf.Abs(mouse_x_delta) > 0 || Mathf.Abs(mouse_y_delta) > 0 ) ) {
			Creator.Stage.transform.RotateAround(pos_center, Camera.main.transform.up, mouse_x_delta * 2);
			Creator.Stage.transform.RotateAround(pos_center, Camera.main.transform.right, mouse_y_delta * 2);
			moving = true;
		}


		// 左クリックが0、右クリックが1
		for(var i = 0; i < 2; i++){
			//クリアしている場合は開けさせない　視点移動は許可する
			if(Input.GetMouseButtonUp(i) && GameInfo.game_stop == false) {
				//視点移動していたら開けない
				if(moving == true){
					moving = false;
					return;
				}
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit = new RaycastHit();
				if (Physics.Raycast(ray, out hit)){
					if(hit.collider.gameObject.tag == "cover"){
						SEM.se_play ((int)SEManager.SENUM.CLICK);
						if (i == 0) {
							hit.collider.gameObject.GetComponent<ObjInfo> ().cover_delete ();
						} else {
							hit.collider.gameObject.GetComponent<ObjInfo> ().banner_up();
						}
					}
				}
			}
		}

	}
	void mouse_wheel(){
		var scroll = Input.GetAxis ("Mouse ScrollWheel");
		if(Mathf.Abs(wheel_move - scroll) < max_wheel_move){
			wheel_move -= scroll;
		}
		var cam_pos = Camera.main.GetComponent<CameraMine> ().default_pos;
		Camera.main.transform.position = new Vector3 ( cam_pos.x , cam_pos.y ,cam_pos.z + wheel_move );
	}
}
