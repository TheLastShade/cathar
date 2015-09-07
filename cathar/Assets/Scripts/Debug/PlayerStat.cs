using UnityEngine;
using System.Collections;
using System;

public class PlayerStat : MonoBehaviour 
{
	public Action OnHealthChanged = delegate {};
	private const int TOP_HEALTH = 128; 
	public int m_MaxHealth;
	public int m_CurrentHealth;

	public float m_MovementSpeed;
	public float m_MovementDrag;

	// Use this for initialization
	void Start () 
	{
		//TODO Find current health from saves
	}

	public void ChangeHealth(int aQuantity)
	{
		m_CurrentHealth += aQuantity;
		m_CurrentHealth = Math.Max (m_CurrentHealth, 0);
		m_CurrentHealth = Math.Min (m_CurrentHealth, m_MaxHealth);
		OnHealthChanged ();
	}

}
