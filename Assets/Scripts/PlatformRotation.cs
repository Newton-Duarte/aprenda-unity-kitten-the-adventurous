using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRotation : MonoBehaviour
{
    [SerializeField] Transform axis;
    [SerializeField] Transform[] platforms;
    [SerializeField] float speed;

    // Update is called once per frame
    void Update()
    {
        axis.Rotate(Vector3.forward * speed * Time.deltaTime);
        
        foreach(Transform platform in platforms)
        {
            platform.Rotate(Vector3.forward * (speed * -1) * Time.deltaTime);
        }
    }
}
