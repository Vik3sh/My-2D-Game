using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("Player ")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private float respawnDelay;



    public player player;


    [Header("Fruits Management")]
    public bool friutsHaveRandomLook;
    public int totalFruits;
    [Header("CheckPoints")]
    public bool canReactivate;

    public int fruitCollected;
    public fruit[] allFruits;


    [Header("Traps")]
    public GameObject arrowPrefab;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy (Instance); 
        }
        
    }
    private void Start()
    {
        CollectFruitInfo();
    }

    private void CollectFruitInfo()
    {
        allFruits = FindObjectsByType<fruit>(FindObjectsSortMode.None);
        totalFruits = allFruits.Length;   
    }

    public void UpdateRespawnPosition(Transform newRespawnPoint) => respawnPoint = newRespawnPoint;
    

    private IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds (respawnDelay);
        GameObject newPlayer = Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
        player = newPlayer.GetComponent<player>();
    }
    public void respawnLayer()=> StartCoroutine(RespawnCoroutine());
    public void AddFruit() => fruitCollected++;

    public bool FruitsHaveRandomLook() => friutsHaveRandomLook;

    public void createObject(GameObject prefab,Transform target, float  delay = 0)
    {
        StartCoroutine(createObjectCoroutine(prefab, target, delay));
    }
    private IEnumerator createObjectCoroutine(GameObject prefab,Transform target, float delay)
    {
        Vector3 newPosition=target.position;
        yield return new WaitForSeconds(delay);
        GameObject newObject=Instantiate(prefab,newPosition,Quaternion.identity);
    }
    
}
