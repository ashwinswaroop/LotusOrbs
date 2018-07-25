using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightPathController : MonoBehaviour
{

    public Sprite rps1, rps2;
    SpriteRenderer rpsr;
    public int status;

    void Start()
    {
        rpsr = gameObject.GetComponent<SpriteRenderer>();
        rpsr.sprite = rps1;
        rpsr.flipX = true;
        rpsr.flipY = false;
        transform.position = new Vector3(3, 0, 0);
        status = 0;
    }


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            if (worldPosition.x >= 3.5 && worldPosition.x <= 6 && worldPosition.y >= -1.5 && worldPosition.y <= 1.5)
            {
                rpsr.sprite = rps2;
                status = 1;
            }
            else if (worldPosition.x > 2.5 && worldPosition.x <= 5 && worldPosition.y >= 0 && worldPosition.y <= 4)
            {
                rpsr.sprite = rps1;
                rpsr.flipY = false;
                status = 0;

            }
            else if (worldPosition.x > 2.5 && worldPosition.x <= 5 && worldPosition.y <= 0 && worldPosition.y >= -4)
            {
                rpsr.sprite = rps1;
                rpsr.flipY = true;
                status = 2;
            }
        }
    }


}
