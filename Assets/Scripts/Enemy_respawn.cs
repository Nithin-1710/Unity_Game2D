using Unity.VisualScripting;
using UnityEngine;

public class Enemy_respawn : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform[] respawnPoints;
    [SerializeField] private float cooldown=2f;
    [Space]
    [SerializeField] private float cooldownRate=0.05f;
    [SerializeField] private float cooldownCap=.7f;
    private float timer;
    private Transform player;
    private void Awake()
    {
        player=FindFirstObjectByType<PlayerScript>().transform;
    }
    private void Update()
    {
        timer-=Time.deltaTime;
        if(timer<=0)
        {
            timer =cooldown;
            CreateNewEnemy();
            cooldown=Mathf.Max(cooldownCap,cooldown-cooldownRate);
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
    }
}
