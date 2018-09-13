using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPathScript : MonoBehaviour
{

    private Vector2 velocity;
    public Player player;

    public float smoothTimeY;
    public float smoothTimeX;

    public bool bounds;
    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x - player.transform.position.x > 32)
        {
            player.died = true;
        }
    }

    private void FixedUpdate()
    {
        if (transform.position.x < 323.5)
        {
            float posX = Mathf.SmoothDamp(transform.position.x, transform.position.x + 0.13f, ref velocity.x, smoothTimeX);

            transform.position = new Vector3(posX, transform.position.y, transform.position.z);

            if (bounds)
            {
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
                    Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
                    Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
            }
        }

    }
}