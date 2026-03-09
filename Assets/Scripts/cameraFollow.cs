using Unity.VisualScripting;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    private Vector3 offset=new Vector3(0f,0f,-10f);
    private float smoothTime=0.25f;
    private Vector3 velocity=Vector3.zero;
    [SerializeField]private Transform target;
    [Header("Camera Settings")]
    [SerializeField] private float groundLevel = 0f;
    private float camHalfHeight;
    void Start()
    {
        camHalfHeight = Camera.main.orthographicSize;
    }
    void Update()
    {
        Vector3 targetPosition=target.position+offset;
        // float minY = groundLevel + camHalfHeight;
        targetPosition.y = Mathf.Max(targetPosition.y,groundLevel);
        transform.position=Vector3.SmoothDamp(transform.position,targetPosition,ref velocity,smoothTime);
    }
}
