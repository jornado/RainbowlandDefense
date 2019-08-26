using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // enemy to shoot at
    private Transform target;

    [Header("General")]
    // range of turret fire
    public float range = 15f;

    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;
    // bullets per second
    public float fireRate = 1f;
    private float fireCountdown;

    [Header("Use Laser")]
    public bool useLaser;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public Transform partToRotate;
    // rotation speed of turret
    public float turnSpeed = 10f;
    
    // muzzle of turret, where bullet comes out
    public Transform firePoint;

    /* Start looking for a target */
    void Start()
    {
        // look for a target every 0.5 seconds
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    /* Find the closest enemy to turret */
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        // Loop through all enemies, and find the closest distance to an enemy
        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        // Found the closest enemy. Set as target!
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        } else
        {
            target = null;
        }
    }

    /* Fire at our target each frame, if cooled down. */
    void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                // disable laser beam if it's active without a target
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
            }

            return;
        }
            
        // rotate to face enemry
        LockOnTarget();

        if (useLaser)
        {
            Laser();
        } else
        {
            // fire when ready!
            if (fireCountdown <= 0f)
            {
                Shoot();
                // How many bullets per second
                fireCountdown = 1f / fireRate;
            }
        }

        // countdown every second
        fireCountdown -= Time.deltaTime;
    }

    /* Make a laser beam shoot from turret to enemy */
    void Laser()
    {
        // enable the beam and play the particle effect
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }

        // render the beam
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        // rotate particle effect away from target 
        Vector3 direction = firePoint.position - target.position;
        impactEffect.transform.rotation = Quaternion.LookRotation(direction);
        // offset particle effect to edge of enemy
        float distanceToTargetEdge = 1f;
        impactEffect.transform.position = target.position
            + direction.normalized * distanceToTargetEdge;
    }

    /* Instatiate a bullet and head towards the target */
    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(target);
    }

    /* Turn the barrel of the turret to face the enemy */
    void LockOnTarget()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    /* Show the range wireframe of the turret */
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
