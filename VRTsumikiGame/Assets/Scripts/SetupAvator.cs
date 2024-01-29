using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupAvator : MonoBehaviour
{
    [SerializeField] float handColliderDia = 0.05f;
    [SerializeField] Vector3 fingerColliderPos = new Vector3(-0.005f, 0, 0);
    [SerializeField] Material prevMat;

    private Transform[] hands = new Transform[2]; //[0]右手, [1]左手
    public Transform[,] fingers = new Transform[2, 5]; //[0]親指, [1]人差し指, [2]中指, [3]薬指, [4]小指
    private GameObject handCollider;
    private GameObject fingerCollider;
    private GameObject preventionInset;

    private void Awake()
    {
        CreateHandCollider();
        CreateFingerCollider();
        CreatePreventionInset();
    }

    // Start is called before the first frame update
    void Start()
    {
        Association();
        
        AddCollider();
    }

    //掌のコライダー
    void CreateHandCollider()
    {
        handCollider = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        handCollider.transform.localScale = new Vector3(handColliderDia, handColliderDia, handColliderDia);
        handCollider.GetComponent<SphereCollider>().isTrigger = true;
        handCollider.AddComponent<Hand>();
        handCollider.AddComponent<Sign>();
        handCollider.AddComponent<Grasp3>();

        Material mat = handCollider.GetComponent<Renderer>().material;
        mat.SetFloat("_Mode", 2);
        mat.color = new Color32(255, 255, 255, 10);


        Rigidbody rb = handCollider.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    //指先のコライダー
    void CreateFingerCollider()
    {
        fingerCollider = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        fingerCollider.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        fingerCollider.GetComponent<SphereCollider>().isTrigger = true;
        fingerCollider.AddComponent<StateComponent>();
    }

    //めり込み検出用コライダー
    void CreatePreventionInset()
    {
        preventionInset = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        preventionInset.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        preventionInset.GetComponent<SphereCollider>().isTrigger = true;
        preventionInset.AddComponent<PreventInset>();
        preventionInset.GetComponent<PreventInset>().HandCollider = handCollider;
        //preventionInset.GetComponent<Renderer>().material.color = Color.red;
        preventionInset.GetComponent<Renderer>().material = prevMat;
    }

    private void AddCollider()
    {
        for (int i = 0; i < 1; i++) //掌
        {
            GameObject controller = Instantiate(handCollider) as GameObject;
            controller.transform.SetParent(hands[i]);
            controller.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
            controller.transform.localPosition = new Vector3(-0.05f, 0, 0);
            Hand hand = controller.GetComponent<Hand>();
            if (i == 0) hand.part = Parts.RIGHT_HAND;
            else if (i == 1) hand.part = Parts.LEFT_HAND;

            GameObject inset = Instantiate(preventionInset) as GameObject;
            inset.transform.SetParent(hands[i]);
            inset.transform.localPosition = new Vector3(-0.06f, 0, 0.04f);
            hand.inset = inset;

            for (int j = 0; j < 5; j++) //指先
            {
                GameObject collider = Instantiate(fingerCollider) as GameObject;

                if (fingers[i, j].childCount > 0) collider.transform.SetParent(fingers[i, j].GetChild(0), true);
                else collider.transform.SetParent(fingers[i, j], true);

                collider.transform.localPosition = fingerColliderPos;

                if (i == 0) collider.GetComponent<StateComponent>().part = Parts.RIGHT_FINGER;
                else if (i == 1) collider.GetComponent<StateComponent>().part = Parts.LEFT_FINGER;

                hand.fingers[j] = collider;
            }
        }

        Destroy(handCollider);
        Destroy(fingerCollider);
        Destroy(preventionInset);
    }

    private void Association()
    {
        Animator animator = gameObject.GetComponent<Animator>(); //身体

        //掌
        hands[0] = animator.GetBoneTransform(HumanBodyBones.RightHand);
        hands[1] = animator.GetBoneTransform(HumanBodyBones.LeftHand);

        //指先 (右)
        fingers[0, 0] = animator.GetBoneTransform(HumanBodyBones.RightThumbDistal);
        fingers[0, 1] = animator.GetBoneTransform(HumanBodyBones.RightIndexDistal);
        fingers[0, 2] = animator.GetBoneTransform(HumanBodyBones.RightMiddleDistal);
        fingers[0, 3] = animator.GetBoneTransform(HumanBodyBones.RightRingDistal);
        fingers[0, 4] = animator.GetBoneTransform(HumanBodyBones.RightLittleDistal);

        //指先 (左)
        fingers[1, 0] = animator.GetBoneTransform(HumanBodyBones.LeftThumbDistal);
        fingers[1, 1] = animator.GetBoneTransform(HumanBodyBones.LeftIndexDistal);
        fingers[1, 2] = animator.GetBoneTransform(HumanBodyBones.LeftMiddleDistal);
        fingers[1, 3] = animator.GetBoneTransform(HumanBodyBones.LeftRingDistal);
        fingers[1, 4] = animator.GetBoneTransform(HumanBodyBones.LeftLittleDistal);
    }
}
