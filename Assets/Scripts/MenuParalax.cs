using UnityEngine;

public class MenuParalax : MonoBehaviour
{
   public float offsetMultiplier=1f;
   public float smoothTime=.3f;
   private Vector2 startPostion;
   private Vector3 velocity;
    void Start()
    {
        startPostion=transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset=Camera.main.ScreenToViewportPoint(Input.mousePosition);
        transform.position=Vector3.SmoothDamp(transform.position,startPostion+(offset*offsetMultiplier),ref velocity,smoothTime);
    }
}
