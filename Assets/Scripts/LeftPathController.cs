using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPathController : MonoBehaviour
{

    public Sprite lps1, lps2;
    SpriteRenderer lpsr;
    public int status;

    void Start()
    {
        lpsr = gameObject.GetComponent<SpriteRenderer>();
        lpsr.sprite = lps1;
        lpsr.flipX = false;
        lpsr.flipY = false;
        transform.position = new Vector3(-3, 0, 0);
        status = 2;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            if (worldPosition.x <= -3.5 && worldPosition.x >= -6 && worldPosition.y >= -1.5 && worldPosition.y <= 1.5)
            {
                lpsr.sprite = lps2;
                status = 1;
            }
            else if (worldPosition.x < -2.5 && worldPosition.x >= -5 && worldPosition.y >= 0 && worldPosition.y <= 4)
            {
                lpsr.sprite = lps1;
                lpsr.flipY = false;
                status = 2;

            }
            else if (worldPosition.x < -2.5 && worldPosition.x >= -5 && worldPosition.y <= 0 && worldPosition.y >= -4)
            {
                lpsr.sprite = lps1;
                lpsr.flipY = true;
                status = 0;
            }
        }
    }
}
