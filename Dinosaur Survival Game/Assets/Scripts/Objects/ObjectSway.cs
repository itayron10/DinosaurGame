using UnityEngine;

public class ObjectSway : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float swayAmount = 1;
    [SerializeField] float smoothAmount = 3;
    
    // apply sway to the object bsaed on the mouse movement
    private void Update() => RotateObjectBaseOnMouse();

    private void RotateObjectBaseOnMouse()
    {
        // record the mouse x and y values
        (float mouseX, float mouseY) = InputManager.GetMouseDeltaPosXandY();
        // add sway to the object based on the mouse x and y amount and the sway amount
        LerpRotation(mouseX * swayAmount, mouseY * swayAmount);
    }

    private void LerpRotation(float mouseX, float mouseY)
    {
        // get the new y and x rotations based on the mouse y and x
        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(-mouseX, Vector3.up);
        // caculate the final target rotaion
        Quaternion targetRotation = rotationX * rotationY;
        // lerps the local rotaion to the new target rotation based on the smooth amount
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, smoothAmount * Time.deltaTime);
    }
}
