using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // speedを制御する
    public float speed = 10;
    private PhysicMaterial physicMaterial;
    float dynamicFriction_base;
    
    // Start is called before the first frame update
    void Start()
    {
        physicMaterial = GetComponent<Collider>().material;
        dynamicFriction_base = physicMaterial.dynamicFriction;
        Debug.Log("Hello World");
        Debug.Log("Base Friction = " + physicMaterial.dynamicFriction);
    }

    void Update()
    {
        // Spaceキーが押されたらブレーキをかける
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space key was pressed.");
            Debug.Log("Friction = " + physicMaterial.dynamicFriction);
            physicMaterial.dynamicFriction = 1.0f;
        }
    }

    void FixedUpdate()
    {
        // 入力をxとzに代入
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        // Debug.Log("x = " + x + ", z = " + z);

        // 同一のGameObjectが持つRigidbodyコンポーネントを取得
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        // rigidbodyのx軸（横）とz軸（奥）に力を加える
        rigidbody.AddForce(x * speed, 0, z * speed);
    }
}
