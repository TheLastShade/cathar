using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private PlayerStat m_PlayerStat;
	private Rigidbody2D m_RigidBody;

	private float currentSpeed;
	// Use this for initialization
	void Start () {
		m_RigidBody = GetComponent<Rigidbody2D> ();
		m_PlayerStat = GetComponent<PlayerStat> ();
		InputManager.Instance.OnInputUpdated += OnInputUpdated;

		SetDefaultSpeedAndDrag ();
	}

	void SetDefaultSpeedAndDrag ()
	{
		m_RigidBody.drag = m_PlayerStat.m_MovementDrag;
		currentSpeed = m_PlayerStat.m_MovementSpeed;
	}

	void OnInputUpdated ()
	{
		float axisX = Input.GetAxis (ControllerSave.m_HorizontalAxis);
		float axisY = Input.GetAxis(ControllerSave.m_VerticalAxis);
		
		Vector2 velocity = new Vector2(Mathf.Lerp(0, axisX, 0.8f),
		                               Mathf.Lerp(0, axisY, 0.8f));
		
		float stickSpeed = Mathf.Max (Mathf.Abs (axisX), Mathf.Abs(axisY));
		//m_RigidBody.velocity = velocity.normalized * currentSpeed*stickSpeed;
		m_RigidBody.AddForce (velocity.normalized * currentSpeed * stickSpeed);
	}
	
	
	void OnTriggerEnter2D(Collider2D aCollider){
		TerrainModifier modifier = aCollider.GetComponent<TerrainModifier> ();
		
		if (modifier != null) {
			m_RigidBody.drag = modifier.dragModifier;
			
			if (modifier.speedModifier != 0){
				currentSpeed = modifier.speedModifier;
			}
			
		}
	}
	
	
	void OnTriggerExit2D (Collider2D aCollider){
		TerrainModifier modifier = aCollider.GetComponent<TerrainModifier> ();
		if (modifier != null) {
			SetDefaultSpeedAndDrag();
		}
	}
}
