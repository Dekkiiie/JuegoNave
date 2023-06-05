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
    public float rollSpeed = 90f,rollAcc = 3.5f, timeAccel,cooldown,saveCooldown,saveTimeAcc;

    public Rigidbody rb;

    public KeyCode Accel;

    public string ver,hov,hor,roll;

    private float saveSpeed;

    bool nitroOn;
    // Start is called before the first frame update
    void Start()
    {
        screenCenter.x = Screen.width * 0.5f;
        screenCenter.y = Screen.height * .5f;

        saveCooldown = cooldown;
        saveTimeAcc = timeAccel;
        rb = GetComponent<Rigidbody>();
        saveSpeed = fowardSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        cooldown -= Time.deltaTime;

        if (Input.GetKeyDown(Accel)&&cooldown <= 0 && nitroOn == false)
        {
            nitroOn = true;
            fowardSpeed *= 5;
            
        }

        if(nitroOn == true )
        {
            timeAccel -= Time.deltaTime;
            if(timeAccel <= 0)
            {
                fowardSpeed = saveSpeed;
                nitroOn = false;
                cooldown = saveCooldown;
                timeAccel = saveTimeAcc;
            }
        }
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

       // mouseDistance.x = (lookInput.x - screenCenter.x)/screenCenter.x;
        //mouseDistance.y = (lookInput.y - screenCenter.y)/screenCenter.y;

       // mouseDistance = Vector2.ClampMagnitude(mouseDistance,1f);

        rollInput = Mathf.Lerp(rollInput,Input.GetAxisRaw(roll),rollAcc * Time.deltaTime);

       // transform.Rotate(-mouseDistance.y * lookRotateSpeed * Time.deltaTime,mouseDistance.x * lookRotateSpeed *Time.deltaTime,rollInput * rollSpeed*Time.deltaTime,Space.Self);
        transform.Rotate(activeHoverSpeed * lookRotateSpeed *Time.deltaTime,activeStrafeSpeed * lookRotateSpeed * Time.deltaTime, rollInput * rollSpeed * Time.deltaTime, Space.Self);


        activeFowardSpeed = Mathf.Lerp(activeFowardSpeed,/* Input.GetAxisRaw(ver) */ fowardSpeed,fowardAcc * Time.deltaTime);
        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw(hor) * strafeSpeed, strafeAcc * Time.deltaTime);
        activeHoverSpeed  = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw(hov) * hoverSpeed, hoverAcc * Time.deltaTime);

        transform.position += transform.forward * activeFowardSpeed * Time.deltaTime;

        Vector3 movement = new Vector3(activeStrafeSpeed, 0f, activeFowardSpeed);

        rb.AddForce(movement * fowardSpeed);
        //transform.position += (transform.right * activeStrafeSpeed * Time.deltaTime) + (transform.up * activeHoverSpeed * Time.deltaTime);

    }
}
