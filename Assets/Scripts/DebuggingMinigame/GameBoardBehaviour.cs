using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GameBoardBehaviour : MonoBehaviour
{

    public Transform playerTransform;

    public Transform handyTransform;

    public float distanceBetweenComponents;

    public Transform upperBound;
    public Transform lowerBound;
    public Transform leftBound;
    public Transform rightBound;

    private float _gameBoardHeight;
    private float _gameBoardWidth;

    private Random _rand;

    public GameObject enemyPrefab;


    // Start is called before the first frame update
    void Start()
    {
        _rand = new Random();
        Initialize();
        StartSpawnCoroutine();
    }

    private void Initialize()
    {
        _gameBoardHeight = gameObject.GetComponent<MeshRenderer>().bounds.size.y;
        _gameBoardWidth = gameObject.GetComponent<MeshRenderer>().bounds.size.x;

        
        transform.position = handyTransform.position + new Vector3(0, 0.004f, 0);
        playerTransform.position = playerTransform.position + new Vector3(0,distanceBetweenComponents,0);
        
        Vector3 gameBoardPos = transform.position;

        //upperBound.position = gameBoardPos + new Vector3(-_gameBoardHeight, 0, 0);
        //lowerBound.position = gameBoardPos + new Vector3(_gameBoardHeight, 0, 0);
        //leftBound.position = gameBoardPos + new Vector3(0, 0, _gameBoardWidth);
        //rightBound.position = gameBoardPos + new Vector3(0, 0, -_gameBoardWidth);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void StartSpawnCoroutine()
    {
        StartCoroutine(WaitForSpawnEnemy());
    }

    private IEnumerator WaitForSpawnEnemy()
    {
        yield return new WaitForSeconds(3);
        SpawnEnemy();
        StartSpawnCoroutine();
    }


    private void SpawnEnemy()
    {
        Vector3 gameBoardPos = transform.position;
        float randX = UnityEngine.Random.Range(gameBoardPos.x - 0.1f, gameBoardPos.x + 0.1f);
        float randZ = UnityEngine.Random.Range(gameBoardPos.z - 0.1f, gameBoardPos.z + 0.1f);;
        Debug.Log("Height: " + _gameBoardHeight);
        Debug.Log("Width: " + _gameBoardWidth);
        Debug.Log("X: " + randX);
        Debug.Log("Z: " + randZ);

        Instantiate(enemyPrefab, new Vector3((float) randX, playerTransform.position.y, (float) randZ), Quaternion.identity);
    }
}
