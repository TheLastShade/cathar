using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private PlayerStat m_PlayerStat;
	private Rigidbody2D m_RigidBody;
	// Use this for initialization
	void Start () {
		m_RigidBody = GetComponent<Rigidbody2D> ();
		m_PlayerStat = GetComponent<PlayerStat> ();
		//InputManager.Instance.OnInputUpdated += OnInputUpdated;
	}

	void OnInputUpdated ()
	{
		float axisX = InputManager.Instance.Input_AxisX;
		float axisY = InputManager.Instance.Input_AxisY;
		
		Vector2 velocity = new Vector2(Mathf.Lerp(0, axisX, 0.8f),
		                               Mathf.Lerp(0, axisY, 0.8f));
		
		float stickSpeed = Mathf.Max (Mathf.Abs (axisX), Mathf.Abs(axisY));
		m_RigidBody.velocity = velocity.normalized * m_PlayerStat.m_MovementSpeed * stickSpeed;
	}
}
