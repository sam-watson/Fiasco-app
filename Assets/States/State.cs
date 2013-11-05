using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class State {
	
	public GameObject menuPanel;
	public StateContext initContext;
	
	public virtual void Enter(StateContext context) {
		initContext = context;
		context.manager.currentState.Exit();
		context.manager.currentState = this;
	}
	
	public virtual void Exit() {
		menuPanel.SetActive(false);
	}
}

public class StateContext {
	
	public StateManager manager;
	
	public StateContext() {
		manager = StateManager.Instance;
	}
	
	
}