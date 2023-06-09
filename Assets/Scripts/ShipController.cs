using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour
{

    public Image leftBar,rightBar,nitroBar;



    public float fowardSpeed = 25f,strafeSpeed = 7.5f,hoverSpeed = 5f;
    private float activeFowardSpeed,activeStrafeSpeed,activeHoverSpeed;
    private float fowardAcc = 2.5f, strafeAcc = 2f,hoverAcc = 2f;

    public float lookRotateSpeed = 90f;
    private Vector2 lookInput,screenCenter,mouseDistance;

    private float rollInput;
    public float rollSpeed = 90f,rollAcc = 3.5f, timeAccel,cooldown,saveCooldown,saveTimeAcc;

    public Rigidbody rb;

    public KeyCode Accel;
    Vector3 movement;

    public string ver,hov,hor,roll;

    private float saveSpeed;

    bool nitroOn;

   [SerializeField] private float health;
    public float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        screenCenter.x = Screen.width * 0.5f;
        screenCenter.y = Screen.height * .5f;

        
        saveTimeAcc = timeAccel;
        rb = GetComponent<Rigidbody>();
        saveSpeed = fowardSpeed;
        health = maxHealth;
    }

    // Update is called once per frame
    public void Boost(float boost)
	{
        fowardSpeed *= boost;
        nitroOn = true;
	}
    void Update()
    {
        if(nitroOn == false)
        {
            cooldown += Time.deltaTime;

        }

        if (Input.GetKeyDown(Accel)&&cooldown >= saveCooldown && nitroOn == false)
        {
            Boost(5f);
            cooldown = 0;
        }

        if(nitroOn == true )
        {
            timeAccel -= Time.deltaTime;
            if(timeAccel <= 0)
            {
                fowardSpeed = saveSpeed;
                nitroOn = false;
               
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

         movement = new Vector3(activeStrafeSpeed, 0f, activeFowardSpeed);

       
        //transform.position += (transform.right * activeStrafeSpeed * Time.deltaTime) + (transform.up * activeHoverSpeed * Time.deltaTime);
        MostrarBarraHP();
    }


    void MostrarBarraHP()
    {
        float _hp = health / maxHealth;
        leftBar.fillAmount = _hp;
        rightBar.fillAmount = _hp;

        float _nitro = cooldown / saveCooldown;
        nitroBar.fillAmount = _nitro;
    }

    private void FixedUpdate()
    {
        rb.AddForce(movement * fowardSpeed);
    }
    
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Asteroid"))
		{
            health--;

            if(health <= 0)
			{
                Destroy(gameObject);
			}
		}

		if (collision.gameObject.CompareTag("PowerUp"))
		{
            Boost(5f);
            Destroy(collision.gameObject);
        }
	}
}
