using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour 
{
	private const int HEALTH_QUANTITY = 8;
	public List<Image> m_ListHealthImage;

	public Sprite m_EmptyHealth;
	public Sprite m_QuarterHealth;
	public Sprite m_HalfHealth;
	public Sprite m_ThreeQuarterHealth;
	public Sprite m_FullHealth;

	private int m_MaxHealth;
	private int m_CurrentHealth;

	public PlayerStat m_PlayerStat;

	// Use this for initialization
	void Start () 
	{
		m_PlayerStat.OnHealthChanged += OnHealthChanged;
		OnHealthChanged ();
	}

	void OnDestroy()
	{
		m_PlayerStat.OnHealthChanged -= OnHealthChanged;

	}

	void OnHealthChanged ()
	{
		//TODO Use real health
		m_MaxHealth = Mathf.Min (m_ListHealthImage.Count * HEALTH_QUANTITY, m_PlayerStat.m_MaxHealth);
		m_CurrentHealth = Mathf.Min (m_PlayerStat.m_CurrentHealth, m_MaxHealth);
		UpdateHealth ();
	}

	public void UpdateHealth()
	{
		int heart = m_CurrentHealth / HEALTH_QUANTITY;
		int moduloHearth = m_CurrentHealth % HEALTH_QUANTITY;
		for (int i = 0; i < m_ListHealthImage.Count; i++) 
		{
			if(i <= m_MaxHealth)
			{
				m_ListHealthImage[i].gameObject.SetActive(true);
				Sprite sprite = m_FullHealth;

				if(i <= heart)
				{
					if(i >= heart)
					{
						switch (moduloHearth) 
						{
						case 0:
						{
							sprite = m_EmptyHealth;
							break;
						}
						case 1:
						case 2:
						{
							sprite = m_QuarterHealth;
							break;
						}
						case 3:
						case 4:
						{
							sprite = m_HalfHealth;
							break;
						}
						case 5:
						case 6:
						{
							sprite = m_ThreeQuarterHealth;
							break;
						}
						}
					}
				}
				else
				{
					sprite = m_EmptyHealth;
				}

				m_ListHealthImage[i].sprite = sprite;
			}
			else
			{
				m_ListHealthImage[i].gameObject.SetActive(false);
			}
		}
	}
}
