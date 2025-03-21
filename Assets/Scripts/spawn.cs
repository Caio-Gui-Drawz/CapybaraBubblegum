using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class spawn : MonoBehaviour
{
    [SerializeField] GameObject[] spawnPrefab;
    [SerializeField] float secondSpawn = 0.5f;
    [SerializeField] float minThreshold;
    [SerializeField] float maxThreshold;

    void Start()
    {
        StartCoroutine(ObjectSpawn());
    }

    IEnumerator ObjectSpawn()
    {
        while (true)
        {
            // Especifica explicitamente UnityEngine.Random para evitar ambiguidade
            float wanted = UnityEngine.Random.Range(minThreshold, maxThreshold);
            Vector3 position = new Vector3(wanted, transform.position.y, transform.position.z);
            
            // Corrigido aqui também
            int index = UnityEngine.Random.Range(0, spawnPrefab.Length);
            GameObject gameObject = Instantiate(spawnPrefab[index], position, Quaternion.identity);
            
            yield return new WaitForSeconds(secondSpawn);
            Destroy(gameObject, 5f);
        }
    }
}