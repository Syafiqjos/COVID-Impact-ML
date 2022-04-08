using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling1 : MonoBehaviour
{
    public int currentEnemyIndex = 0; // Menyimpan index enemy saat ini
    public List<GameObject> enemies; // Kumpulan object enemy

    private void Awake()
    {
        // Disable seluruh enemy di awal game dimulai
        foreach (GameObject enemy in enemies) {
            enemy.SetActive(false);
        }
    }

    private void Update()
    {
        // Spawn enemy ketika menekan tombol spasi
        if (Input.GetKeyDown(KeyCode.Space)) {
            SpawnEnemy();
        }
    }

    public void SpawnEnemy() {
        // Jika ada enemy yang belum di spawn maka spawn 1 kali,
        // hingga seluruh enemy ter-spawn seluruhnya
        if (currentEnemyIndex < enemies.Count) {
            enemies[currentEnemyIndex].SetActive(true);
            currentEnemyIndex++;
        }
    }
}
