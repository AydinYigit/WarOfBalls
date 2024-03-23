using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject powerUpGem;
    public GameObject Player;
    [SerializeField] private int initPoolSize;
    private int spawnRange = 9;
    [SerializeField] private PooledObject objectToPool;

    private bool isGameOver;
    // store the pooled objects in a collection
    private Stack<PooledObject> stack;
    private int levelDifficulty = 1;
    
    void Start()
    {
        SetupPool();
        
    }
    
    void Update()
    {
        isGameOver = Player.transform.position.y < -2;
        // when player win against enemies
        if (stack.Count == initPoolSize && !isGameOver)
        {
            StartCoroutine(nameof(NextWaveRoutine));
        }
        
    }
    IEnumerator NextWaveRoutine()
    {
        
        for (int i = 0; i < levelDifficulty; i++)
        {
            GetPooledObject();
        }
        
        powerUpGem.gameObject.SetActive(true);
        levelDifficulty++;
       
        yield break;

    }
    
    // creates the pool (invoke when the lag is not noticeable)
    public void SetupPool()
    {
        stack = new Stack<PooledObject>();
        PooledObject instance = null;
        for (int i = 0; i < initPoolSize; i++)
        {
            instance = Instantiate(objectToPool);
            instance.Pool = this;
            instance.gameObject.SetActive(false);
            stack.Push(instance);
        }
    }

    public  void GetPooledObject()
    {
        //just grab the next one from the list
        PooledObject nextInstance = stack.Pop();
        nextInstance.transform.position = GenerateSpawnPosition();
        nextInstance.gameObject.SetActive(true);
    }

    public void ReturnToPool(PooledObject pooledObject)
    {
        stack.Push(pooledObject);
        pooledObject.gameObject.SetActive(false);
    }
    Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0.1f, spawnPosZ);
        return randomPos;
    }
}



