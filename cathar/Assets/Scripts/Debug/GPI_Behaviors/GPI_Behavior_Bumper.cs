using UnityEngine;
using System.Collections;

public class GPI_Behavior_Bumper : MonoBehaviour {

	public float force;
	public bool isHurting;

	void OnCollisionEnter2D (Collision2D aCollision){
		Rigidbody2D newBody = aCollision.gameObject.GetComponentInParent<Rigidbody2D> ();
		if (newBody != null) {
			newBody.AddForce(-(this.transform.position - newBody.transform.position) * force);

			if (isHurting){
				//call for a specific anim?
			}
		}
	}

}
