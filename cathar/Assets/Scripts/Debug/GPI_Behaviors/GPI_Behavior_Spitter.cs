using UnityEngine;
using System.Collections;

public class GPI_Behavior_Spitter : MonoBehaviour {

	public GameObject projectile;
	public GameObject muzzleFlare;
	public float cooldown;
	public float speed;
	int currentCooldown;
	
	void Update () {
		currentCooldown ++;

		if (currentCooldown >= cooldown) {
			currentCooldown = 0;


			if (muzzleFlare != null){
				GameObject newMuzzleFlare = Instantiate(muzzleFlare);
				Animator anim = newMuzzleFlare.GetComponent<Animator>();
				anim.speed = 4f;
				newMuzzleFlare.transform.localScale = new Vector3 (0.5f, 1.5f, 1f);
				newMuzzleFlare.transform.rotation = this.transform.rotation;
				newMuzzleFlare.transform.position = this.transform.position + (this.transform.up * 0.6f);
				newMuzzleFlare.transform.position = new Vector3 (newMuzzleFlare.transform.position.x, newMuzzleFlare.transform.position.y, 40);
			}


			if (projectile != null)
			{
				GameObject newProjectile = Instantiate(projectile);
				SpriteRenderer sr = newProjectile.GetComponent<SpriteRenderer>();
				//newProjectile.transform.position = this.transform.position + (this.transform.up * 1f);
				newProjectile.transform.rotation = this.transform.rotation;
				newProjectile.transform.position = this.transform.position + this.transform.up * (sr.bounds.extents.magnitude * 3f);
				Rigidbody2D newBody = newProjectile.GetComponent<Rigidbody2D>();
				newBody.AddForce (newProjectile.transform.up * speed);
				//newProjectile.transform.position = new Vector3 (newProjectile.transform.position.x, newProjectile.transform.position.y, 50);
			}

		}
	}
}
