using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
	[SerializeField] RayCast ray;
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

	void Start()
	{
	

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

	void Update()
	{
		// We are grounded, so recalculate move direction based on axes
		Vector3 forward = transform.TransformDirection(Vector3.forward);
		Vector3 right = transform.TransformDirection(Vector3.right);
		// Press Left Shift to run
		bool isRunning = Input.GetKey(KeyCode.LeftShift);
		float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
		float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
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
		MouseInputDown();
		MouseInputUp();
		if (canMove && canMouseMove)
		{
			rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
			rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
			playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
			transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
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
			if(Vector3.Distance(Input.mousePosition,lastMousePos)!= 0.0f)
			{
				
				canRay = false;
			}
			
		}
		
	}
	
	void MouseInputUp()
	{
		Debug.Log("MOuse Up");
		if(Input.GetMouseButtonUp(0))
		{
			canMouseMove = false;
			if(canRay)
			{
				Debug.Log("rayCast");
				
				ray.Ray();
			
				canRay = false;
				
			}
		}
		
	}
	/////////////////////////
	
	public void MoveByPoint(Vector3 vec3)
	{
		
		Vector3 v = vec3;
		v.y=1f;
		characterController.enabled = false;
		transform.DOKill();
		transform.DOMove(v, 2f, false).OnComplete(() => 
		{
			characterController.enabled = true;
		});
		//transform.DORestart();
		
		Debug.Log("Move");
		
	}
	
	
	
	
}
