using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Playset : MonoBehaviour {

	public Texture2D coverImage;
	public Texture2D thumbImage;
	public string summary;
	public PlaysetElements elements;
}

public class PlaysetElements {
	
	public Dictionary<string, List<string>> relationships;
	public Dictionary<string, List<string>> needs;
	public Dictionary<string, List<string>> locations;
	public Dictionary<string, List<string>> objects;
}
