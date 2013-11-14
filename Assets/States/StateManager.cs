using UnityEngine;
using System;
using System.Collections;

public class StateManager : MonoBehaviour {
	
	public GameObject playsetsMenu;
	
	public GameObject playsetButton;
	
	public PlaysetManager playsetManager;
	
	[HideInInspector] public State currentState;
	
	private static StateManager _Instance;
	public static StateManager Instance {
		get {
			return _Instance;
		}
	}
	
	void Start() {
		if (Instance != null) {
			throw new Exception("Attempted to create more than one instance of StateManager");
		}
		_Instance = this;
		playsetManager = gameObject.AddComponent<PlaysetManager>();
		new BrowsePlaysetsState().Enter(new StateContext());
	}
}
