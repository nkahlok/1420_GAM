using UnityEngine;

public class FollowPlayerCam : MonoBehaviour
{
    public PlayerCam MainCamera;

    private void FixedUpdate()
    {
        transform.position = MainCamera.transform.position;
    }
}
