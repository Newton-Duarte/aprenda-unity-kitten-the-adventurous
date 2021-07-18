using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsAI : MonoBehaviour
{
    [SerializeField] Transform objectToMove;
    [SerializeField] Transform[] positions;
    [SerializeField] float moveSpeed;
    [SerializeField] bool isLookLeft;
    SpriteRenderer objectSr;
    int idTarget;

    // Start is called before the first frame update
    void Start()
    {
        objectSr = objectToMove.GetComponent<SpriteRenderer>();
        objectToMove.position = positions[0].position;
        idTarget = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (objectToMove)
        {
            objectToMove.position = Vector3.MoveTowards(objectToMove.position, positions[idTarget].position, moveSpeed * Time.deltaTime);

            if (objectToMove.position == positions[idTarget].position)
            {
                idTarget++;

                if (idTarget == positions.Length)
                {
                    idTarget = 0;
                }
            }

            if (positions[idTarget].position.x < objectToMove.position.x && !isLookLeft){ flipX(); }
            else if (positions[idTarget].position.x > objectToMove.position.x && isLookLeft) { flipX(); }
        }
        else if (!objectToMove) { Destroy(gameObject); }
    }

    void flipX()
    {
        isLookLeft = !isLookLeft;
        objectSr.flipX = !objectSr.flipX;
    }
}
