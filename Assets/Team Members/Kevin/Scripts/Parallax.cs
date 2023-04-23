using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject cam;
    [SerializeField] private float parallaxEffect;
    public float startPos;

    void Start()
    {
        startPos = transform.position.x;
        cam = GameManager.Instance.cameraTracker.gameObject;
    }

    void Update()
    {
        if (cam == null) return;
        float distance = (cam.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);
    }
}
