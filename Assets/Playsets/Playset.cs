using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Playset {

	public string name;
	public string summary;
	public Texture2D coverImage;
	public Texture2D thumbImage;
	public PlaysetElements elements;
	
	public Playset() {}
	
	public Playset(string name, string summary, string coverImg, string thumbImg, PlaysetElements elements) {
		this.name = name;
		this.summary = summary;
		this.coverImage = (Texture2D)Resources.Load(coverImg);
		this.thumbImage = Resources.Load(thumbImg) as Texture2D;
		this.elements = elements;
	}
}

public class PlaysetElements {
	
	public Dictionary<string, List<string>> relationships;
	public Dictionary<string, List<string>> needs;
	public Dictionary<string, List<string>> locations;
	public Dictionary<string, List<string>> objects;
	
	public PlaysetElements(Dictionary<string, List<string>> relationships, Dictionary<string, List<string>> needs,
		Dictionary<string, List<string>> locations, Dictionary<string, List<string>> objects) {
		this.relationships = relationships;
	}
	
}
