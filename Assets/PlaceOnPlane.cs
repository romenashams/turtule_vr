using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;


[HelpURL("https://youtu.be/HkNVp04GOEI")]
[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlane : PressInputBase
{
    [SerializeField]
    [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    GameObject placedPrefab;


    GameObject spawnedObject;
    bool isPressed;

    ARRaycastManager aRRaycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();


    public Button upButton;
    public Button downButton;
    public Button leftButton;
    public Button rightButton;
    public Button rotationButton;
    public float moveAmount = 1f;
    public float moveSpeed = 5f;
    public float rotationSpeed = 90f;
    protected override void Awake()
    {
        base.Awake();
        aRRaycastManager = GetComponent<ARRaycastManager>();
        upButton.onClick.AddListener(MoveUp);
        downButton.onClick.AddListener(MoveDown);
        leftButton.onClick.AddListener(MoveLeft);
        rightButton.onClick.AddListener(MoveRight);
        rotationButton.onClick.AddListener(() => Rotation(10));
    }

    public void MoveUp()
    {
        spawnedObject.transform.Translate(Vector2.left * moveSpeed * Time.deltaTime); // Moves left
    }

    public void MoveDown()
    {
        spawnedObject.transform.Translate(Vector2.right * moveSpeed * Time.deltaTime); // Moves right
    }

    public void MoveLeft()
    {
        spawnedObject.transform.Rotate(Vector2.up, -90, Space.World); // Rotates around y-axis
        spawnedObject.transform.Translate(Vector2.left * moveSpeed * Time.deltaTime); // Moves left


    }

    public void MoveRight()
    {
        spawnedObject.transform.Rotate(Vector2.up, 90, Space.World); // Rotates around y-axis
        spawnedObject.transform.Translate(Vector2.left * moveSpeed * Time.deltaTime); // Moves left
        //spawnedObject.transform.Translate(Vector2.down * moveSpeed * Time.deltaTime); // Moves backward

    }

    public void Rotation(float degrees)
    {
        spawnedObject.transform.Rotate(Vector3.up, degrees, Space.World); // Rotates around y-axis
    }

    void Update()
    {
        if (Pointer.current == null || !isPressed)
            return;

        var touchPosition = Pointer.current.position.ReadValue();

        if (aRRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            if (spawnedObject == null)
            {

                spawnedObject = Instantiate(placedPrefab, hitPose.position, Quaternion.Euler(-90, 180, 0));

                //Quaternion initialRotation = Quaternion.Euler(-90, 180, 0);

                spawnedObject.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);  // Scale the object to half size
                Debug.Log("Spawned and rotated object to: " + spawnedObject.transform.rotation.eulerAngles);
                
            }
            else
            {
                Debug.Log("Spawned and rotated object to: " + spawnedObject.transform.rotation.eulerAngles);

            }
            // Additional debug to confirm rotation after any changes
            Debug.Log("Current Rotation: " + spawnedObject.transform.rotation.eulerAngles);
            // Optional: To make the spawned object always look at the camera
            //Vector3 lookPos = Camera.main.transform.position - spawnedObject.transform.position;
            //lookPos.y = 0; // Keep the object upright relative to the camera
            //spawnedObject.transform.rotation = Quaternion.LookRotation(lookPos);
        }
    }

    protected override void OnPress(Vector3 position) => isPressed = true;

    protected override void OnPressCancel() => isPressed = false;
}


