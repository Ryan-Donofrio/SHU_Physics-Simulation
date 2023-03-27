using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

    public class DisableOthersScripts : MonoBehaviourPun
    {
        public GameObject XRInteractionManager;
        public GameObject LocomotionSystem;

        void Start()
        {
            PhotonView PV = GetComponent<PhotonView>();

            if (PV.IsMine)
            {
                gameObject.tag = "Player";
            }
            else
            {
                Destroy(XRInteractionManager);
                Destroy(LocomotionSystem);
                Destroy(this);
            }
        }
    }

