using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class State {
	
	public GameObject menuPanel;
	public StateContext initialContext;
	
	public virtual void Enter(StateContext context) {
		initialContext = context;
		if (context.manager.currentState != null) {
			context.manager.currentState.Exit();
		}
		context.manager.currentState = this;
		Debug.Log("Entering " + this.GetType());
	}
	
	public virtual void Exit() {
		if (menuPanel != null)
			menuPanel.SetActive(false);
	}
}

public class StateContext {
	
	public StateManager manager;
	public PlaysetManager playsets;
	
	public StateContext() {
		manager = StateManager.Instance;
		playsets = manager.playsetManager;
	}
}