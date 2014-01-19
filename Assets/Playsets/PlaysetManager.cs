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
			playset.elements = new ElementParser().ParseElements(elementText.text);
			//TODO: images
			playsets.Add(playset);
			Debug.Log("Loading... " + playsets.IndexOf(playset) + ": " + playset.name);
		}
	}
}
	
public class ElementParser {
	
	PlaysetElements elements;
	int elemType = 0;
	Dictionary<string, List<string>> elemDict;
	string curKey;
	int keyElemNum;
	int subElemNum;
	int diceNum;
	int diceSize = 6;
	
	private void Init() {
		elements = new PlaysetElements();
		//set vars to trigger a new cycle and dictionary
		elemType = -1;
		keyElemNum = diceSize;
		subElemNum = diceSize;
	}
	
	public PlaysetElements ParseElements(string bunchoText) {
		Init();
		char[] eols = {'\r', '\n'};
		string[] elementStrings = bunchoText.Split(eols, System.StringSplitOptions.RemoveEmptyEntries);
		foreach (var elemString in elementStrings) {
			if (!System.Int32.TryParse(elemString.Substring(0,2), out diceNum)) {
				break; //unnumbered line
			}
			if (!PlaceElement(diceNum, elemString.Substring(2))) {
				break; //finished or misnumbered element list == stop
			}
		}
		return elements;
	}
	
	private bool PlaceElement(int diceNum, string elemString) {
		if (subElemNum < diceSize && diceNum == ++subElemNum) {
			elemDict[curKey].Add(elemString);
			return true;
		} else if (keyElemNum < diceSize && diceNum == ++keyElemNum) {
			elemDict.Add(elemString, new List<string>());
			curKey = elemString;
			subElemNum = 0;
			return true;
		} else if (System.Enum.IsDefined(typeof(PlaysetElements.ElementType), ++elemType)) {
			elemDict = elements.GetElements(elemType);
			elemDict.Add(elemString, new List<string>());
			curKey = elemString;
			keyElemNum = 1;
			subElemNum = 0;
			return true;
		} else return false; //parsing finished or number mismatch
	}
}