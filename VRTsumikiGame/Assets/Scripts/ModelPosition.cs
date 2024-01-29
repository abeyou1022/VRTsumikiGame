using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelPosition : MonoBehaviour
{
    public GameObject MainCamera;
    Transform main;
    public float deviationY = 1.8f;
    public float deviationZ = 0.0f;
    public float height = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        main = MainCamera.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = new Vector3(main.position.x, main.position.y-deviationY, main.position.z-deviationZ);
        //Vector3 position = new Vector3(main.position.x, height, main.position.z-deviationZ);
        gameObject.transform.position = position;

        //Quaternion rotation = main.rotation; //アバターの角度の調整(未確認）
        //gameObject.transform.rotation = Quaternion.Euler(0,rotation.eulerAngles.y,0);

        //Debug.Log(main.rotation);
    }
}
