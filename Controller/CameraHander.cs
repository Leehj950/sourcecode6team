using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHander : MonoBehaviour
{
    [SerializeField] Transform playerTransfom;
    private new Camera camera;
    void Awake()
    {
        camera = GetComponentInChildren<Camera>();
    }

    private void FixedUpdate()
    {
        CameraPos();
    }

    private void CameraPos()
    {
        camera.transform.position = new Vector3(playerTransfom.position.x,playerTransfom.position.y, camera.transform.position.z);
    }
}
