using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSaikoro : MonoBehaviour {
	//サイコロのようにテクスチャを貼るためのスクリプト　
	//そのままテクスチャを貼ると、横の１が上下反転してしまうため、これを用いる
	Vector2[] uv = new Vector2[24];

	public void mesh_apply(){
		MeshFilter mf = gameObject.GetComponent<MeshFilter>();
		mf.mesh.uv = uv;
	}


	// Use this for initialization
	void Start()
	{
		uv[1].x = 0.0f;        uv[1].y = 0.0f;
		uv[0].x = 1.0f / 6.0f; uv[0].y = 0.0f;
		uv[3].x = 0.0f;        uv[3].y = 1.0f;
		uv[2].x = 1.0f / 6.0f; uv[2].y = 1.0f;

		uv[6].x = 1.0f / 6.0f; uv[6].y = 0.0f;
		uv[7].x = 2.0f / 6.0f; uv[7].y = 0.0f;
		uv[10].x = 1.0f / 6.0f; uv[10].y = 1.0f;
		uv[11].x = 2.0f / 6.0f; uv[11].y = 1.0f;

		uv[20].x = 2.0f / 6.0f; uv[20].y = 0.0f;
		uv[23].x = 3.0f / 6.0f; uv[23].y = 0.0f;
		uv[21].x = 2.0f / 6.0f; uv[21].y = 1.0f;
		uv[22].x = 3.0f / 6.0f; uv[22].y = 1.0f;

		uv[16].x = 3.0f / 6.0f; uv[16].y = 0.0f;
		uv[19].x = 4.0f / 6.0f; uv[19].y = 0.0f;
		uv[17].x = 3.0f / 6.0f; uv[17].y = 1.0f;
		uv[18].x = 4.0f / 6.0f; uv[18].y = 1.0f;

		uv[15].x = 4.0f / 6.0f; uv[15].y = 0.0f;
		uv[14].x = 5.0f / 6.0f; uv[14].y = 0.0f;
		uv[12].x = 4.0f / 6.0f; uv[12].y = 1.0f;
		uv[13].x = 5.0f / 6.0f; uv[13].y = 1.0f;

		uv[4].x  = 5.0f / 6.0f; uv[4].y  = 0.0f;
		uv[5].x  = 6.0f / 6.0f; uv[5].y  = 0.0f;
		uv[8].x  = 5.0f / 6.0f; uv[8].y  = 1.0f;
		uv[9].x  = 6.0f / 6.0f; uv[9].y  = 1.0f;

		mesh_apply ();
	}

	// Update is called once per frame
	void Update()
	{

	}
}
