﻿using UnityEngine;
using System.Collections;

abstract public class ManagerBase<T> : ManagerInitializer where T : ManagerInitializer {

	static private T m_Instance;

	public static T Instance {get {return m_Instance;}}

	protected void MakeSingleton(T aInstance)
	{
		m_Instance = aInstance;
	}

	void Awake()
	{
		CallMakeSingleton ();
	}

	abstract protected void CallMakeSingleton();
}
