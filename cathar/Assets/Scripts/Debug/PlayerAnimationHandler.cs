using UnityEngine;
using System.Collections;

public class PlayerAnimationHandler : MonoBehaviour {

	public Animator m_Animator;
	
	private Rigidbody2D m_RigidBody;
	// Use this for initialization
	void Start () 
	{
		m_RigidBody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector2 velocity = m_RigidBody.velocity.normalized;
		bool isWalking = (m_RigidBody.velocity.magnitude > 0f);
		if (isWalking) {

			m_Animator.SetFloat ("x", velocity.x);
			m_Animator.SetFloat ("y", velocity.y);
		}

		m_Animator.SetBool ("isWalking", isWalking);
	}
}
