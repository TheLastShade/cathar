using UnityEngine;
using System.Collections;
using System;

public class InputManager : ManagerBase<InputManager> 
{
	public Action OnInputUpdated = delegate {};

	public string m_AttackButton = "Button0";
	public string m_ActionButton = "Button1";
	public string m_HorizontalAxis = "Horizontal";
	public string m_VerticalAxis = "Vertical";

	private float m_AxisX;
	private float m_AxisY;

	private float m_Attack;
	private float m_Action;

	public float Input_AxisX {get {return m_AxisX;}}
	public float Input_AxisY {get {return m_AxisY;}}

	public float Input_Attack {get {return m_Attack;}}
	public float Input_Action {get {return m_Action;}}
	
	protected override void CallMakeSingleton ()
	{
		MakeSingleton (this);
	}
	
	public override void PreInit ()
	{
		base.PreInit ();
	}
	public override void PostInit ()
	{
		base.PostInit ();

		//TODO Load saved controller here

	}

	void FixedUpdate () 
	{
		m_AxisX = Input.GetAxis(m_HorizontalAxis);
		m_AxisY = Input.GetAxis(m_VerticalAxis);
		
		m_Attack = Input.GetAxis (m_AttackButton);
		m_Action = Input.GetAxis (m_ActionButton);

		OnInputUpdated ();
	}

}
