using UnityEngine;
using System.Collections;

public class GPI_Behavior_Spitter : MonoBehaviour {

	public GameObject projectile;
	public float cooldown;
	public float speed;
	int currentCooldown;
	
	void Update () {
		currentCooldown ++;

		if (currentCooldown >= cooldown) {
			currentCooldown = 0;
			GameObject newProjectile = Instantiate(projectile);
			newProjectile.transform.position = this.transform.position + this.transform.up * 0.75f;
			newProjectile.transform.rotation = this.transform.rotation;
			Rigidbody2D newBody = newProjectile.GetComponent<Rigidbody2D>();
			newBody.AddForce (newBody.transform.up * speed);
			//newProjectile.transform.position = new Vector3 (newProjectile.transform.position.x, newProjectile.transform.position.y, 50);
		}
	}
}
