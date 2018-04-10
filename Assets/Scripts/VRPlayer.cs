using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.Networking;

public class VRPlayer : NetworkBehaviour {


    //public enum LocomotionMode { TELEPORT, DRIVE };

    [SerializeField]
    private Transform Head;
    [SerializeField]
    private HandController LeftHand;
    [SerializeField]
    private HandController RightHand;

    // from steamvr camera-rig
    [SerializeField]
    private Transform SteamVR_Rig;
    [SerializeField]
    private SteamVR_TrackedObject hmd;
    [SerializeField]
    private SteamVR_TrackedObject LeftController; 
    [SerializeField]
    public SteamVR_TrackedObject RightController;

    private GameManager gm;
   // public GUIController gui;

    [SyncVar]
    Vector3 headPos;
    [SyncVar]
    Quaternion headRot;
    [SyncVar]
	Vector3 leftHandPos;
	[SyncVar]
	Quaternion leftHandRot;
	[SyncVar]
	Vector3 rightHandPos;
	[SyncVar]
	Quaternion rightHandRot;

    /**
     * Utility Function 
     * copys transform.position and transform.rotation from a to b
     */
    private static void copyTransform(Transform a, Transform b) {
        b.position = a.position;
        b.rotation = a.rotation;
    }

    private void Awake() {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void FixedUpdate() {

        if (isLocalPlayer) {

            if (UnityEngine.XR.XRSettings.enabled) { // was VRSettings.enabled

                if (SteamVR_Rig == null) { //only want this to run first time

                    SteamVR_Rig = gm.vrCameraRig.transform;
                    hmd = gm.hmd;
                    LeftController = gm.leftController;
                    RightController = gm.rightController;

                }

                copyTransform(this.transform, SteamVR_Rig.transform);
                copyTransform(LeftController.transform, LeftHand.transform);
                copyTransform(RightController.transform, RightHand.transform);
                copyTransform(hmd.transform, Head);

                //TODO: add Transform feet and find feet position here

                //TODO: handle controller inputs
                handleControllerInputs();


            }//VRSettings.enabled
            else {
                // enable flying controls for non-VR camera
                float vertical = Input.GetAxis("Vertical");
                float horizontal = Input.GetAxis("Horizontal");
                float mouseY = -Input.GetAxis("Mouse Y");
                float mouseX = Input.GetAxis("Mouse X");

                var cam = gm.nonVRCameraRig;

                cam.transform.Translate(vertical * Time.fixedDeltaTime * Vector3.forward);
                cam.transform.Translate(horizontal * Time.fixedDeltaTime * Vector3.right);
                //cam.transform.Rotate

            }
            // local player calls command to run on server
            CmdSyncPlayer(Head.transform.position, Head.transform.rotation, // sync head
                LeftHand.transform.position, LeftHand.transform.rotation, // sync left hand
                RightHand.transform.position, RightHand.transform.rotation); // sync right hand
        } 
        else { // not local player
            // runs on all other clients and server
            // move to syncvars
            Head.position = headPos;
            Head.rotation = headRot;
            LeftHand.transform.position = leftHandPos;
            LeftHand.transform.rotation = leftHandRot;
            RightHand.transform.position = rightHandPos;
            RightHand.transform.rotation = rightHandRot;
            
        }
    }// FixedUpdate

    // no idea what this is used for, but it's in Johnsen's code, so it's here too :-)
    [Command]
    public void CmdGetAuthority(NetworkIdentity id) {
        id.AssignClientAuthority(this.connectionToClient);
    }

    /**
     * Called by local player, runs on server 
     */
    [Command]
    void CmdSyncPlayer(Vector3 hmdPos, Quaternion hmdRot, Vector3 lhPos, Quaternion lhRot, Vector3 rhPos, Quaternion rhRot) {
        Head.transform.position = hmdPos;
        Head.transform.rotation = hmdRot;
        LeftHand.transform.position = lhPos;
        LeftHand.transform.rotation = lhRot;
        RightHand.transform.position = rhPos;
        RightHand.transform.rotation = rhRot;
        //set syncvars
        headPos = hmdPos;
        headRot = hmdRot;
        leftHandPos = lhPos;
        leftHandRot = lhRot;
        rightHandPos = rhPos;
        rightHandRot = rhRot;
    }

    private void Update()
    {
        //LeftHand.gameObject.transform.position = LeftController.transform.position;
        //LeftHand.gameObject.transform.rotation = LeftController.transform.rotation;

        //RightHand.gameObject.transform.position = RightController.transform.position;
        //RightHand.gameObject.transform.rotation = RightController.transform.rotation;
    }

    private void handleControllerInputs()
    {
        int indexLeft = (int)LeftController.index;
        int indexRight = (int)RightController.index;


        LeftHand.controllerVelocity = getControllerVelocity(LeftController);
        RightHand.controllerVelocity = getControllerVelocity(RightController);
        LeftHand.controllerAngularVelocity = getControllerAngularVelocity(LeftController);
        RightHand.controllerAngularVelocity = getControllerAngularVelocity(RightController);

        float triggerLeft = getTrigger(LeftController);
        float triggerRight = getTrigger(RightController);
        bool a_btn = SteamVR_Controller.Input(indexRight).GetPressDown(Valve.VR.EVRButtonId.k_EButton_A);
      //  interactionZone.a_btn = a_btn;

        Vector2 joyLeft = getJoystick(LeftController);
       // gui.handleInput(joyLeft);
        Vector2 joyRight = getJoystick(RightController);
        drive(joyRight);


    }

    private float getTrigger(SteamVR_TrackedObject controller)
    {
        return controller.index >= 0 ? SteamVR_Controller.Input((int)controller.index).GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).magnitude : 0.0f;
    }

    private Vector2 getJoystick(SteamVR_TrackedObject controller)
    {
        return controller.index >= 0 ? SteamVR_Controller.Input((int)controller.index).GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad) : Vector2.zero;
    }

    private Vector3 getControllerVelocity(SteamVR_TrackedObject controller)
    {
        Vector3 controllerVelocity = controller.index >= 0 ? SteamVR_Controller.Input((int)controller.index).velocity : Vector3.zero;
        return SteamVR_Rig.localToWorldMatrix.MultiplyVector(controllerVelocity.normalized) * controllerVelocity.magnitude;
    }

    private Vector3 getControllerAngularVelocity(SteamVR_TrackedObject controller)
    {
        Vector3 angularVelocity = controller.index >= 0 ? SteamVR_Controller.Input((int)controller.index).angularVelocity : Vector3.zero;
        return SteamVR_Rig.localToWorldMatrix.MultiplyVector(angularVelocity.normalized) * angularVelocity.magnitude;
    }

    public void drive(Vector2 rightJoystick)
    {
        //need to lock y pos
       float rightSpeed = Mathf.Clamp(rightJoystick.y, 0, 1); //y^ x> 
        Vector3 rightDirection = RightHand.transform.forward;
        Vector3 displacement = (rightDirection * rightSpeed) * Time.deltaTime;
        this.transform.Translate(displacement, Space.World);
        
        float rightSpeed2 = Mathf.Clamp(-(rightJoystick.y), 0, 1); //y^ x> 
        Vector3 rightDirection2 = -(RightHand.transform.forward);
        Vector3 displacement2 = (rightDirection2 * rightSpeed2) * Time.deltaTime;
        this.transform.Translate(displacement2, Space.World);

        float rightSpeed3 = Mathf.Clamp(rightJoystick.x, 0, 1); //y^ x> 
        Vector3 rightDirection3 = RightHand.transform.right;
        Vector3 displacement3 = (rightDirection3 * rightSpeed3) * Time.deltaTime;
        this.transform.Translate(displacement3, Space.World);

        float rightSpeed4 = Mathf.Clamp(-(rightJoystick.x), 0, 1); //y^ x> 
        Vector3 rightDirection4 = -(RightHand.transform.right);
        Vector3 displacement4 = (rightDirection4 * rightSpeed4) * Time.deltaTime;
        this.transform.Translate(displacement4, Space.World);
    }

}
