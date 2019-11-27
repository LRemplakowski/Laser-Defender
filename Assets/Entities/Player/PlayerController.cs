using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject weapon;

    [HideInInspector]
    public Projectile projectile;
    [HideInInspector]
	public float playerMaxHealth;
    [HideInInspector]
    public bool shieldOnline = false;

    public float playerHealth;
    public float movementSpeed;
	public float edgePadding;
    //Moved to Projectile.cs for sanity reasons
    //public float projectileSpeed;
    //public float fireRate;

    private HealthCount healthCount;
	private float xMin, xMax;
    private float dualOffset;
	
	void Start () {
        playerMaxHealth = playerHealth;
        projectile = weapon.GetComponent<Projectile>();
        healthCount = GameObject.Find("Health Count").GetComponent<HealthCount>();
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftEdge = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 rightEdge = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance));
		xMin = leftEdge.x + edgePadding;
		xMax = rightEdge.x - edgePadding;
        dualOffset = -0.3f;
	}
	
	// Update is called once per frame
	void Update () {
		PlayerMovement ();
		
		if (Input.GetKeyDown (KeyCode.Space)) {InvokeRepeating ("Fire", 0.000001f, projectile.fireRate);}
		if (Input.GetKeyUp (KeyCode.Space)) {CancelInvoke ("Fire");}
	}
	
	void OnTriggerEnter2D (Collider2D collider) {
		Projectile beam = collider.GetComponent<Projectile>();
		if (beam && !shieldOnline)
        {
			playerHealth -= beam.GetDamage();
            healthCount.HealthCountUpdate();
			beam.Hit();
//			Debug.Log ("Player hit!");
			if (playerHealth <= 0)
            {
				Die ();
			}
		}
        PowerUp bonus = collider.GetComponent<PowerUp>();
        if (bonus)
        {
            bonus.GetBonus();
            if (bonus.powerUpType == PowerUpType.healthUp) { healthCount.HealthCountUpdate(); }
            Destroy(bonus);
        }
	}
	
	void Die () {
		LevelManagerScript Menago = GameObject.Find ("Level Manager").GetComponent <LevelManagerScript>();
		Menago.LoadLevel ("Win");
		Destroy (this.gameObject);
	}
	
	void Fire () {
        if (projectile.isDual)
        {
            Vector3 laserPosOffset = transform.position + new Vector3(dualOffset, 0.25f, 0.0f);
            GameObject laser = ObjectPooler.SharedInstance.GetPooledObject(weapon);
            if (laser != null)
            {
                laser.transform.position = laserPosOffset;
                laser.SetActive(true);
            }
            //laser.GetComponent<Rigidbody2D>().velocity = Vector3.up * projectile.projectileSpeed;
            if (!SoundControllerScript.soundsMute) { GetComponent<AudioSource>().Play(); }
            dualOffset = -dualOffset;
        }
        else
        {
            Vector3 laserPosOffset = transform.position + new Vector3(0.0f, 0.55f, 0.0f);
            GameObject laser = ObjectPooler.SharedInstance.GetPooledObject(weapon);
            if (laser != null)
            {
                laser.transform.position = laserPosOffset;
                laser.SetActive(true);
            }
            //laser.GetComponent<Rigidbody2D>().velocity = Vector3.up * projectile.projectileSpeed;
            if (!SoundControllerScript.soundsMute) { GetComponent<AudioSource>().Play(); }
        }
	}
	
	void PlayerMovement () {
		if (Input.GetKey(KeyCode.LeftArrow)) {
			transform.position += Vector3.left * movementSpeed * Time.deltaTime;
		} else
			
		if (Input.GetKey(KeyCode.RightArrow)) {
			transform.position += Vector3.right * movementSpeed * Time.deltaTime;
		}
		
		// Restricting player to gamespace
		float newX = Mathf.Clamp (transform.position.x, xMin, xMax);
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);
	}


}
