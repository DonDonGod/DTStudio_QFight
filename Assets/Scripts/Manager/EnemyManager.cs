using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> enemy_pool;
    public GameObject enemy;//手动加载
    private int create_time;

    void Start()
    {
        enemy_pool = new List<GameObject>();
        create_time = 5;

        StartCoroutine(CountTime());
    }

    void FixedUpdate()
    {
        
    }

    void OnDestroy()
    {
        StopAllCoroutines();
    }

    IEnumerator CountTime()
    {
        while(true)
        {
            if (create_time <= 0)
            {
                create_time = 5;
                CreateEnemy();
            }
            yield return new WaitForSeconds(1);
            create_time--;
        }
    }

    void CreateEnemy()
    {
        GameObject new_enemy = Instantiate(enemy, this.transform.position, Quaternion.identity,this.transform);
        enemy_pool.Add(new_enemy);
    }

    public void DeleteEnemy(GameObject obj)
    {
        enemy_pool.Remove(obj);
        Destroy(obj);
    }
}
