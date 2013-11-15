using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Playset {

	public string name;
	public string summary;
	public Texture2D coverImage;
	public Texture2D thumbImage;
	public PlaysetElements elements;
	
	public Playset(string name, string summary, string coverImg, string thumbImg, PlaysetElements elements) {
		this.name = name;
		this.summary = summary;
		this.coverImage = (Texture2D)Resources.Load(coverImg);
		this.thumbImage = Resources.Load(thumbImg) as Texture2D;
		this.elements = elements;
	}
	
	// JSON to class conversion
	public static explicit operator Playset(JSON value) {
		checked {
			return new Playset(
				value.ToString("name"),
				value.ToString("summary"),
				value.ToString("coverImage"),
				value.ToString("thumbImage"),
				(PlaysetElements)value.ToJSON("elements"));
		}
	}
	
	// convert a JSON array to a class Array
	public static Playset[] Array(JSON[] array) {
		List<Playset> tc = new List<Playset>();
		for (int i=0; i<array.Length; i++) {
			tc.Add((Playset)array[i]);
		}
		return tc.ToArray();
	}
	
	//serializer - not sure if needed, done for practice
//	public static implicit operator JSON(Playset value) {
//		JSON js = new JSON();
//		js["name"] = value.name;
//		js["summary"] = value.summary;
//		js["coverImage"] = value.coverImage.name;
//		js["thumbImage"] = value.thumbImage.name;
//		JSON jsElements = (JSON)value.elements;
//		js["elements"] = jsElements;
//		return js;
//	}
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
	
	// JSON to class conversion
	public static explicit operator PlaysetElements(JSON value) {
		checked {
			return new PlaysetElements(
				UnpackJsonElements(value, "relationships"),
				UnpackJsonElements(value, "needs"),
				UnpackJsonElements(value, "locations"),
				UnpackJsonElements(value, "objects"));
		}
	}
	
	private static Dictionary<string, List<string>> UnpackJsonElements(JSON json, string name) {
		JSON jsElement = json.ToJSON(name);
		var dict = new Dictionary<string, List<string>>();
		foreach (var e in jsElement.fields) {
			if (e.Value is IEnumerable) {
				dict.Add(e.Key, e.Value as List<string>);
			}
		}
		return dict;
	}
}
