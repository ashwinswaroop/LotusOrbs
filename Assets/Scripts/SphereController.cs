using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{

    public float speed;
    public Transform cpp;
    public Transform lpp;
    public Transform rpp;
    public Sprite[] sphereSpriteList;
    public Sprite incorrect;
    CenterPathController cpc;
    LeftPathController lpc;
    RightPathController rpc;
    Vector3 destination;
    public int sphereColor;
    bool isColliding;

    void Start()
    {
        isColliding = false;
        cpc = cpp.GetComponent<CenterPathController>();
        lpc = lpp.GetComponent<LeftPathController>();
        rpc = rpp.GetComponent<RightPathController>();
        transform.position = new Vector3(0, -3, 0);
        destination = new Vector3(0, 0, 0);
    }

    void Update()
    {
        if(GameObject.Find("GameController").GetComponent<GameController>().deaths >= 5)
        {
            Destroy(gameObject);
        }
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, destination, step);
        if (transform.position.x == destination.x && transform.position.y == destination.y)
        {
            if(destination.x==0 && destination.y == 0 && cpc.status == 0)
            {
                transform.Rotate(new Vector3(0, 0, -90));
                destination = new Vector3(-3, 0, 0);
            }
            else if (destination.x == -3 && destination.y == 0 && lpc.status == 0)
            {
                transform.Rotate(new Vector3(0, 0, -90));
                destination = new Vector3(-3, -2.85f, 0);
                speed = 8;
            }
            else if (destination.x == -3 && destination.y == 0 && lpc.status == 1)
            {
                destination = new Vector3(-5.85f, 0, 0);
                speed = 8;
            }
            else if (destination.x == -3 && destination.y == 0 && lpc.status == 2)
            {
                transform.Rotate(new Vector3(0, 0, 90));
                destination = new Vector3(-3, 2.85f, 0);
                speed = 8;
            }
            else if (destination.x == 0 && destination.y == 0 && cpc.status == 1)
            {
                transform.Rotate(new Vector3(0, 0, 90));
                destination = new Vector3(3, 0, 0);
            }
            else if (destination.x == 3 && destination.y == 0 && rpc.status == 2)
            {
                transform.Rotate(new Vector3(0, 0, 90));
                destination = new Vector3(3, -2.85f, 0);
                speed = 8;
            }
            else if (destination.x == 3 && destination.y == 0 && rpc.status == 1)
            {
                destination = new Vector3(5.85f, 0, 0);
                speed = 8;
            }
            else if (destination.x == 3 && destination.y == 0 && rpc.status == 0)
            {
                transform.Rotate(new Vector3(0, 0, -90));
                destination = new Vector3(3, 2.85f, 0);
                speed = 8;
            }

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Target"))
        {
            if (isColliding) return;
            isColliding = true;
            if (sphereColor == other.gameObject.GetComponent<TargetController>().targetColor)
            {
                gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                Destroy(gameObject, 1f);
                GameObject.Find("GameController").GetComponent<GameController>().streak++;
                GameObject.Find("DM-CGS-32").GetComponent<AudioSource>().Play();
            }
            else
            {
                gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                gameObject.GetComponent<SpriteRenderer>().sprite = incorrect;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                Destroy(gameObject, 1f);
                GameObject.Find("GameController").GetComponent<GameController>().deaths++;
                GameObject.Find("GameController").GetComponent<GameController>().streak = 0;
                GameObject.Find("DM-CGS-31").GetComponent<AudioSource>().Play();
            }
        }
    }
}
