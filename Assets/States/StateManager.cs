using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour {
	
	public GameObject playsetsMenu;
	
	public GameObject playsetButton;
	
	public PlaysetManager playsetManager;
	
	[HideInInspector] public State currentState;
	
	private static StateManager _Instance;
	public static StateManager Instance {
		get {
			if (_Instance == null) 
				throw new System.Exception("StateManager is null");
			return _Instance;
		}
	}
	
	void Start() {
		playsetManager = gameObject.AddComponent<PlaysetManager>();
		new BrowsePlaysetsState().Enter(new StateContext());
	}
}
