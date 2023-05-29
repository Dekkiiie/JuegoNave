using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;
    private Rigidbody rB;
    public Vector3 oFFset;
    public float speed;


    private void Start()
    {
        rB = player.GetComponent<Rigidbody>();
    }



 
    private void LateUpdate()
    {
        Vector3 playerForward = (rB.velocity + player.transform.forward).normalized;
        transform.position = Vector3.Lerp(transform.position, player.position + player.TransformVector(oFFset) + playerForward * (-5f), speed * Time.deltaTime);
        transform.LookAt(player);
    }


}
