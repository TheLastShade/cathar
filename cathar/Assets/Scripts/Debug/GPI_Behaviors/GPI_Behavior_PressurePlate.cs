using UnityEngine;
using System.Collections;

public class GPI_Behavior_PressurePlate : MonoBehaviour {

	public float requiredMass;
	public bool isResettable;
	public Sprite enabledSprite;
	public Sprite disabledSprite;


	SpriteRenderer sr;

	bool triggerEnabled = false;



	void OnTriggerStay2D(Collider2D aCollision){
		Rigidbody2D newBody = aCollision.gameObject.GetComponentInParent<Rigidbody2D> ();


		if (newBody != null) {
			if (newBody.mass >= requiredMass){
				triggerEnabled = true;
			}
		}
	}

	void OnTriggerExit2D(Collider2D aCollision){
		triggerEnabled = false;
	}

	void Update(){

		if (sr == null) {
			sr = this.GetComponent<SpriteRenderer>();
		}

		if (triggerEnabled) {
			sr.sprite = enabledSprite;
		} else {
			sr.sprite = disabledSprite;
		}
	}


	public bool isEnabled(){
		return triggerEnabled;
	}

}
