using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject fireBall;

    [HideInInspector]
    public float timeToSpawn = 1f;

    float minSpawnTimew = 0.5f;

    [SerializeField]
    Transform world;
    int c;

    public static Spawner spawner;
    [HideInInspector] public bool canSpawn = true;
    void Start()
    {
        if (spawner != null) Destroy(spawner);
        else spawner = this;
    }

    public void StartSpawning()
    {
        canSpawn = true;
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while(canSpawn)
        {
            var randomPoint = Random.onUnitSphere * (world.localScale.x * 1.2f) ;

            var obj = Instantiate(fireBall, randomPoint, Quaternion.identity);
            c++;

            if (c % 10 == 0 && timeToSpawn > minSpawnTimew && !GameManager.gameManager.isPaused) timeToSpawn -= 0.1f ;

            yield return new WaitForSeconds(timeToSpawn);

        }
        
    }

    public void StopSpawn()
    {
        canSpawn = false;
    }

    public void Reset()
    {
        timeToSpawn = 1;

    }

}
