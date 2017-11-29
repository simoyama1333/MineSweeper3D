using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour {
	public enum SENUM : int{
		CLICK = 0, CLEAR, EXPLODE
	}
	private AudioSource[] _sources;
	private AudioSource[] AudioSources{
		get{
			if(_sources == null){
				_sources = GetComponents<AudioSource>();
			}
			return _sources;
		}

	}
	//enum SENUMを入れる
	public void se_play(int num){
		AudioSources[num].PlayOneShot(AudioSources[num].clip);
	}
}
