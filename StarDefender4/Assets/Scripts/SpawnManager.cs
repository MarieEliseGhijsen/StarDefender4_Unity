using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class SpawnManager : MonoBehaviour
{
    public GameObject BE_Prefab;
    public GameObject CE_Prefab;
    public GameObject FE_Prefab;
    public int waveNumber = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnEnemyWave();
    }

    // Update is called once per frame
    void Update()
    {
        if(!(GameObject.FindWithTag("Block") || GameObject.FindWithTag("Circ") || GameObject.FindWithTag("Free")))
        {
            waveNumber = waveNumber + 1;
            SpawnEnemyWave();
        }
    }

    void SpawnEnemyWave()
    {
        if (waveNumber == 1)
        {
            for (int i = 0; i < 8; i++) //row - 8 enemies between -10 and 10
            {
                for (int j = 1; j <= 2; j++) //col - 2 enemies between 4 and 6
                {
                    Instantiate(BE_Prefab, new Vector3((2 + i * 2) - 10, 0.25f, (2 + j * 2)), BE_Prefab.transform.rotation);
                }
            }
        }
        else if (waveNumber == 2)
        {
            for (int i = 0; i < 6; i++)
            {
                Instantiate(CE_Prefab, new Vector3(0.0f, 0.25f, 0.0f), CE_Prefab.transform.rotation);
            }
        }
        else if (waveNumber == 3)
        {
            for (int i = 0; i < 6; i++)
            {
                Instantiate(FE_Prefab, new Vector3(Random.Range(-8,8), 0.25f, Random.Range(-4, 4)), FE_Prefab.transform.rotation);
            }
        }
        else
        {
            waveNumber = 1;
            SpawnEnemyWave();
        }
    }
}