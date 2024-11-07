using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    [SerializeField] Enemy knightPrefab;
    [SerializeField] Enemy bearPrefab;
    [SerializeField] Enemy whiteSnowPrefab;
    [SerializeField] Enemy batmanPrefab;

    private Enemy _enemy;

    // Create a List "enemyList" of type "Enemy"
    public List<Enemy> enemyList;

    // Create function "SetUpEnemyManager()"
    public void SetUpEnemyManager() {
        Debug.Log("SetUp the \"EnemyManager\""); // DEBUG
        // Initialize the "enemyList"
        Debug.Log("Initialize the \"enemyList\""); // DEBUG
        enemyList = new List<Enemy> { knightPrefab, bearPrefab, whiteSnowPrefab, batmanPrefab };
    }

    public void CreateEnemy(Enemy enemy) {
        _enemy = selectedEnemyPrefab();
        _enemy = Instantiate(_enemy);
        _enemy.transform.position = new Vector3(0, 0, 0);
        enemy = _enemy;

        /*
        Enemy randomEnemyInstance = selectedEnemyPrefab();
        randomEnemyInstance = Instantiate(randomEnemyInstance, transform);
        randomEnemyInstance.transform.position = new Vector3(0, 0, 0);
        */
        
        /*
        enemyInstance = selectedEnemyPrefab();
        
        if (enemyInstance == knightPrefab) {
            knight = Instantiate(knightPrefab, transform);
            knight.transform.position = new Vector3(0, 0, 0);
        }
        else if (enemyInstance == bearPrefab) {
            bear = Instantiate(bearPrefab, transform);
            bear.transform.position = new Vector3(0, 0, 0);
        }
        else if (enemyInstance == whiteSnowPrefab) {
            whiteSnow = Instantiate(whiteSnowPrefab, transform);
            whiteSnow.transform.position = new Vector3(0, 0, 0);
        }
        else if (enemyInstance == batmanPrefab) {
            batman = Instantiate(batmanPrefab, transform);
            batman.transform.position = new Vector3(0, 0, 0);
        }
        */
    }

    // Method that return a random enemy prefab
    Enemy selectedEnemyPrefab() {
        int randomValue = Random.Range(0, 100 + 1);
        Enemy randomEnemy;

        if (randomValue <= 30) { // 30%
            return randomEnemy = knightPrefab;
        }
        else if (randomValue <= 60) { // 30%
            return randomEnemy = bearPrefab;
        }
        else if (randomValue <= 85) { // 25%
            return randomEnemy = whiteSnowPrefab;
        }
        else if (randomValue <= 100) { // 15%
            return randomEnemy = batmanPrefab;
        }
        else {
            Debug.LogWarning("Error on the \"RandomEnemy()\" at the script \"Enemy\"");
            return null;
        }
    }

}