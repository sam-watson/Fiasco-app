using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Playset {

	public string name;
	public PlaysetInfo info;
	public PlaysetElements elements;
	public Texture2D coverImage;
	public Texture2D thumbImage;
}

public class PlaysetInfo {
	
	public string subtitle;
	public string summary;
	public string movienight;
	public string credits;
}

public class PlaysetElements {
	
	public enum ElementType {
		Relationships,
		Needs,
		Locations,
		Objects
	}
	
	public Dictionary<string, List<string>> relationships;
	public Dictionary<string, List<string>> needs;
	public Dictionary<string, List<string>> locations;
	public Dictionary<string, List<string>> objects;
	
	public Dictionary<string, List<string>> GetElements (ElementType type) {
		switch (type) {
		case ElementType.Relationships:
			return relationships;
		case ElementType.Needs:
			return needs;
		case ElementType.Locations:
			return locations;
		default:
			return objects;
		}
	}
}
