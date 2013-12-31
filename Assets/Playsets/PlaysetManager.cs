using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class PlaysetManager : MonoBehaviour {

	public List<Playset> playsets = new List<Playset>();
	
	void Start() {
		LoadAllPlaysets();
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
			var infoText = (TextAsset)Resources.Load(playsetName + "/" + playsetName + " Info");
			playset.info = JsonMapper.ToObject<PlaysetInfo>(infoText.text);
			var elementText = (TextAsset)Resources.Load(playsetName + "/" + playsetName + " Elements");
			playset.elements = ParseElements(elementText.text);
			//TODO: images
			playsets.Add(playset);
			Debug.Log("Loading... " + playsets.IndexOf(playset) + ": " + playset.name);
		}
	}
	
	public PlaysetElements ParseElements(string bunchoText) {
		var elements = new PlaysetElements();
		int c = 0;
		elements.relationships = ElementParser(bunchoText, ref c);
		elements.needs = ElementParser(bunchoText, ref c);
		elements.locations = ElementParser(bunchoText, ref c);
		elements.objects = ElementParser(bunchoText, ref c);
		return elements;
	}
	
	private Dictionary<string, List<string>> ElementParser(string readStr, ref int c) {
		//iterate through big string looking for numbers and new-lines, 6 sets of 6
		var element = new Dictionary<string, List<string>>();
		for (int i=1; i<=6; i++) {
			string keyStr;
			keyStr = SubElementParser(readStr, ref c, i);
			var subElems = new List<string>();
			for (int ii=1; ii<=6; ii++) {
				subElems.Add(SubElementParser(readStr, ref c, ii));
			}
			element.Add(keyStr, subElems);
		}
		//verify
		bool complete = true;
		if (element.Count != 6) {complete = false;}
		foreach (var subElem in element.Values) {
			if (subElem.Count != 6) {complete = false;}
		}
		if (complete == false) {Debug.Log("*** problem parsing playset elements!!! ***");}
		
		return element;
	}
	
	private string SubElementParser(string readStr, ref int c, int i) {
		string buildStr = "";
		if ( readStr[c].ToString() == i.ToString() && readStr[c+1].ToString() == " ") {
			c = c+2;
			//Debug.Log("----number found");
		}
		while ( c < readStr.Length ) {
			if ( readStr[c] == (char)13 && readStr[c+1] == (char)10 ) {
				//Debug.Log("----escape found");
				c = c+2;
				break;
			} else {
				buildStr += readStr[c].ToString();
				c++;
			}
		}
		return buildStr;
	}
}
