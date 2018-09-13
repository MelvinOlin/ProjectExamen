using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baby : MonoBehaviour
{

    bool taken = false;
    // Use this for initialization
    void Start()
    {
        LevelController.instance.babyCount++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!taken)
        {
            var play = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            play.PlayCollectDiamondSound();
            LevelController.instance.babyCountTaken++;
            Destroy(gameObject);
            taken = true;
        }
    }
}
