using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventInset : MonoBehaviour
{
    public bool IsInset = false;
    public GameObject HandCollider;
    private Material material;

    private void Start()
    {
        material = gameObject.GetComponent<Renderer>().material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GraspAble>() != null){
            IsInset = true;
            material.SetFloat("_Toggle",1);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<GraspAble>() != null){
            IsInset = false;
            material.SetFloat("_Toggle", 0);
        }
    }

    public bool GetInset()
    {
        return IsInset;
    }
}
