using UnityEngine;
using System.Collections;

public class PlayerMovements : MonoBehaviour {

	public float m_Speed;
	public float defaultDrag;

	private Rigidbody2D m_RigidBody;


	void Start () {
		m_RigidBody = GetComponent<Rigidbody2D> ();
		m_RigidBody.drag = defaultDrag;
	}

	void FixedUpdate () 
	{
		float axisX = Input.GetAxis (ControllerSave.m_HorizontalAxis);
		float axisY = Input.GetAxis(ControllerSave.m_VerticalAxis);

		Vector2 velocity = new Vector2(Mathf.Lerp(0, axisX, 0.8f),
		                               Mathf.Lerp(0, axisY, 0.8f));

		float stickSpeed = Mathf.Max (Mathf.Abs (axisX), Mathf.Abs(axisY));
		//m_RigidBody.velocity = velocity.normalized * m_Speed*stickSpeed;
		m_RigidBody.AddForce (velocity.normalized * m_Speed * stickSpeed);
	}

	void OnTriggerEnter2D(Collider2D aCollider){
		TerrainModifier modifier = aCollider.GetComponent<TerrainModifier> ();

		if (modifier != null) {
			m_RigidBody.drag = modifier.dragModifier;
		}
	}

	void OnTriggerExit2D (Collider2D aCollider){
		TerrainModifier modifier = aCollider.GetComponent<TerrainModifier> ();
		if (modifier != null) {
			m_RigidBody.drag = defaultDrag;
		}
	}

}
