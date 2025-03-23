using System.Collections;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] GameObject[] spawnPrefab;
    [SerializeField] float minThreshold = -8f;
    [SerializeField] float maxThreshold = 8f;
    public float secondSpawn = 2f;
    public float chanceTwoObjects = 0.3f;
    public float chanceThreeObjects = 0.1f;

    void Start()
    {
        StartCoroutine(ObjectSpawn());
    }

    IEnumerator ObjectSpawn()
    {
        while (true)
        {
            int spawnCount = 1;
            float roll = Random.value;

            if (roll < chanceThreeObjects)
                spawnCount = 3;
            else if (roll < chanceTwoObjects)
                spawnCount = 2;

            for (int i = 0; i < spawnCount; i++)
            {
                float randomX = Random.Range(minThreshold, maxThreshold);
                Vector3 position = new Vector3(randomX, transform.position.y, transform.position.z);
                Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));

                int index = Random.Range(0, spawnPrefab.Length);
                GameObject obj = Instantiate(spawnPrefab[index], position, randomRotation);

                ScaleSettings scaleSettings = obj.GetComponent<ScaleSettings>();
                if (scaleSettings != null)
                {
                    float randomScale = Random.Range(scaleSettings.minScale, scaleSettings.maxScale);
                    obj.transform.localScale = Vector3.one * randomScale;
                }

                yield return new WaitForSeconds(Random.Range(0f, 1.5f));
                Destroy(obj, 5.5f);
            }

            yield return new WaitForSeconds(secondSpawn);
        }
    }
}
