using UnityEngine;

public class camera : MonoBehaviour
{
    [Header("追蹤物件")]
    public Transform target;
    [Header("追蹤速度"), Range(0f, 100f)]
    public float speed = 1;

    private void LateUpdate()
    {
        CameraTracker(); 
    }

    private void CameraTracker()
    {
        Vector3 pos1 = target.position;
        Vector3 pos2 = transform.position;
        pos1.z = -10;

        pos2 = Vector3.Lerp(pos2, pos1, 0.5f * speed * Time.deltaTime);
        transform.position = pos2;
    }
}
