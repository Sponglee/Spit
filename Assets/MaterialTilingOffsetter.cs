using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialTilingOffsetter : MonoBehaviour
{
    private Material targetMat;
    [SerializeField] private Vector2 speed = new Vector2(1,1);

    private void Start()
    {
        targetMat = GetComponent<Renderer>().material;
        InvokeRepeating(nameof(RotateTexture), Random.Range(0f, 2f), Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void RotateTexture()
    {
        targetMat.mainTextureOffset += new Vector2(speed.x, speed.y) / 1000f;

        if (targetMat.mainTextureOffset.x >= 1000f)
            targetMat.mainTextureOffset = new Vector2(0, targetMat.mainTextureOffset.y);
        if (targetMat.mainTextureOffset.y >= 1000f)
            targetMat.mainTextureOffset = new Vector2(targetMat.mainTextureOffset.x, 0);
    }
}
