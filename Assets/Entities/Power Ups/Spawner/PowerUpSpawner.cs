using UnityEngine;
using System.Collections;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject[] healthUps, shieldUps, laserModes;

    public float powerUpFallSpeed = 10.0f;
    public int healthChance, shieldChance, laserChance, powerUpChance;

    private int spawnChecksum;

    private void Start()
    {
        spawnChecksum += healthChance + shieldChance + laserChance;
    }

    public void OnEnemyDestroyed(Vector2 spawnPosition)
    {
        if (RollChance(100) <= powerUpChance)
        {
            Debug.Log("Power Up Chance = " + powerUpChance);
            GameObject[] spawnArray = DetermineArray(healthChance, shieldChance, laserChance);
            Debug.Log("Health chance = " + healthChance);
            Debug.Log("Shield chance = " + shieldChance);
            Debug.Log("Laser chance = " + laserChance);
            GameObject powerUp = Instantiate(DeterminePowerUp(spawnArray), spawnPosition, Quaternion.identity) as GameObject;
            powerUp.GetComponent<Rigidbody2D>().velocity = Vector3.down * powerUpFallSpeed;
        }
    }

    private GameObject[] DetermineArray(int healthChance, int shieldChance, int laserChance)
    {   
        int rollResult = RollChance(spawnChecksum);
        Debug.Log("Roll result = " + rollResult);
        if (rollResult <= healthChance) { return healthUps; }
        else if (rollResult <= healthChance+shieldChance) { return shieldUps; }
        else if (rollResult <= spawnChecksum) { return laserModes; }
        else { Debug.LogError("Null returned in GameObject[] DetermineArray()"); return null; }
    }

    private GameObject DeterminePowerUp (GameObject[] array)
    {
        int powerUpChecksum = 0;
        foreach (GameObject powerUp in array)
        {
            powerUpChecksum += powerUp.GetComponent<PowerUp>().chanceToDrop;
        }
        int rollResult = RollChance(powerUpChecksum);
        int sum = 0;
        foreach (GameObject powerUp in array)
        {
            sum += powerUp.GetComponent<PowerUp>().chanceToDrop;
            if (rollResult <= sum) { Debug.Log(powerUp.name); return powerUp; }
        }
        return null;
    }

    private int RollChance (int chanceMax)
    {
        return Random.Range(1, chanceMax);
    }
}
