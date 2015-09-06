﻿using UnityEngine;
using System.Collections;

public class LifeChanger : MonoBehaviour 
{
	public float m_WaitingTimeSec;
	public int m_QuantityToChange;
	private IEnumerator m_CoroutineLifeChanger;

	void OnTriggerEnter2D(Collider2D aCollision) 
	{
		PlayerStat playerStat = aCollision.gameObject.GetComponentInParent<PlayerStat> ();
		DispatchEventTrigger (playerStat);

	}

	void OnCollisionEnter2D (Collision2D aCollision)
	{
		PlayerStat playerStat = aCollision.gameObject.GetComponentInParent<PlayerStat> ();
		DispatchEventTrigger (playerStat);

	}

	void OnTriggerExit2D(Collider2D aCollision) 
	{
		PlayerStat playerStat = aCollision.gameObject.GetComponentInParent<PlayerStat> ();
		KillEventTrigger (playerStat);		

	}
	
	void OnCollisionExit2D(Collision2D aCollision) 
	{
		PlayerStat playerStat = aCollision.gameObject.GetComponentInParent<PlayerStat> ();
		KillEventTrigger (playerStat);		

	}

	void DispatchEventTrigger (PlayerStat playerStat)
	{
		if (playerStat != null) 
		{
			TryStopLifeChange();
			m_CoroutineLifeChanger = ChangeLife(playerStat);
			StartCoroutine(m_CoroutineLifeChanger);
		}
	}

	IEnumerator ChangeLife(PlayerStat aPlayerStat)
	{
		do 
		{
			aPlayerStat.ChangeHealth(m_QuantityToChange);
			yield return new WaitForSeconds(m_WaitingTimeSec);
		} while(true);
	}
	


	void KillEventTrigger (PlayerStat playerStat)
	{
		if (playerStat != null) 
		{
			TryStopLifeChange();
		}
	}


	private void TryStopLifeChange()
	{
		if(m_CoroutineLifeChanger != null)
		{
			StopCoroutine(m_CoroutineLifeChanger);
		}
	}
}
