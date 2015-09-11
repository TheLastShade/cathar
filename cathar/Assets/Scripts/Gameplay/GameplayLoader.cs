using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameplayLoader : MonoBehaviour {

	public bool m_LoadMapGPI = true;

	public bool m_IsLoadingMap = true;
	public string m_MapToLoad;
	public GameObject m_CharacterToLoad;
	public GameObject m_UIToLoad;

	public Follow2DTransform m_FollowCamera;

	private MapInfo m_CurrentMapInfo;
	private MapImporter m_MapImporter;


	private bool m_IsLoading = true;
	private GameObject m_Character = null;

	//TODO Should not use that later in game...
	public MapInfo CurrentMapInfo {get {return m_CurrentMapInfo;}}

	// Use this for initialization
	void Start () {
		
		m_MapImporter = gameObject.AddComponent<MapImporter> ();

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
		if (m_IsLoadingMap) {
			SetupMap (m_MapToLoad);
		}

		m_Character = SetupCharacter(Instantiate (m_CharacterToLoad));
		PlaceCharacter();

		if (m_UIToLoad) {
			SetupHealth(Instantiate (m_UIToLoad));
		}

		yield return 0;

		m_IsLoading = false;
	}

	void SetupMap (string aMapToLoad)
	{
		m_CurrentMapInfo = m_MapImporter.LoadMap (aMapToLoad);

		if (m_CurrentMapInfo == null) {
			Debug.LogError("Error while loading the map");
			return;
		}
		
		m_CurrentMapInfo.OnTriggerTeleport += OnTriggerTeleport;

		if (m_LoadMapGPI) {
			m_CurrentMapInfo.LoadGpi ();
		}

		m_FollowCamera.m_TopLeftLimit = m_CurrentMapInfo.m_CameraTopLeft;
		m_FollowCamera.m_BottomRightLimit = m_CurrentMapInfo.m_CameraBottomRight;
	}

	GameObject SetupCharacter (GameObject aCharacterInstantiated)
	{
		m_FollowCamera.m_ToFollow = aCharacterInstantiated.transform;

		return aCharacterInstantiated;
	}

	void PlaceCharacter (string aSpawnPointName = null)
	{
		Vector3 position = Vector3.zero;
		if (m_CurrentMapInfo != null) {
			position = m_CurrentMapInfo.GetSpawnPoint (aSpawnPointName);
		}
		position = new Vector3 (position.x, position.y, m_Character.transform.localPosition.z);
		m_Character.transform.localPosition = position;
	}

	void SetupHealth (GameObject aHealthUI)
	{
		HealthSystem healthSystem = aHealthUI.GetComponentInChildren<HealthSystem> ();
		healthSystem.m_PlayerStat = m_Character.GetComponent<PlayerStat> ();
	}

	void OnTriggerTeleport (string aMapToLoad, string aSpawnPoint)
	{
		if (!m_IsLoading) {
			StartCoroutine(LoadNewMap(aMapToLoad, aSpawnPoint));
		}

	}

	IEnumerator LoadNewMap (string aMapToLoad, string aSpawnPoint)
	{
		m_IsLoading = true;

		m_CurrentMapInfo.OnTriggerTeleport -= OnTriggerTeleport;
		m_MapImporter.UnloadCurrentMap (m_CurrentMapInfo);
		m_CurrentMapInfo = null;

		SetupMap (aMapToLoad);
		PlaceCharacter (aSpawnPoint);

		yield return 0;
		m_IsLoading = false;
	}


}
