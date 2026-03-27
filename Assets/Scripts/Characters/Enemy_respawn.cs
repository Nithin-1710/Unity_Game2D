using Unity.VisualScripting;
using UnityEngine;

public class Enemy_respawn : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform[] respawnPoints;
    [SerializeField] private float cooldown=2f;
    [Space]
    [SerializeField] private float cooldownRate=0.05f;
    [SerializeField] private float cooldownCap=1f;
    private float timer;
    private Transform player;
    private Transform charToProtect;
    [SerializeField] private int maxSpawn=15;
    public int MaxSpawn=>maxSpawn;
    [SerializeField]private int enemiesSpawned=0;
    private bool canSpawn=false;
    [SerializeField]private SpriteRenderer portal;
    private void Awake()
    {
        player=FindFirstObjectByType<PlayerScript>().transform;
        charToProtect=FindFirstObjectByType<charToProtect>().transform;
    }
    private void Update()
    {
        StartRespawn();
        if(!canSpawn)
            return;
        if(enemiesSpawned>=maxSpawn)
            return;
        timer-=Time.deltaTime;
        if(timer<=0)
        {
            timer =cooldown;
            CreateNewEnemy();
            cooldown=Mathf.Max(cooldownCap,cooldown-cooldownRate);
        }
    }
    private void StartRespawn()
    {
        if (player.position.x > charToProtect.position.x)
        {
            canSpawn=true;
            portal.enabled=true;
        }
    }
    private void CreateNewEnemy()
    {
        int respawnPointIndex = Random.Range(0, respawnPoints.Length);
        Vector3 spawnPoint=respawnPoints[respawnPointIndex].position;
        GameObject newEnemy=Instantiate(prefab,spawnPoint,Quaternion.identity);
        bool createdOnRight=newEnemy.transform.position.x>player.transform.position.x;
        if (createdOnRight)
        {
            newEnemy.GetComponent<EnemyScript>().Flip();
        }
        enemiesSpawned++;
    }
}
