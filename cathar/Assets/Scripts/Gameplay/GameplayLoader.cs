using UnityEngine;
using System.Collections;

public class GameplayLoader : MonoBehaviour {

	public GameObject m_MapToLoad;
	public GameObject m_CharacterToLoad;
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
		SetupMap (Instantiate (m_MapToLoad));

		GameObject character = SetupCharacter(Instantiate (m_CharacterToLoad));

		SetupHealth(Instantiate (m_UIToLoad), character);
		yield return 0;
	}

	void SetupMap (GameObject aMapInstantiated)
	{
		MapInfo mapInfo = aMapInstantiated.GetComponent<MapInfo> ();

		if (mapInfo == null) {
			Debug.LogError("Map must have a Map Info Script on it");
			return;
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
