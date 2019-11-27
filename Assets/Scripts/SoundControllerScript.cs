using UnityEngine;
using System.Collections;

public class SoundControllerScript : MonoBehaviour {

	public static bool soundsMute = false;		
	private MusicPlayerScript musicPlayer;
	
	void Start () {
		musicPlayer = GameObject.FindObjectOfType<MusicPlayerScript>();
	}
		
	public void MusicToggle () {
		AudioSource musicSource = musicPlayer.GetComponent<AudioSource>();
		musicSource.mute = !musicSource.mute;
		Debug.Log ("Music toggled!");
	}
	
	public void SoundToggle () {
		soundsMute = !soundsMute;
		Debug.Log ("Sound toggled!");
	}
}
