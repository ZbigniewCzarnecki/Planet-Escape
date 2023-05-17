using Cinemachine;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    CinemachineVirtualCamera _virtualCamera;

    private void Awake()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _virtualCamera.Follow = Player.Instance.transform;
    }
}
