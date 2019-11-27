using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public enum PowerUpType { healthUp, shield, laserMode };

public class PowerUp : MonoBehaviour {

    PlayerController playerController;

	public float healthBonus, shieldDuration, shieldHealth;
    public GameObject weapon;
    public GameObject shield;
	public PowerUpType powerUpType;

    //[Range(0, 100)]
    public int chanceToDrop;
	

	// Use this for initialization
	void Start ()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
	}
	
    public void GetBonus()
    {
        if (powerUpType == PowerUpType.laserMode)
        {
            if (weapon)
            {
                playerController.weapon = weapon;
                playerController.projectile = playerController.weapon.GetComponent<Projectile>();
            }
        }

        if (powerUpType == PowerUpType.healthUp)
        {
            playerController.playerHealth += healthBonus;
            if (playerController.playerHealth > playerController.playerMaxHealth) { playerController.playerHealth = playerController.playerMaxHealth; }
        }

        if (powerUpType == PowerUpType.shield)
        {

            if (playerController.shieldOnline == true)
            {
                ChargeShield();
            }
            else
            {
                SpawnShield();
            }

        }
    }

    private void SpawnShield ()
    {
        GameObject deflector = Instantiate(shield, playerController.transform.position, Quaternion.identity) as GameObject;
        deflector.transform.parent = playerController.transform;
        DeflectorShield shieldSpawned = deflector.GetComponent<DeflectorShield>();
        shieldSpawned.shieldDuration = shieldDuration;
        shieldSpawned.shieldHealth = shieldHealth;
        playerController.shieldOnline = true;
    }
	
    private void ChargeShield ()
    {
        DeflectorShield currentShield = GameObject.FindGameObjectWithTag("Shield").GetComponent<DeflectorShield>();
        currentShield.shieldDuration += shieldDuration;
        currentShield.shieldHealth += shieldHealth;
        Debug.Log("Shield health remaining: " + currentShield.shieldHealth);
    }
}
