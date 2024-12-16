using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigater : MonoBehaviour
{
    public Transform target; // ターゲットへの参照
    public Transform player; // プレイヤーへの参照
    private Transform my_transform; // 自分自身のTransform

    void Start()
    {
        my_transform = this.transform;
    }

    void Update()
    {
        // playerからtargetへの方向ベクトルを計算
        Vector3 direction = target.position - player.position;
        
        // 矢印オブジェクトをplayerの位置に配置
        my_transform.position = player.position;
        Vector3 pos = my_transform.position;
        pos.y += 1.0f;
        my_transform.position = pos;
        
        // 矢印オブジェクトをdirectionの方向に回転
        my_transform.rotation = Quaternion.LookRotation(direction);
        Vector3 euler = my_transform.rotation.eulerAngles;
        euler.x += 90;
        my_transform.rotation = Quaternion.Euler(euler);
    }
}
