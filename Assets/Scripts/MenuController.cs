using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

namespace Game
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField, Header("Inputs")] private InputActionReference menuInputActionReference;
        [SerializeField] private InputActionReference DevInputActionReference;
        [SerializeField] private InputActionReference DevInputActionReference2;
        [SerializeField] private DefaultPhysicsValues defaultPhysicsValues;

        [Header("Menu Components")]
        public GameObject tablet;
        public GameObject numPad;
        public GameObject helpPad;
        public TextMeshProUGUI airtimeText;
        public TextMeshProUGUI impactForceText;

        [Header("Sliders")]
        [SerializeField] private Slider massSlider;
        [SerializeField] private Slider gravitySlider;
        [SerializeField] private Slider angleSlider;
        [SerializeField] private Slider launchForceSlider;
        [SerializeField] private Slider launchHeightSlider;

        [Header("Test Values")]
        public InputAction setValuesKey;
        [SerializeField] private DefaultPhysicsValues TimeToGroundTest;

        private Vector3 initialLocalPos;
        private Quaternion initialLocalRot;
        private Vector3 initialLocalScale;
        private GameObject PlayerCam;
        private ObjectLauncher objectLauncher;

        //Text variables
        private const string defaultAirtimeText = "Airtime until impact is: ";
        private const string defaultImpactText = "Force of Initial Impact: ";
        private bool airtimeSet = false;
        private bool impactSet = false;

        private static MenuController menuController;

        public static MenuController GetMenuController
        {
            get { return menuController; }
            set { menuController = value; }
        }

        private void Awake()
        {
            if (menuController != null)
                Destroy(gameObject);
            else
                menuController = this;

            initialLocalPos = transform.localPosition;
            initialLocalRot = transform.localRotation;
            initialLocalScale = transform.localScale;
            PlayerCam = transform.parent.gameObject;
            //Debug.Log(PlayerCam.name);
            transform.parent = null;
            
        }

        private void Start()
        {
            menuInputActionReference.action.started += MenuPressed;
            objectLauncher = ObjectLauncher.GetObjectLauncher;
            objectLauncher.ResetPos();
            SetSliderValues(defaultPhysicsValues.GetAllValues());
        }

        private void ResetMenuToCam()
        {
            transform.parent = PlayerCam.transform;
            transform.localPosition = initialLocalPos;
            transform.localRotation = initialLocalRot;
            transform.localScale = initialLocalScale;
            transform.parent = null;
            
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Keyboard.current.hKey.wasReleasedThisFrame)
            {
                Debug.Log("Setting values to TimeToGroundTest");
                SetSliderValues(TimeToGroundTest.GetAllValues());
                objectLauncher.ResetPos();
            }

            if (Keyboard.current.lKey.wasReleasedThisFrame)
            {
                Debug.Log("Firing");
                objectLauncher.Fire();
            }

            if (Keyboard.current.rKey.wasReleasedThisFrame)
            {
                Debug.Log("Resetting projectile");
                objectLauncher.ResetPos();
                ResetText();
            }

            if (Keyboard.current.qKey.wasReleasedThisFrame)
            {
                Debug.Log("Setting values to default values");
                SetSliderValues(defaultPhysicsValues.GetAllValues());
                objectLauncher.ResetPos();
            }

            if (DevInputActionReference.action.triggered && DevInputActionReference2.action.triggered)
            {
                Debug.Log("BothPressed");
                tablet.SetActive(!tablet.activeSelf);
            }
        }
#endif

        private void SetSliderValues(float[] newValues)
        {
            massSlider.value = newValues[0];
            gravitySlider.value = newValues[1];
            angleSlider.value = newValues[2];
            launchForceSlider.value = newValues[3];
            launchHeightSlider.value = newValues[4];
        }

        public void MenuPressed(InputAction.CallbackContext context)
        {
            Debug.Log("MenuPressed!");
            tablet.SetActive(!tablet.activeSelf);

            if (tablet.activeSelf)
                ResetMenuToCam();
        }

        public void ToggleKeypad()
        {
            numPad.SetActive(!numPad.activeSelf);
        }

        public void ToggleHelpPad()
        {
            helpPad.SetActive(!helpPad.activeSelf);
        }

        public void SetAirtimeText(string airtime)
        {
            if (airtime == "0")
            {
                airtimeText.text = defaultAirtimeText + "0";
                airtimeSet = false;
                return;
            }


            if (!airtimeSet)
            {
                airtimeText.text = defaultAirtimeText + airtime;
                airtimeSet = true;
            }
        }

        public void SetImpactText(string impactForce)
        {
            if (impactForce == "0")
            {
                impactForceText.text = defaultImpactText + "0";
                impactSet = false;
                return;
            }


            if (!impactSet)
            {
                impactForceText.text = defaultImpactText + impactForce;
                impactSet = true;
            }
        }

        public void ResetText()
        {
            airtimeText.text = defaultAirtimeText + "0";
            airtimeSet = false;

            impactForceText.text = defaultImpactText + "0";
            impactSet = false;
        }

        public void UpdateProjectileMass()
        {
            objectLauncher.UpdateProjectileMass(massSlider.value);
        }

        public void UpdateProjectileAngle()
        {
            objectLauncher.LaunchAngle(angleSlider.value);
        }

        public void UpdateProjectileForce()
        {
            objectLauncher.LaunchForce(launchForceSlider.value);
        }

        public void UpdateProjectileGravity()
        {
            objectLauncher.UpdateProjectileGravity(gravitySlider.value);
        }

        public void UpdateProjectileHeight()
        {
            objectLauncher.StartHeight(launchHeightSlider.value);
        }
    }
}
