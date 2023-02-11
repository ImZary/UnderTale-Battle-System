using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour, IFightObject
{
    public Transform playerTransform;
    private List<IFightObject> fightObjects = new();
    public PelletType type;
    private float time;
    public void Spawn()
    {
    }

    public void Tick()
    {
        playerTransform = FindObjectOfType<PlayerMovement>().transform;
        time += Time.deltaTime;

        if (type == PelletType.FollowDirect)
            HandleFollowDirect();
        else if (type == PelletType.FallFollowDirect)
            HandleFall();
        if (type == PelletType.JumpDirect)
            JumpDirect();
    }
    void HandleFollowDirect()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, Time.deltaTime);
    }

    void JumpDirect()
    {
        if (time < 1)
            return;
        else
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerTransform.position.x, transform.position.y * -2) , Time.deltaTime * 2);
    }
    void HandleFall()
    {
        if (time < 1)
            transform.position += Vector3.down * Time.deltaTime;
        else
            HandleFollowDirect();
    }


    public void Remove()
    {
        Destroy(gameObject);
    }

   
}






