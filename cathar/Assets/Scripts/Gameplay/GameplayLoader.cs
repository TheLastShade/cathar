using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameplayLoader : MonoBehaviour {

	public bool m_LoadMap = true;
	public bool m_LoadMapGPI = true;
	public string m_MapToLoad;
	
	public bool m_LoadCharacter = true;
	public GameObject m_CharacterToLoad;

	public bool m_LoadUI = true;
	public GameObject m_UIToLoad;

	public Follow2DTransform m_FollowCamera;

	// Use this for initialization
	void Start () {
		if (ApplicationManager.Instance.IsInitialized) {
			OnManagersInitlized();
		} else {
			ApplicationManager.Instance.OnManagersInitialized += OnManagersInitlized;
		}
	}

	void OnManagersInitlized (){
		ApplicationManager.Instance.OnManagersInitialized -= OnManagersInitlized;
		StartCoroutine (LoadGameplay ());
	}

	IEnumerator LoadGameplay (){
		if (m_LoadMap) {
			SetupMap ();
		}

		GameObject character = null;
		if (m_LoadCharacter) {
			character = SetupCharacter(Instantiate (m_CharacterToLoad));
		}

		if (m_UIToLoad) {
			SetupHealth(Instantiate (m_UIToLoad), character);
		}

		yield return 0;
	}

	void SetupMap ()
	{
		MapImporter mapImporter = gameObject.AddComponent<MapImporter> ();

		MapInfo mapInfo = mapImporter.LoadMap (m_MapToLoad);

		if (mapInfo == null) {
			Debug.LogError("Error while loading the map");
			return;
		}

		if (m_LoadMapGPI) {
			mapInfo.LoadGpi ();
		}

		m_FollowCamera.m_TopLeftLimit = mapInfo.m_CameraTopLeft;
		m_FollowCamera.m_BottomRightLimit = mapInfo.m_CameraBottomRight;
	}

	GameObject SetupCharacter (GameObject aCharacterInstantiated)
	{
		m_FollowCamera.m_ToFollow = aCharacterInstantiated.transform;

		return aCharacterInstantiated;
	}

	void SetupHealth (GameObject aHealthUI, GameObject aCharacterInstantiated)
	{
		HealthSystem healthSystem = aHealthUI.GetComponentInChildren<HealthSystem> ();
		healthSystem.m_PlayerStat = aCharacterInstantiated.GetComponent<PlayerStat> ();
	}
}
