using UnityEngine;
using System.Collections;
using System;

public class PlayerAttack : MonoBehaviour {
	
	public float m_AttackCooldown;

	public Action OnAttackTrigger = delegate {};

	private float m_CurrentWaiting;
	// Update is called once per frame
	void Update () {
		float fire = Input.GetAxis (ControllerSave.m_AttackButton);

		if (m_CurrentWaiting > 0) {
			m_CurrentWaiting -= Time.deltaTime;
			if(m_CurrentWaiting < 0){
				m_CurrentWaiting = 0;
			}
		}
		if ((fire > 0) && (m_CurrentWaiting <=0)) {
			m_CurrentWaiting = m_AttackCooldown;
			OnAttackTrigger();
		}
	}
}
