using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{

    public float fowardSpeed = 25f,strafeSpeed = 7.5f,hoverSpeed = 5f;
    private float activeFowardSpeed,activeStrafeSpeed,activeHoverSpeed;
    private float fowardAcc = 2.5f, strafeAcc = 2f,hoverAcc = 2f;

    public float lookRotateSpeed = 90f;
    private Vector2 lookInput,screenCenter,mouseDistance;

    private float rollInput;
    public float rollSpeed = 90f,rollAcc = 3.5f;

    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        screenCenter.x = Screen.width * 0.5f;
        screenCenter.y = Screen.height * .5f;

        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

       // mouseDistance.x = (lookInput.x - screenCenter.x)/screenCenter.x;
        //mouseDistance.y = (lookInput.y - screenCenter.y)/screenCenter.y;

       // mouseDistance = Vector2.ClampMagnitude(mouseDistance,1f);

        rollInput = Mathf.Lerp(rollInput,Input.GetAxisRaw("Roll"),rollAcc * Time.deltaTime);

       // transform.Rotate(-mouseDistance.y * lookRotateSpeed * Time.deltaTime,mouseDistance.x * lookRotateSpeed *Time.deltaTime,rollInput * rollSpeed*Time.deltaTime,Space.Self);
        transform.Rotate(activeHoverSpeed * lookRotateSpeed *Time.deltaTime,activeStrafeSpeed * lookRotateSpeed * Time.deltaTime, rollInput * rollSpeed * Time.deltaTime, Space.Self);


        activeFowardSpeed = Mathf.Lerp(activeFowardSpeed, Input.GetAxisRaw("Vertical") * fowardSpeed,fowardAcc * Time.deltaTime);
        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed, strafeAcc * Time.deltaTime);
        activeHoverSpeed  = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcc * Time.deltaTime);

        transform.position += transform.forward * activeFowardSpeed * Time.deltaTime;

        Vector3 movement = new Vector3(activeStrafeSpeed, 0f, activeFowardSpeed);

        rb.AddForce(movement * fowardSpeed);
        //transform.position += (transform.right * activeStrafeSpeed * Time.deltaTime) + (transform.up * activeHoverSpeed * Time.deltaTime);

    }
}
