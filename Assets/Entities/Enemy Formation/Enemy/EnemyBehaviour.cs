using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	public GameObject weapon;
	public AudioClip explosion;

	public float health;
	public float enemyProjectileSpeed;
	public float enemyFireRate;
	public int pointsWorth;
	
	private ScoreKeeper scoreKeeper;
	private PowerUpSpawner powerUpSpawner;
	
	void Start () {
		scoreKeeper = GameObject.Find ("ScoreKeeper").GetComponent<ScoreKeeper>();
		powerUpSpawner = FindObjectOfType<PowerUpSpawner>();
	}
	
	void Update () {
		float probability = Time.deltaTime * enemyFireRate;
		if (Random.value < probability) {
			EnemyFire();
		}
	}

	void OnTriggerEnter2D (Collider2D collider) {
		Projectile beam = collider.GetComponent<Projectile>();
		if (beam) {
			health -= beam.GetDamage();
			beam.Hit();
//			Debug.Log ("Enemy " + GetInstanceID() + " hit!");
			if (health <= 0) {
				if (!SoundControllerScript.soundsMute) {AudioSource.PlayClipAtPoint (explosion, transform.position, 1.0f);}
				powerUpSpawner.OnEnemyDestroyed(this.transform.position);
				Destroy (gameObject);
				scoreKeeper.Score (pointsWorth);				
			}
		}
	}
	
	void EnemyFire () {
		Vector3 laserPosOffset = transform.position + new Vector3 (0.0f, -0.55f, 0.0f);
		GameObject enemyLaser = Instantiate (weapon, laserPosOffset, Quaternion.identity) as GameObject;
		enemyLaser.rigidbody2D.velocity = Vector3.down * enemyProjectileSpeed;
		if (!SoundControllerScript.soundsMute) {audio.Play();}
	}
	
	void DestroyTrail () {	
//		Debug.Log ("Destroy trail called");
		Component trail = gameObject.GetComponentInChildren<TrailRenderer>();
		DestroyObject (trail);
	}
}
