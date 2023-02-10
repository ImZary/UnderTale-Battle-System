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
    }
    void HandleFollowDirect()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, Time.deltaTime);
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






