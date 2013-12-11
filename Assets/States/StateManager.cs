using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class StateManager : MonoBehaviour {
	
	public GameObject browserPanel;
	public GameObject viewerPanel;
	public Prefabs prefabs;
	
//	public GameObject playsetButtonPrefab;
//	public GameObject playsetLabelPrefab;
//	public GameObject playsetSubPagePrefab;
	
	private PlaysetManager playsetManager;
	[HideInInspector] public State currentState;
	
	private static StateManager _Instance;
	public static StateManager Instance {
		get {
			return _Instance;
		}
	}
	
	private List<Playset> _playsets;
	public List<Playset> Playsets {
		get {
			if (playsetManager != null) {
				return playsetManager.playsets;
			} else throw new Exception("No playset manager is set");
		}
	}
	
	void Start() {
		if (Instance != null) {
			throw new Exception("Attempted to create more than one instance of StateManager");
		}
		_Instance = this;
		playsetManager = gameObject.AddComponent<PlaysetManager>();
		new WaitingState().Enter(new StateContext());
	}
	
	public void WaitForIt(float time, EventDelegate it) {
		StartCoroutine(WaitingForIt(time, it));
	}
	
	private IEnumerator WaitingForIt(float time, EventDelegate it) {
		yield return new WaitForSeconds(time);
		it.Execute();
	}
}