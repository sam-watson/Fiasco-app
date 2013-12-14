using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class State {
	
	public GameObject menuPanel;
	protected StateContext initialContext;
	
	protected PageMap pageMap;
	
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
	
	protected void SetMenuPanel(GameObject menuPanel) {
		this.menuPanel = menuPanel;
		menuPanel.SetActive(true);
		pageMap = menuPanel.GetComponent<PageMap>();
	}
}

public class StateContext {
	
	public StateManager manager;
	public readonly Playset playset;
	
	public StateContext() {
		manager = StateManager.Instance;
	}
	
	public StateContext(Playset playset) {
		manager = StateManager.Instance;
		this.playset = playset;
	}
}