using UnityEngine;
using Cinemachine;
using System.Collections;

public class FP_CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public GameObject cow;
    public GameObject player;
    private bool cow_showed = false;

    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        cow = GameObject.FindGameObjectWithTag("Cow");
        virtualCamera.Follow = player.transform;
    }

    void Update()
    {
        if (!cow_showed)
        {
            virtualCamera.Follow = cow.transform;
            StartCoroutine(DisplayPlayer());
        }
        cow_showed = true;
    }

    IEnumerator DisplayPlayer()
    {
        yield return new WaitForSeconds(3f);
        virtualCamera.Follow = player.transform;
    }

}
