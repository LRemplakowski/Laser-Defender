using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	public float timeToDestroy;

    private void Awake()
    {
        if (!SoundControllerScript.soundsMute)
        {
            GetComponent<AudioSource>().Play();
        }
    }
    // Use this for initialization
    void Start () {
		Destroy (gameObject, timeToDestroy);
	}
}
