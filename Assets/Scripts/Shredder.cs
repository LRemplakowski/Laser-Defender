using UnityEngine;
using System.Collections;

public class Shredder : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D collider)
    {
        if (collider.gameObject.GetComponent<Projectile>())
        {
            collider.gameObject.SetActive(false);
        } else
        {
            Destroy(collider.gameObject);
        }
	}
}
