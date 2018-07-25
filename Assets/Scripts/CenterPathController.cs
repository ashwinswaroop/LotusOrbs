using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterPathController : MonoBehaviour {

    public Sprite cps;
    public int status;
    SpriteRenderer cpsr;

    void Start () {
        cpsr = gameObject.GetComponent<SpriteRenderer>();
        cpsr.sprite = cps;
        cpsr.flipX = true;
        cpsr.flipY = true;
        transform.position = new Vector3(0, 0, 0);
        status = 0;
    }

    void OnMouseDown()
    {
        
    }

    void Update () {

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            if (worldPosition.x >= -2.5 && worldPosition.x <= 0)
            {
                cpsr.flipX = true;
                status = 0;
            }
            else if (worldPosition.x > 0 && worldPosition.x <= 2.5)
            {
                cpsr.flipX = false;
                status = 1;
            }
        }
		
	}
}
