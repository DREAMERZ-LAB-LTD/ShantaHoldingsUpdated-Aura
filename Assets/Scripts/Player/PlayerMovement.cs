using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
   // [SerializeField] VariableJoystick vj;
	[SerializeField] RayCast ray;
	[SerializeField] GameObject areaTouchRotate;
	public float walkingSpeed = 7.5f;
	public float runningSpeed = 11.5f;
	public float jumpSpeed = 8.0f;
	public float gravity = 20.0f;
	public Camera playerCamera;
	public float lookSpeed = 2.0f;
	public float lookXLimit = 45.0f;

	CharacterController characterController;
	Vector3 moveDirection = Vector3.zero;
	float rotationX = 0;

	//	[HideInInspector]
	public bool canMove = true;
	//[HideInInspector]
	public bool canMouseMove = false;
	//[HideInInspector]
	public bool canRay = false;

    [SerializeField] float autoWalk = 1f;

    #region Test
    [SerializeField] Slider slider;
    [SerializeField] Text textLoockSpeed;
    [SerializeField] Slider slider1;
    [SerializeField] Text textWalkSpeed;
    public void ChangeLookScpped(float val)
    {
        lookSpeed = val;
        textLoockSpeed.text = lookSpeed.ToString();
    }
    public void ChangewalkSpeed(float val)
    {
        autoWalk = val;
        textWalkSpeed.text = autoWalk.ToString();
    }
    #endregion

    void Start()
	{
        slider.value = lookSpeed;
        textLoockSpeed.text = lookSpeed.ToString();
        
        slider1.value = autoWalk;
        textWalkSpeed.text = autoWalk.ToString();
        // Lock cursor
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }
	// This function is called when the object becomes enabled and active.
	protected void OnEnable()
	{
		DOTween.Init();
		characterController = GetComponent<CharacterController>();
		canMove = true;
		canMouseMove = false;
		canRay = false;
	}
    float rotX;
    float rotY;
    void Update()
	{
		// We are grounded, so recalculate move direction based on axes
		Vector3 forward = transform.TransformDirection(Vector3.forward);
		Vector3 right = transform.TransformDirection(Vector3.right);
		// Press Left Shift to run
		bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;

        // for mobile 
        /*float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * vj.Vertical : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * vj.Horizontal : 0;*/

        float movementDirectionY = moveDirection.y;
		moveDirection = (forward * curSpeedX) + (right * curSpeedY);

		// There are not need for Jump on this project so far
		//if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
		//{
		//	moveDirection.y = jumpSpeed;
		//}
		//else
		//{
		//	moveDirection.y = movementDirectionY;
		//}

		// Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
		// when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
		// as an acceleration (ms^-2)
		if (!characterController.isGrounded)
		{
			moveDirection.y -= gravity * Time.deltaTime;
		}

		// Move the controller
		characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        //MouseInputDown();
        //MouseInputUp();
        Touch touch;
        if (!isPointerUp)
        {
            /*	rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);*/

            // for mobile
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    areaTouchRotate.SetActive(false);
                    characterController.enabled = true;
                    Debug.Log("MouseDown");
                    
                    canMouseMove = true;
                    canRay = true;


                   // vj.gameObject.SetActive(false);
                }
                else if (touch.phase == TouchPhase.Moved && canMove && canMouseMove)
                {
                    // form metrial selct
                    MetrialUI.instance.metrailPenel.SetActive(false);
                    MetrialUI.instance.ObjectPenel.SetActive(false);
                    
                    canRay = false;
                    Vector2 pos = touch.position;
                    rotX += touch.deltaPosition.y * lookSpeed * Mathf.Deg2Rad * Time.deltaTime;
                    rotY += touch.deltaPosition.x * lookSpeed * Mathf.Deg2Rad * Time.deltaTime;

                    /*rotX = Mathf.Clamp(rotX, minX, maxX);
                    rotY = Mathf.Clamp(rotY, minY, maxY);*/

                    //camera.transform.localEulerAngles = new Vector3(0, rotY, 0);
                    playerCamera.transform.localEulerAngles = new Vector3(-rotX, playerCamera.transform.localEulerAngles.y, 0);
                    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, rotY, 0);

                    //transform.DORotate(new Vector3(0, transform.rotation.y + rotX, transform.rotation.z + rotY), 0.1f, RotateMode.Fast);

                    //transform.Rotate(new Vector3(0f, transform.rotation.y + rotX, transform.rotation.z + rotY) * sencativity * Time.deltaTime, Space.World);
                    //camera.transform.Rotate(new Vector3(camera.transform.loca.x - rotY , camera.transform.rotation.y + rotX , 0) * sencativity * Time.deltaTime, Space.Self);

                    //camera.transform.rotation = Quaternion.Euler(camera.transform.rotation.x, camera.transform.rotation.y, 0f);
                }
                
            }
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                
                if (touch.phase == TouchPhase.Ended)
                {
                    Debug.Log("MOuse UP");
                    isPointerUp = true;
                  // vj.gameObject.SetActive(true);
                    canMouseMove = false;
                    if (canRay)
                    {
                        Debug.Log("rayCast");

                        ray.Ray();

                        canRay = false;

                    }
                    areaTouchRotate.SetActive(true);
                }
                
            }
        }
	}
	
	/// <summary>
	/// This code detect The mmouse moved or not
	/// and can is allow to detect the serface?
	/// </summary>
	Vector3 lastMousePos;
	void MouseInputDown()
	{
		
		if(Input.GetMouseButtonDown(0))
		{
			characterController.enabled = true;
			//Debug.Log("MouseDown");
			lastMousePos = Input.mousePosition;
			canMouseMove = true;
			canRay =true;
		}
		if(canMouseMove)
		{
			//Debug.Log(Vector3.Distance(Input.mousePosition,lastMousePos).ToString());
			if(Vector3.Distance(Input.mousePosition,lastMousePos) != 0.0f)
			{
				
				canRay = false;
			}
			
		}
		
	}

    void MouseInputUp()
    {
        //Debug.Log("MOuse Up");
        if (Input.GetMouseButtonUp(0))
        {
            canMouseMove = false;
            if (canRay)
            {
                Debug.Log("rayCast");

                ray.Ray();

                canRay = false;

            }
        }

    }

    [SerializeField] private bool isPointerUp = true;
    public void EventPointerUp()
    {
        isPointerUp = true;
       // vj.gameObject.SetActive(true);
    }
    public void EventPointerDown()
    {
        isPointerUp = false;

       // vj.gameObject.SetActive(false);
    }

	/////////////////////////
	
	public void MoveByPoint(Vector3 vec3)
	{
		
		Vector3 v = vec3;
		v.y= transform.position.y;
		characterController.enabled = false;
        Debug.Log(Vector3.Distance(transform.position, v).ToString());
		transform.DOKill();
		transform.DOMove(v, Vector3.Distance(transform.position, v) * autoWalk, false).OnComplete(() => 
		{
			characterController.enabled = true;
		});
		//transform.DORestart();
		
		Debug.Log("Move");
		
	}
	
	
	
	
}
