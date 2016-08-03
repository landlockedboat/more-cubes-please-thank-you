using UnityEngine;
using System.Collections;

public class CubeSpawner : MonoBehaviour {
    [SerializeField]
    float spawnWidth = 5f;
    [SerializeField]
    float spawnHeight = 5f;
    [SerializeField]
    GameObject cubePrefab;
    [SerializeField]
    float timeBetweenCubes = .25f;

	void Start () {
        StartCoroutine("SpawnCubes");
	}
	
    IEnumerator SpawnCubes() {
        while (true)
        {
            
            Vector3 randomSpawnPos = new Vector3(Random.Range(-spawnWidth / 2, spawnWidth / 2),
                transform.position.y, Random.Range(-spawnHeight / 2, spawnHeight / 2));
            GameObject cube = 
            Instantiate(cubePrefab, randomSpawnPos, Random.rotation) as GameObject;
            cube.GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0,1,1,1,1,1);
            yield return new WaitForSeconds(timeBetweenCubes);
        }
    }

}
