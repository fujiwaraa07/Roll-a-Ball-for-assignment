using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // speedを制御する
    public float speed = 10;
    public float jumpPower = 10;
    private PhysicMaterial physicMaterial;
    private new Rigidbody rigidbody;
    float dynamicFriction_base;
    float mass_base;
    float drag_base;
    float brake_power;
    
    // Start is called before the first frame update
    void Start()
    {
        physicMaterial = GetComponent<Collider>().material;
        rigidbody = GetComponent<Rigidbody>();
        dynamicFriction_base = physicMaterial.dynamicFriction;
        mass_base = rigidbody.mass;
        drag_base = rigidbody.drag;
        Debug.Log("Hello World");
        Debug.Log("Base Friction = " + physicMaterial.dynamicFriction + ", Base Mass = " + rigidbody.mass);
    }

    void Update()
    {
        // 現在のシーン番号を取得
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (rigidbody.position.y < -50)
        {
            // 現在のシーンを再読込する
            SceneManager.LoadScene(sceneIndex);
        }
        
    }

    void FixedUpdate()
    {
        // 入力をxとzに代入
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        // Debug.Log("x = " + x + ", z = " + z);
        
        // Bキーが押されている間はブレーキをかける
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("B key was pressed.");
            rigidbody.mass = mass_base * 100.0f;
            rigidbody.drag = 50.0f;
            physicMaterial.dynamicFriction = 1.0f;
            brake_power = -1.0f * rigidbody.mass * physicMaterial.dynamicFriction;
            rigidbody.AddForce(brake_power * speed, 0, brake_power * speed);
            Debug.Log("Friction = " + physicMaterial.dynamicFriction + ", Mass = " + rigidbody.mass + ", Drag = " + rigidbody.drag);
        }
        else
        {
            physicMaterial.dynamicFriction = dynamicFriction_base;
            rigidbody.mass = mass_base;
            rigidbody.drag = drag_base;
        }

        // Spaceキーが押されている間はJumpする
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rigidbody.velocity.y) < 0.01f)
        {
            Debug.Log("Space key was pressed.");
            rigidbody.AddForce(0, 20*jumpPower, 0);
        }

        // rigidbodyのx軸（横）とz軸（奥）に力を加える
        rigidbody.AddForce(x * speed, 0, z * speed);

    }
}
