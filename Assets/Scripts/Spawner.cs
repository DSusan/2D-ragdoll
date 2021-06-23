using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float respawnTime = 6.0f;
    private Vector2 screenBounds;
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Debug.Log(screenBounds);
        StartCoroutine(EnemyWave());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnEnemy()
    {
        GameObject i = Instantiate(enemyPrefab) as GameObject;
        i.transform.position = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), screenBounds.y-1);
    }

    IEnumerator EnemyWave()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(respawnTime);
            SpawnEnemy();
        }
    }
}
