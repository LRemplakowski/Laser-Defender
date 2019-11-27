using UnityEngine;
using System.Collections;

public class PowerUpSpawner : MonoBehaviour {

	public GameObject[] powerUps;

//	public enum powerUpType {healthUp, shield, standardLaser, doubleLaser, fastLaser, strongLaser};

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
	

	public void OnEnemyDestroyed (Vector2 spawnPosition) {
		GameObject powerUp = powerUps[Random.Range (0, powerUps.Length)];
		GameObject powerUpSpawned = Instantiate (powerUp, spawnPosition, Quaternion.identity) as GameObject;
	}
}
