using System.Collections;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] GameObject[] spawnPrefab;
    [SerializeField] GameObject onibusObstacle;
    [SerializeField] GameObject leftIndicator;
    [SerializeField] GameObject rightIndicator;
    [SerializeField] float minThreshold = -8f;
    [SerializeField] float maxThreshold = 8f;
    public float secondSpawn = 2f;
    public float chanceTwoObjects = 0.3f;
    public float chanceThreeObjects = 0.1f;
    public float chanceOnibusObstacle = 0.1f;  // Chance inicial de 10% para spawn do onibus

    void Start()
    {
        StartCoroutine(ObjectSpawn());
    }

    IEnumerator ObjectSpawn()
    {
        while (true)
        {
            // Decide se deve spawnar o ônibus ou objetos normais
            if (Random.value < chanceOnibusObstacle)
            {
                SpawnOnibusObstacle();
                yield return new WaitForSeconds(3f);
            }
            else
            {
                // Spawn padrão de objetos
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
            }

            yield return new WaitForSeconds(secondSpawn);
        }
    }

    // Função para spawnar o ônibus e os indicadores
    void SpawnOnibusObstacle()
    {
        int spawnSide = Random.Range(0, 2); // 0 para esquerda, 1 para direita
        Vector3 spawnPosition = new Vector3(spawnSide == 0 ? -4f : 4f, -28.5f, transform.position.z);

        GameObject indicator = spawnSide == 0 ? leftIndicator : rightIndicator;
        indicator.SetActive(true);  // Ativa o indicador para o lado sorteado

        StartCoroutine(SpawnOnibusAfterDelay(indicator, spawnPosition));
    }

    // Função que cuida do delay para o spawn do ônibus
    IEnumerator SpawnOnibusAfterDelay(GameObject indicator, Vector3 spawnPosition)
    {
        yield return new WaitForSeconds(3f);  // Delay de 3 segundos antes do spawn do ônibus
        indicator.SetActive(false);  // Desativa o indicador

        // Spawn do ônibus com rotação fixa de 0
        GameObject onibus = Instantiate(onibusObstacle, spawnPosition, Quaternion.identity);
        Destroy(onibus, 5.5f);  // Destrói o ônibus após 5.5 segundos
    }
}
