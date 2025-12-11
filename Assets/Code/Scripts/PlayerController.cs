using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private KeyCode[] moveKeyCodes;
    [SerializeField] private Vector3[] moveDirections;
    [SerializeField] private float moveVelocity = 5f;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private Camera playerCamera;

    private float rotationCamEnX = 0f;
    private void Awake()
    {
        // garder cursor locked in place
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnValidate()
    {
        Debug.Assert(moveKeyCodes.Length == moveDirections.Length);
        Debug.Assert(playerCamera != null);
    }
    private void Update()
    {
        CheckMoveInput();
        CheckMouseRotation();
    }
    private void CheckMoveInput()
    {
        for(int i = 0; i != moveKeyCodes.Length; ++i)
            if (Input.GetKey(moveKeyCodes[i]))
                Move(Time.deltaTime * moveVelocity * moveDirections[i]);
    }
    private void Move(Vector3 velocity) =>
        transform.Translate(velocity);

    private void CheckMouseRotation()
    {
        // get the relevant axis for mouse rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        //rotate whole player around Y (turning around)
        transform.Rotate(Vector3.up * mouseX);

        // for vertical: only camera looks around
        rotationCamEnX -= mouseY;
        // clamp
        //rotationCamEnX = Mathf.Clamp(rotationCamEnX, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationCamEnX, 0f, 0f);
    }
}
