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
        public GameObject teacherStats;
        public TextMeshProUGUI airtimeText;
        public TextMeshProUGUI impactForceText;
        public TextMeshProUGUI maxHeightText;
        public TextMeshProUGUI horizontalDistance;

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
        public GameObject rockObject;
        private RockManager rockscript;

        //Text variables
        private const string defaultAirtimeText = "Airtime until impact is: ";
        private const string defaultImpactText = "Force of Initial Impact: ";
        private const string defaultHorizontalDistanceText = "Total Horizontal Distance: ";
        private const string defaultVerticleDistanceText = "Peak Vertical Distance: ";
        private bool airtimeSet = false;
        private bool impactSet = false;
        private bool horizontalSet = false;
        private bool verticleSet = false;
        

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
            rockscript = rockObject.GetComponent<RockManager>();
            rockscript.ResetPos();
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
                rockscript.ResetPos();
            }

            if (Keyboard.current.lKey.wasReleasedThisFrame)
            {
                Debug.Log("Firing");
                rockscript.Fire();
            }

            if (Keyboard.current.rKey.wasReleasedThisFrame)
            {
                Debug.Log("Resetting projectile");
                rockscript.ResetPos();
                ResetText();
            }

            if (Keyboard.current.qKey.wasReleasedThisFrame)
            {
                Debug.Log("Setting values to default values");
                SetSliderValues(defaultPhysicsValues.GetAllValues());
                rockscript.ResetPos();
            }

            if (DevInputActionReference.action.triggered && DevInputActionReference2.action.triggered)
            {
                Debug.Log("BothPressed");
                tablet.SetActive(!tablet.activeSelf);
            }

            if (Keyboard.current.spaceKey.wasReleasedThisFrame)
            {
                FindObjectOfType<SimulationGameManager>().OutputNetworkingInfo();
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

        public void ToggleTeacherStats()
        {
            teacherStats.SetActive(!teacherStats.activeSelf);
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
                
            }


            else
            {
                impactForceText.text = defaultImpactText + impactForce;
                impactSet = true;
                return;
            }
        }

        public void SetHorisontalDistance(float distance)
        {
            horizontalDistance.text = defaultHorizontalDistanceText + distance;
        }
        

        public void SetMaxHeightText(float maxHeight)
        {
               maxHeightText.text = defaultVerticleDistanceText + maxHeight;
        }

        public void ResetText()
        {
            airtimeText.text = defaultAirtimeText + "0";
            airtimeSet = false;

            impactForceText.text = defaultImpactText + "0";
            impactSet = false;

            maxHeightText.text = defaultVerticleDistanceText + "0";

            horizontalDistance.text = defaultHorizontalDistanceText + "0";
        }

        public void UpdateProjectileMass()
        {
            rockscript.UpdateProjectileMass(massSlider.value);
        }

        public void UpdateProjectileAngle()
        {
            rockscript.LaunchAngle(angleSlider.value);
        }

        public void UpdateProjectileForce()
        {
            rockscript.LaunchForce(launchForceSlider.value);
        }

        public void UpdateProjectileGravity()
        {
            rockscript.UpdateProjectileGravity(gravitySlider.value);
        }

        public void UpdateProjectileHeight()
        {
            rockscript.StartHeight(launchHeightSlider.value);
        }
    }
}
