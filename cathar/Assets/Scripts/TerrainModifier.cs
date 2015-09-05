using UnityEngine;
using System.Collections;

public class TerrainModifier : MonoBehaviour {

	//Suggested:
	//speedModifier = 10 :: Accelerates movement (oil)
	//speedModifier = -10 :: Decelerates movement (sand)
	public float speedModifier;

	//Suggested:
	//Minimum 2 to prevent facing issues, can be experimented with
	public float modifierThreshold;

	void OnTriggerStay2D(Collider2D aCollider){
		Rigidbody2D body = aCollider.GetComponentInParent<Rigidbody2D>();
		if (body.velocity.magnitude > modifierThreshold) {
			body.AddForce (body.velocity * speedModifier);
		}
	}
}
