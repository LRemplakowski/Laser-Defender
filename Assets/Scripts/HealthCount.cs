using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCount : MonoBehaviour {

    public Text healthCount;

    PlayerController playerController;

	// Use this for initialization
	void Start () {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        healthCount = this.gameObject.GetComponent<Text>();
        healthCount.text = "Health: " + playerController.playerHealth.ToString() + "/" + playerController.playerMaxHealth.ToString();
	}
	
	// Update is called once per frame
	public void HealthCountUpdate ()
    {
        healthCount.text = "Health: " + playerController.playerHealth.ToString() + "/" + playerController.playerMaxHealth.ToString();
    }
}
