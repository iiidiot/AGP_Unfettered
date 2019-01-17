using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIHandling : MonoBehaviour 
{
    private ThirdPersonCamera.CameraController cc;
    private ThirdPersonCamera.FreeForm freeForm;

    public Toggle optionCameraEnabled;
    public Toggle optionSmartPivot;
    public Toggle optionOcclusionCheck;
    public Toggle optionThicknessCheck;
    public Toggle optionControllerEnabled;
    public Toggle optionControllerInvertY;
    public Toggle optionMouseInvertY;

    public Dropdown optionCameraMode;

    public InputField inputDesiredDistance;
    public InputField inputMaxThickness;
    public InputField inputZoomOutStepValue;
    public InputField inputCollisionDistance;

    public Slider mouseSliderX;
    public Slider mouseSliderY;
    public Slider controllerSliderX;
    public Slider controllerSliderY;

    private bool ignoreChanges;

	void Awake() 
    {
        ignoreChanges = true;

        cc = Camera.main.GetComponent<ThirdPersonCamera.CameraController>();
        freeForm = Camera.main.GetComponent<ThirdPersonCamera.FreeForm>();

        
        optionSmartPivot.isOn = cc.smartPivot;
        optionOcclusionCheck.isOn = cc.occlusionCheck;

        inputDesiredDistance.text = cc.desiredDistance.ToString();
        inputMaxThickness.text = cc.maxThickness.ToString();
        inputZoomOutStepValue.text = cc.zoomOutStepValue.ToString();
        inputCollisionDistance.text = cc.collisionDistance.ToString();
        
        optionThicknessCheck.isOn = cc.thicknessCheck;

        if (freeForm != null)
        {
            optionCameraEnabled.isOn = freeForm.cameraEnabled;
            optionControllerEnabled.isOn = freeForm.controllerEnabled;
            optionControllerInvertY.isOn = freeForm.controllerInvertY;
            optionMouseInvertY.isOn = freeForm.mouseInvertY;

            if (freeForm.cameraMode == ThirdPersonCamera.CameraMode.Hold)
                optionCameraMode.value = 0;
            else
                optionCameraMode.value = 1;

            mouseSliderX.value = freeForm.mouseSensitivity.x;
            mouseSliderY.value = freeForm.mouseSensitivity.y;

            controllerSliderX.value = freeForm.controllerSensitivity.x;
            controllerSliderY.value = freeForm.controllerSensitivity.y;
        }

        ignoreChanges = false;
	}

    public void HandleUI()
    {
        if (ignoreChanges)
            return;
        
        cc.smartPivot = optionSmartPivot.isOn;

        if (!cc.smartPivot)
        {
            cc.cameraNormalMode = true;
        }
            
        cc.occlusionCheck = optionOcclusionCheck.isOn;
        cc.thicknessCheck = optionThicknessCheck.isOn;

        float newDistance = 0.0f;
        if (float.TryParse(inputDesiredDistance.text, out newDistance))
            cc.desiredDistance = newDistance;

        float newThickness = 0.0f;
        if (float.TryParse(inputMaxThickness.text, out newThickness))
            cc.maxThickness = newThickness;

        float newStep = 0.0f;
        if (float.TryParse(inputZoomOutStepValue.text, out newStep))
            cc.zoomOutStepValue = newStep;

        float newCd = 0.0f;
        if (float.TryParse(inputCollisionDistance.text, out newCd))
            cc.collisionDistance = newCd;

        if (freeForm != null)
        {
            if (optionCameraMode.value == 0)
                freeForm.cameraMode = ThirdPersonCamera.CameraMode.Hold;
            else
                freeForm.cameraMode = ThirdPersonCamera.CameraMode.Always;

            freeForm.controllerEnabled = optionControllerEnabled.isOn;
            freeForm.controllerInvertY = optionControllerInvertY.isOn;
            freeForm.mouseInvertY = optionMouseInvertY.isOn;

            freeForm.cameraEnabled = optionCameraEnabled.isOn;
            freeForm.mouseSensitivity.x = mouseSliderX.value;
            freeForm.mouseSensitivity.y = mouseSliderY.value;

            freeForm.controllerSensitivity.x = controllerSliderX.value;
            freeForm.controllerSensitivity.y = controllerSliderY.value;
        }
    }

    public void Update()
    {
        if (freeForm != null)
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(Input.mousePosition.x, Input.mousePosition.y), Vector2.zero);

            if (hit.collider != null && hit.collider.name == "Collider")
            {
                freeForm.cameraEnabled = false;
                optionCameraEnabled.isOn = false;
            }
            else
            {
                freeForm.cameraEnabled = true;
                optionCameraEnabled.isOn = true;
            }

            if (Input.GetKey(KeyCode.Escape))
            {
                if (freeForm.cameraMode == ThirdPersonCamera.CameraMode.Always)
                {
                    freeForm.cameraMode = ThirdPersonCamera.CameraMode.Hold;
                    optionCameraMode.value = 0;
                }

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }

        ignoreChanges = true;
        inputDesiredDistance.text = cc.desiredDistance.ToString();
        ignoreChanges = false;
    }
}