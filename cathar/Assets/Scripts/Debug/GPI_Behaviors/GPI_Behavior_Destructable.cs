﻿using UnityEngine;
using System.Collections;

public class GPI_Behavior_Destructable : MonoBehaviour {

	public int lifePoints;
	public GameObject deathSpawn;
	public GameObject rewardSpawn;

	void Update () {
	
	}


	void OnCollisionEnter2D (Collision2D aCollision){
		PlayerStat playerStat = aCollision.gameObject.GetComponentInParent<PlayerStat> ();
		
		if (playerStat != null) {
			ReceiveDamage (1);
		}
	}


	void ReceiveDamage (int damage){
		//TODO: handle armor if any: damage -= ?!
		//TODO: add damagetype if any

		lifePoints -= damage;
		if (lifePoints <= 0) {
			Death ();
		}
	}


	void Death () {

		//Create a generic death effect, most likely a timed animation
		if (deathSpawn != null) {
			GameObject death = Instantiate (deathSpawn);
			death.transform.position = this.transform.position;
		}

		//Create a generic reward on death, most likely a random generator that generates (or not) some loot
		if (rewardSpawn != null) {
			GameObject reward = Instantiate (rewardSpawn);
			reward.transform.position = this.transform.position;
		}

		//TODO: Add sound
		Destroy (this.gameObject);
	}
}