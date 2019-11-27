using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	public GameObject explosion;
    //public AudioClip explosion;

    public GameObject weapon;

	public float health;
	public float enemyProjectileSpeed;
	public int pointsWorth;

    private float enemyFireRate;
    private ScoreKeeper scoreKeeper;
	private PowerUpSpawner powerUpSpawner;
    private float saveHealth;
    private Transform saveTransform;

    [HideInInspector]
    public bool isInvulnerable;


    void Awake () {
		scoreKeeper = GameObject.Find ("ScoreKeeper").GetComponent<ScoreKeeper>();
		powerUpSpawner = FindObjectOfType<PowerUpSpawner>();
        SaveVariables(health, this.transform);
        enemyFireRate = weapon.GetComponent<Projectile>().fireRate;
	}
	
	void Update () {
		float probability = Time.deltaTime * enemyFireRate;
		if (Random.value < probability) {
			EnemyFire();
		}
	}

	void OnTriggerEnter2D (Collider2D collider) {
		Projectile beam = collider.GetComponent<Projectile>();
		if (beam && !isInvulnerable) {
			health -= beam.GetDamage();
			beam.Hit();
//			Debug.Log ("Enemy " + GetInstanceID() + " hit!");
			if (health <= 0) {
                //if (!SoundControllerScript.soundsMute) {AudioSource.PlayClipAtPoint (explosion, transform.position, 1.0f);}
                GameObject explode = Instantiate(explosion, this.transform.position, Quaternion.identity) as GameObject;
				powerUpSpawner.OnEnemyDestroyed(this.transform.position);
                gameObject.transform.parent.DetachChildren();
                gameObject.SetActive(false);
				scoreKeeper.Score (pointsWorth);				
			}
		}
	}

    private void OnDisable()
    {
        health = saveHealth;
        this.transform.position = new Vector3 (saveTransform.position.x, saveTransform.position.y, saveTransform.position.z);
        this.transform.rotation = Quaternion.identity;
        this.transform.localScale = new Vector3 (1.0f,1.0f,1.0f);
    }

    void EnemyFire () {
		Vector3 laserPosOffset = transform.position + new Vector3 (0.0f, -0.55f, 0.0f);
        GameObject enemyLaser = ObjectPooler.SharedInstance.GetPooledObject(weapon);
        if (enemyLaser != null)
        {
            enemyLaser.transform.position = laserPosOffset;
            enemyLaser.SetActive(true);
        }
        //if (!weapon.GetComponent<Projectile>().isHoming) { enemyLaser.GetComponent<Rigidbody2D>().velocity = Vector3.down * enemyProjectileSpeed; }
        if (!SoundControllerScript.soundsMute) {GetComponent<AudioSource>().Play();}
	}
	
	void DestroyTrail () {	
//		Debug.Log ("Destroy trail called");
		Component trail = gameObject.GetComponentInChildren<TrailRenderer>();
		DestroyObject (trail);
	}

    void SaveVariables(float health, Transform transform)
    { 
        saveHealth = health;
        saveTransform = transform;
    }
}
