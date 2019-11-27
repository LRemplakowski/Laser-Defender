using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public GameObject explosion;

	public float damage;
    public float projectileSpeed;
    [Range (0.0f,1.0f)]
    public float fireRate;
    public float explosionRadius;
    public bool isDual, isHoming, isExplosive;

    private Vector3 targetPosition;
    private GameObject explode;
    private string targetTag;

    private void OnEnable()
    {
        if (gameObject.layer == 9 && !isHoming)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.up * projectileSpeed;
        } else if (gameObject.layer == 11 && !isHoming)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.down * projectileSpeed;
        } else if (isHoming)
        {
            if (gameObject.tag == "Enemy Torpedo") { targetTag = "Player"; targetPosition = GetTargetPosition(targetTag); }
            else if (gameObject.tag == "Friendly Torpedo") { targetTag = "Enemy"; targetPosition = GetTargetPosition(targetTag); }
        }
    }

    private void Update()
    {
        if (isHoming)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, projectileSpeed/50);
            if (transform.position == targetPosition)
            {
                if (isExplosive) { GoBoom(explosionRadius); }
                else { Hit(); }
            }
        }
    }

    private void OnDisable()
    {
        explode = null;
    }

    public float GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        if (isExplosive) { GoBoom(explosionRadius); }
        gameObject.SetActive(false);
    }

    private void GoBoom(float radius)
    {
        CircleCollider2D boom = gameObject.GetComponent<CircleCollider2D>();
        boom.radius = explosionRadius;
        if (explode == null) { explode = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject; }
        StartCoroutine(ObjectActive(false, 0.3f));
    }

    public static Vector3 GetTargetPosition(string tag)
    {
        if (tag != null)
        {
            GameObject target = GameObject.FindGameObjectWithTag(tag);
            return target.transform.position;
        }
        else return new Vector3(0,0,0);
    }

    IEnumerator ObjectActive(bool status, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        gameObject.SetActive(status);
    }
}
