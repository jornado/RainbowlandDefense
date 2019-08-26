using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;

    private Transform target;
    private int waypointIndex;

    public int health = 100;
    public int monetaryValue = 50;

    public GameObject deathEffect;

    private void Start()
    {
        // Start with the first waypoint
        target = Waypoints.points[0];
    }

    /* Navigate to each waypoint */
    private void Update()
    {
        // move toward waypoint
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        // if enemy has arrived at about waypoint location, move to next waypoint
        if (Vector3.Distance(transform.position, target.position) < 0.4f)
        {
            GetNextWaypoint();
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        PlayerStats.Money += monetaryValue;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(effect, 5f);

        Destroy(gameObject);
    }

    /* Update destination to next waypoint target */
    void GetNextWaypoint()
    {
        // Remove enemy when it reaches the END 
        if (waypointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }
        waypointIndex++;
        target = Waypoints.points[waypointIndex];
    }

    void EndPath()
    {
        PlayerStats.Lives --;
        Destroy(gameObject);
    }
}
