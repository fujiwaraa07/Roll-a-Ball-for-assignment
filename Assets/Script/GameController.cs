using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public UnityEngine.UI.Text scoreLabel;
    public GameObject winnerLabelObject;
    private int maxItemCount;

    // Start is called before the first frame update
    void Start()
    {
        maxItemCount = GameObject.FindGameObjectsWithTag ("Item").Length;
        winnerLabelObject.SetActive (false);
    }

    // Update is called once per frame
    void Update()
    {
        int count = GameObject.FindGameObjectsWithTag ("Item").Length;
        scoreLabel.text = count.ToString() + " / " + maxItemCount.ToString();

        if (count == 0)
        {
            // オブジェクトをアクティブにする
            winnerLabelObject.SetActive (true);
        }
    }
}
