using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot_Script : MonoBehaviour
{

    public Player player;
    bool eaten = false;

    private void Start()
    {
        player = GameManager_Script.instance.player;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!eaten)
        {
            Eat();
            eaten = true;
        }
    }

    void Eat()
    {
        StartCoroutine(SpeedBoostTimer());
        gameObject.GetComponent<Renderer>().enabled = false;
    }

    IEnumerator SpeedBoostTimer()
    {


        player.speed = player.speed * 2;
        yield return new WaitForSeconds(5);
        player.speed = player.speed / 2;
        Destroy(gameObject);


    }
}
