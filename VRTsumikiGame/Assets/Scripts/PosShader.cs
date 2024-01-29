using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosShader : MonoBehaviour
{
    [SerializeField] Renderer mat;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        mat.material.SetVector("_Center", transform.position);
    }
}
