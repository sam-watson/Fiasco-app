using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaysetManager : MonoBehaviour {

	public List<Playset> playsets;
	
	void Start() {
		LoadPlaysetsFromFile();
	}
	
	public void LoadPlaysetsFromFile() {
		var js = new JSON();
		var txt = (TextAsset)Resources.Load("playsets");
		js.serialized = txt.text;
		playsets = new List<Playset>(Playset.Array(js.ToArray<JSON>("playsets")));
	}
	
}
