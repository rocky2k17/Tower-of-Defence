   
using UnityEngine;
using System.Collections;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;

    public Transform spawnPoint;

    public TextMeshProUGUI WaveCountDownText;

    public float timeBetweenWaves = 5f;

    private float countdown = 2f;

    private int waveIndex =0 ; 

    void Update()
    {
        if(countdown <= 0f)
        {
            StartCoroutine(SpawnWave()); 
            countdown = timeBetweenWaves ; 
        }

        countdown -= Time.deltaTime ;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        WaveCountDownText.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;
        PlayerStat.Rounds++;
        
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(.5f);
        }
        waveIndex++;
    } 

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position , spawnPoint.rotation);
    }
}
