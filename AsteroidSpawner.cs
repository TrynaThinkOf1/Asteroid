using UnityEngine;
using System.Collections;
using System.Threading.Tasks;

public class AsteroidSpawner : MonoBehaviour
{
    public float spawnRate = 2.0f;
    public int spawnAmount = 1;
    public ASteroid asteroidPrefab;
    public float spawnDistance = 15;
    public float trajectoryVariance = 15.0f;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
        Invoke(nameof(RaiseAmount), 15.0f);
        Invoke(nameof(RaiseAmount), 20.0f);
        Invoke(nameof(RaiseAmount), 25.0f);
    }
    private void Spawn()
    {
        for (int i =0; i < spawnAmount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
            Vector3 spawnPoint = this.transform.position + spawnDirection;

            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            ASteroid asteroid = Instantiate(this.asteroidPrefab, spawnPoint, rotation);
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);
            asteroid.SetTrajectory(rotation * -spawnDirection);
        }
    }
    private void RaiseAmount()
    {
        this.spawnAmount += 1;
        this.spawnRate += 0.25f;
    }
}
