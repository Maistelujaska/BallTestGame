using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraPosition;
    public GameObject player;
    private Vector3 offset;
    
    void Start()
    {
        offset = transform.position - player.transform.position;
    }
    
    
    void Update()
    {
        transform.position = cameraPosition.position;
    }

}
