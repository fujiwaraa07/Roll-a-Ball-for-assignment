using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigater : MonoBehaviour
{
    private Transform target; // ターゲットへの参照
    public Transform player; // プレイヤーへの参照
    private Transform my_transform; // 自分自身のTransform
    // Item_holder tagを持つオブジェクトを格納する配列
    private GameObject[] items;

    void Start()
    {
        my_transform = this.transform;        
    }

    void Update()
    {
        // Item tagを持つオブジェクトのうち、プレイヤーから最も近いオブジェクトをtargetに設定
        items = GameObject.FindGameObjectsWithTag("Item");
        for (int i = 0; i < items.Length; i++)
        {
            if (i == 0)
            {
                target = items[i].transform;
            }
            else
            {
                if (Vector3.Distance(player.position, items[i].transform.position) < Vector3.Distance(player.position, target.position))
                {
                    target = items[i].transform;
                }
            }
        }

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
