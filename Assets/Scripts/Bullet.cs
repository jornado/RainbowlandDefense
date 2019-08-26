using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 70f;
    public int damage = 50;
    public float explosionRadius;
    public GameObject impactEffect;

    private string enemyTag = "Enemy";

    /* Update the enemy the bullet is heading towards */
    public void Seek(Transform _target)
    {
        target = _target;
    }

    /* Move the bullet towards the target/hit the target each frame */
    void Update()
    {
        // if there's no target, get rid of the bullet
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // If the length of the direction vector is less than the distance
        // we are moving this frame, then we hit the target
        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        // Move the bullet and rotate it towards the target
        transform.Translate(
            direction.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    /* Hit the enemy and make a particle effect */
    void HitTarget()
    {
        // Make a bullet particle effect
        GameObject effectIns = (GameObject)Instantiate(
            impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 5f);

        // Do explosion particle effect, if specified
        if (explosionRadius > 0)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        // remove bullet from game
        Destroy(gameObject);
    }

    /* Hit all enemies within radius */
    void Explode()
    {
        // Get an array of all objects collided with within a radius
        Collider[] hitObjects = Physics.OverlapSphere(
            transform.position, explosionRadius);
        foreach (var hitObject in hitObjects)
        {
            // Only damage enemy objects
            if (hitObject.tag == enemyTag)
            {
                Damage(hitObject.transform);
            }
        }
    }

    /* Damage/destroy one enemy */
    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
        {
            e.TakeDamage(damage);
        }
    }

    /* Show the range wireframe of the explosion */
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
