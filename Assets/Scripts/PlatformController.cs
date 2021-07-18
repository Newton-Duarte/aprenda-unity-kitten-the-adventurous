using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] Transform platform;
    [SerializeField] Transform[] positions;
    [SerializeField] float speed;
    int idTarget;

    // Start is called before the first frame update
    void Start()
    {
        platform.position = positions[0].position;
        idTarget = 1;
    }

    // Update is called once per frame
    void Update()
    {
        platform.position = Vector3.MoveTowards(platform.position, positions[idTarget].position, speed * Time.deltaTime);

        if (platform.position == positions[idTarget].position)
        {
            idTarget++;

            if (idTarget == positions.Length) { idTarget = 0; }
        }
    }
}
