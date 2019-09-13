using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int waypointIndex;

    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        // Start with the first waypoint
        target = Waypoints.points[0];
    }

    float GetWaveSpeed(float currentBaseSpeed)
    {
        int wave = WaveSpawner.instance.GetCurrentWave();

        if (wave >= 5)
        {
            return currentBaseSpeed * 1.2f;
        }
        else if (wave >= 10)
        {
            return currentBaseSpeed * 1.6f;
        }
        else if (wave >= 15)
        {
            return currentBaseSpeed * 2.0f;
        }
        else if (wave >= 20)
        {
            return currentBaseSpeed * 2.5f;
        }
        else if (wave >= 30)
        {
            return currentBaseSpeed * 5f;
        }
        else if (wave >= 40)
        {
            return currentBaseSpeed * 7f;
        }
        else if (wave >= 50)
        {
            return currentBaseSpeed * 10f;
        }
        else if (wave >= 60)
        {
            return currentBaseSpeed * 20f;
        }
        else
        {
            return currentBaseSpeed;
        }
    }

    /* Navigate to each waypoint */
    private void Update()
    {
        // move toward waypoint
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * enemy.speed * Time.deltaTime, Space.World);

        // if enemy has arrived at about waypoint location, move to next waypoint
        if (Vector3.Distance(transform.position, target.position) < 0.4f)
        {
            GetNextWaypoint();
        }

        // reset enemy speed in case it is out of range of laser
        enemy.speed = GetWaveSpeed(enemy.startSpeed);
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
        if (PlayerStats.Lives > 0)
        {
            PlayerStats.Lives--;
        }
        Destroy(gameObject);
    }
}
