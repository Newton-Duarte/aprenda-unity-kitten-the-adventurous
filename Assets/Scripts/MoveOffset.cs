using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOffset : MonoBehaviour
{
    float offset;
    MeshRenderer meshRenderer;
    Material currentMaterial;
    [SerializeField] float moveSpeed;
    [SerializeField] float offsetIncrement;
    [SerializeField] string sortingLayer;
    [SerializeField] int orderInLayer;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        currentMaterial = meshRenderer.material;

        meshRenderer.sortingLayerName= sortingLayer;
        meshRenderer.sortingOrder = orderInLayer;
    }

    // Update is called once per frame
    void Update()
    {
        offset += offsetIncrement;
        currentMaterial.SetTextureOffset("_MainTex", new Vector2(offset * moveSpeed, 0));
    }
}
