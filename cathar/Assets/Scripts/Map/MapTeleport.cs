using UnityEngine;
using System.Collections;
using System;

public class MapTeleport : MonoBehaviour 
{
	public Action<string, string> OnTriggerTeleport = delegate {};
	public string m_MapToTeleport;
	public string m_SpawnPointToTeleport;



	void OnTriggerEnter2D(Collider2D aCollision) 
	{
		TriggerTeleport (aCollision.gameObject);
	}

	void OnCollisionEnter2D (Collision2D aCollision)
	{
		TriggerTeleport (aCollision.gameObject);
	}

	void TriggerTeleport (GameObject aColliderGo)
	{
		PlayerStat playerStat = aColliderGo.GetComponentInParent<PlayerStat> ();
		if (playerStat != null) {
			OnTriggerTeleport(m_MapToTeleport, m_SpawnPointToTeleport);
		}
	}
}
