using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour 
{

	public List<Image> m_ListHealthImage;
	public Sprite m_EmptyHealth;
	public Sprite m_HalfHealth;
	public Sprite m_FullHealth;

	public int m_MaxHealth;
	public int m_CurrentHealth;

	// Use this for initialization
	void Start () 
	{
		UpdateHealth ();
	}

	void GetHealthInfo ()
	{
		//TODO Use real health
		m_MaxHealth = Mathf.Min (m_ListHealthImage.Count * 2, m_MaxHealth);
		m_CurrentHealth = Mathf.Min (m_CurrentHealth, m_MaxHealth);
	}

	[ContextMenu("UpdateHealth")]
	public void UpdateHealth()
	{
		GetHealthInfo ();

		for (int i = 0; i < m_ListHealthImage.Count; i++) 
		{
			if(i < (m_MaxHealth/2))
			{
				m_ListHealthImage[i].gameObject.SetActive(true);
				int temp = m_CurrentHealth - ((i+1)*2);
				if(temp < -1)
				{
					m_ListHealthImage[i].sprite = m_EmptyHealth;
				}
				else if(temp < 0)
				{
					m_ListHealthImage[i].sprite = m_HalfHealth;
				}
				else
				{
					m_ListHealthImage[i].sprite = m_FullHealth;
				}
			}
			else
			{
				m_ListHealthImage[i].gameObject.SetActive(false);
			}
		}
	}
}
