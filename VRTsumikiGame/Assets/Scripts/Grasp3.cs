using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grasp3 : MonoBehaviour
{

    public GameObject target; //掴む対象
    public GameObject holdingObject; //現在掴んでいる対象
    GameObject empty; //空のオブジェクト(何も掴んでいたいとき)
    //public Parts part;


    private void Start()
    {
        empty = new GameObject("EmptyTarget");
        target = empty;
        holdingObject = empty;
    }

    private void Update()
    {
        //Debug.Log(GetComponent<Hand>().IsInset());
        if (gameObject.GetComponent<Hand>().Target(target) || gameObject.GetComponent<Sign>().HandSign() == HandSign.ROCK) //それぞれの指が対象に触れていたら掴む または グーのとき
        {
            if (!gameObject.GetComponent<Hand>().IsInset()) //めり込みの判定
            {
                holdingObject = target;
                if (holdingObject.GetComponent<Rigidbody>() != null)
                {
                    if (gameObject.GetComponent<FixedJoint>() == null)
                    {
                        holdingObject.GetComponent<Rigidbody>().isKinematic = true;
                    }

                }
                holdingObject.transform.SetParent(gameObject.transform, true);
                if (holdingObject.GetComponent<GraspAble>() != null) holdingObject.GetComponent<GraspAble>().holding = true;
            }
            else Release();

        }
        else
        {
            Release();
        }

    }

    private void Release()
    {
        holdingObject.transform.SetParent(null, true);
        //if(gameObject.transform.childCount>0) gameObject.transform.GetChild(0).SetParent(null, true);
        if (holdingObject.GetComponent<Rigidbody>() != null)
        {
            Rigidbody rb = holdingObject.GetComponent<Rigidbody>();
            if (rb.useGravity) rb.isKinematic = false;

            /*
            //投げる
            Vector3 force = -gameObject.transform.forward * 100f;
            rb.AddForce(force);
            Debug.Log(force);
            */
        }
        if(holdingObject.GetComponent<GraspAble>() != null) holdingObject.GetComponent<GraspAble>().holding = false;
        holdingObject = empty;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GraspAble>() != null && target==empty) //掴めるオブジェクトのみ
            target = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<GraspAble>() != null && target != empty) //掴めるオブジェクトのみ
            target = empty;
    }
}


