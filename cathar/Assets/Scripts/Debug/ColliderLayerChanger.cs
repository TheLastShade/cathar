using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColliderLayerChanger : MonoBehaviour {

	private Transform m_PlayerTransform;
	public Transform m_CentralPoint;
	public ColliderLayerChangerOrientation m_ColliderLayerChangerOrientation;
	public List<string> m_StartLayer;
	public List<string> m_EndLayer;

	//Find better way to dispatch event layer
	public GameplayLoader m_TempGameplayLoader;

	private bool m_DefaultLayerActivated;
	 
	void Update()
	{
		if (m_PlayerTransform != null) {
			float distance = GetDistanceCharacter();

			if(distance < 0)
			{
				if(!m_DefaultLayerActivated)
				{
					SwitchLayer();
				}
			}
			else
			{
				if(m_DefaultLayerActivated)
				{
					SwitchLayer();
				}
			}
		}
	}

	void SwitchLayer ()
	{
		if (m_DefaultLayerActivated) {
			
			m_TempGameplayLoader.CurrentMapInfo.ChangeCollisionLayer (m_StartLayer, m_EndLayer);
		} else {
			
			m_TempGameplayLoader.CurrentMapInfo.ChangeCollisionLayer (m_EndLayer, m_StartLayer);
		}
		m_DefaultLayerActivated = !m_DefaultLayerActivated;
		
		//Find better way to dispatch event layer
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		if (m_PlayerTransform != null) {
			Gizmos.DrawLine (m_CentralPoint.position, m_PlayerTransform.position);
		}
	}

	void OnDrawGizmosSelected() {
		if (m_CentralPoint != null) {
			Gizmos.color = Color.blue;
			Gizmos.DrawSphere(m_CentralPoint.position, 0.02f);
		}
	}

	private float GetDistanceCharacter()
	{
		float distance = 0f;
		if (m_PlayerTransform != null) {
			switch (m_ColliderLayerChangerOrientation) {
			case ColliderLayerChangerOrientation.HORIZONTAL_LEFT_TO_RIGHT:{
				distance = m_CentralPoint.position.x - m_PlayerTransform.position.x;
				break;
			}
			case ColliderLayerChangerOrientation.HORIZONTAL_RIGHT_TO_LEFT:{
				distance = m_PlayerTransform.position.x - m_CentralPoint.position.x;
				break;
			}
			case ColliderLayerChangerOrientation.VERTICAL_TOP_BOTTOM:{
				distance = m_CentralPoint.position.y - m_PlayerTransform.position.y;
				break;
			}
			case ColliderLayerChangerOrientation.VERTICAL_BOTTOM_TOP:{
				distance = m_PlayerTransform.position.y - m_CentralPoint.position.y;
				break;
			}
			default:
			break;
			}
		}
		return distance;
	}


	void OnTriggerEnter2D (Collider2D aCollision)
	{
		PlayerStat playerStat = aCollision.gameObject.GetComponentInParent<PlayerStat> ();
		if (playerStat != null) {
			m_PlayerTransform = aCollision.gameObject.transform;
		}
	}

	void OnTriggerExit2D(Collider2D aCollision) 
	{
		PlayerStat playerStat = aCollision.gameObject.GetComponentInParent<PlayerStat> ();
		if (playerStat != null) {
			m_PlayerTransform = null;
		}
	}
}

public enum ColliderLayerChangerOrientation
{
	HORIZONTAL_LEFT_TO_RIGHT,
	HORIZONTAL_RIGHT_TO_LEFT,
	VERTICAL_TOP_BOTTOM,
	VERTICAL_BOTTOM_TOP,
}
