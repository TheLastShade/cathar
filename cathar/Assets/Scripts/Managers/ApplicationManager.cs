using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ApplicationManager : MonoBehaviour {

	static private ApplicationManager m_Instance;
	public static ApplicationManager Instance {get {return m_Instance;}}

	void Awake()
	{
		m_Instance = this;

		DontDestroyOnLoad (gameObject);
		
		//We don't want to destroy manager ever, but if we have to, keep this list for later
		List<ManagerInitializer> listScriptManager = new List<ManagerInitializer>();
		
		foreach (GameObject manager in m_ListManager) {
			ManagerInitializer managerScript = CreateNewManager(manager);
			if(managerScript != null)
			{
				listScriptManager.Add(managerScript);
			}
		}
		
		foreach (ManagerInitializer managerScript in listScriptManager) {
			managerScript.PostInit();
		}
		
		m_IsInitialized = true;
		OnManagersInitialized ();
	}

	public Action OnManagersInitialized = delegate {};

	public List<GameObject> m_ListManager;

	private bool m_IsInitialized = false;

	public bool IsInitialized {get {return m_IsInitialized;}}

	ManagerInitializer CreateNewManager (GameObject aManager)
	{
		GameObject instance = Instantiate(aManager);
		instance.transform.SetParent(transform, false);

		ManagerInitializer managerScript = instance.GetComponent<ManagerInitializer>();
		if (managerScript != null) {
			managerScript.PreInit();

			return managerScript;
		}

		Debug.LogError ("You want to create a Manager without the needed BaseManager extended class");

		return null;
	}
}
