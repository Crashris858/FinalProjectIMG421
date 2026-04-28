using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float CamRotationSpeedX=50f; 
    public float CamRotationSpeedY=50f; 
    private float XRotation=0; 
    private float YRotation=0; 
    public Transform Orientation;
    public bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        //handle cursor 
        Cursor.lockState =CursorLockMode.Locked; 
        Cursor.visible=false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove) return;
        
        //get mouse movement 
        float HorizontalMouse = Input.GetAxis("Mouse X")*CamRotationSpeedX*Time.deltaTime; 
        float VerticalMouse=Input.GetAxis("Mouse Y")*CamRotationSpeedY*Time.deltaTime; 

        //calcualte rotations 
        XRotation+=HorizontalMouse; 
        YRotation-=VerticalMouse; 

        //clamp to limitations 
        YRotation=Mathf.Clamp(YRotation,-90f,90f);

        //rotate around
        //note for my refrence: Unity uses degress, relatives, or euler angles 
        //note: Swapped X and Y rotations in name for visual purposes. 
        transform.rotation=Quaternion.Euler(YRotation,XRotation,0);
        Orientation.rotation = Quaternion.Euler(0,XRotation,0);
    }
}
