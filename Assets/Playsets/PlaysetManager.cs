using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class PlaysetManager : MonoBehaviour {

	public List<Playset> playsets = new List<Playset>();
	
	void Start() {
		LoadAllPlaysets();
	}
	
	public void LoadPlaysetsFromFile() {
//		var js = new JSON();
//		var txt = (TextAsset)Resources.Load("playsets");
//		js.serialized = txt.text;
//		playsets = new List<Playset>(Playset.Array(js.ToArray<JSON>("playsets")));
	}
	
	public void LoadAllPlaysets() {
		//get list of all play-sets
		Debug.Log("Loading playsets...");
		var playsetText = (TextAsset)Resources.Load("playsets");
		var playsetData = JsonMapper.ToObject(playsetText.text);
		for (int i=0; i<playsetData["playsets"].Count; i++) {
			var playsetName = playsetData["playsets"][i].ToString();
			var playset = new Playset();
			playset.name = playsetName;
			var summary = (TextAsset)Resources.Load(playsetName + "/" + playsetName);
			//playset.summary = summary.text;
			//images
			playsets.Add(playset);
			Debug.Log(playsets.IndexOf(playset) + ": " + playset.name);
		}
	}
}
