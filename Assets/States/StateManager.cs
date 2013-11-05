using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour {
	
	public GameObject playsetsMenu;
	
	[HideInInspector] public State currentState;
	
	public static StateManager Instance {
		get {
			if (_Instance == null) 
				throw new System.Exception("StateManager is null");
			return _Instance;
		}
	}
	private static StateManager _Instance;
	
}
