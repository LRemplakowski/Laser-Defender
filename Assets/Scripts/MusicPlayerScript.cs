using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicPlayerScript : MonoBehaviour {

	static MusicPlayerScript Instance = null;

	public AudioClip startClip, gameClip, endClip;

	private AudioSource music;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Awake () {	
		if (Instance != null && Instance != this) {
			Destroy (gameObject);
			print ("Duplicate Music Player self-destructing!");
		} else {
			Instance = this;
			GameObject.DontDestroyOnLoad (gameObject);
			music = GetComponent<AudioSource>();
			music.clip = startClip;
			music.Play ();
		}
	}

	void OnSceneLoaded (Scene scene, LoadSceneMode mode) {
		Debug.Log ("MusicPlayer: loaded level " + scene.buildIndex);

		music.Stop ();

		if (scene.buildIndex == 0) {music.clip = startClip;}
		if (scene.buildIndex == 1) {music.clip = gameClip;}
		if (scene.buildIndex == 2) {music.clip = endClip;}

		music.loop = true;
		music.Play ();
	}
}
