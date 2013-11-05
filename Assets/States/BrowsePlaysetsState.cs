﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaysetsState : State {
	
	public override void Enter (StateContext context)
	{
		base.Enter (context);
		menuPanel = context.manager.playsetsMenu;
		//generate buttons linking to playset viewer state
	}
	
	public override void Exit ()
	{
		base.Exit ();
	}
}
