using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // speedを制御する
    public float speed = 10;
    public float jumpPower = 450;
    private PhysicMaterial physicMaterial;
    private new Rigidbody rigidbody;
    float dynamicFriction_base;
    float mass_base;
    float drag_base;
    float brake_power;
    bool onGround = false;
    
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

    // 接触判定
    // オブジェクトと接触した時に呼ばれるコールバック
    void OnCollisionEnter(Collision hit)
    {
        // 接触対象はGroundタグか
        if (hit.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
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
        
        // Spaceキーが押されている間 and "Ground"tagを付与されたオブジェクトと接触した時はJumpする
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            physicMaterial.dynamicFriction = dynamicFriction_base;
            rigidbody.mass = mass_base;
            rigidbody.drag = drag_base;
            Debug.Log("Space key was pressed." + ", onGround = " + onGround + ", Mass = " + rigidbody.mass + ", Drag = " + rigidbody.drag);
            // 自身にかかっている力をリセット
            Vector3 now_velocity = rigidbody.velocity;
            now_velocity.y = 0.0f;
            rigidbody.velocity = now_velocity;
            rigidbody.AddForce(0, jumpPower, 0);
            onGround = false;
        }
        else if (Input.GetKeyDown(KeyCode.B) && onGround)
        {
            // Bキーが押されている間はブレーキをかける
            Debug.Log("B key was pressed.");
            rigidbody.mass = mass_base * 100.0f;
            rigidbody.drag = 50.0f;
            physicMaterial.dynamicFriction = 1.0f;
            brake_power = 10.0f * rigidbody.mass * physicMaterial.dynamicFriction;
            Vector3 brake_direction = -1.0f * rigidbody.velocity;
            rigidbody.AddForce(brake_power * speed * brake_direction.x, 0, brake_power * speed * brake_direction.z);
            Debug.Log("Friction = " + physicMaterial.dynamicFriction + ", Mass = " + rigidbody.mass + ", Drag = " + rigidbody.drag);
        }
        else
        {
            physicMaterial.dynamicFriction = dynamicFriction_base;
            rigidbody.mass = mass_base;
            rigidbody.drag = drag_base;
        }
        
    }

    void FixedUpdate()
    {
        // Debug.Log("onGround = " + onGround + ", Mass = " + rigidbody.mass + ", Drag = " + rigidbody.drag);
        // 入力をxとzに代入
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Debug.Log("x = " + x + ", z = " + z);
        rigidbody.AddForce(x * speed, 0, z * speed);
        x = 0;
        z = 0;
    }

}
