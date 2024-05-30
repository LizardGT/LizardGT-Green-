using ExitGames.Client.Photon;
using GorillaNetworking;
using GorillaTag;
using HarmonyLib;
using StupidTemplate.Notifications;
using Photon.Pun;
using Photon.Realtime;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
using static StupidTemplate.Classes.RigManager;
using static StupidTemplate.Menu.Main;
using System.IO;
using System.Runtime.CompilerServices;


namespace StupidTemplate.Mods
{
    internal class Simple
    {
        public static void AutoSetMaster()
        {
            Tutorial.FlushRPC();
            if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.CustomProperties["gameMode"].ToString().ToLower().Contains("modded"))
            {
                PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
            }
        }

        public static GorillaScoreBoard[] boards = null;
        public static void AntiReportDisconnect()
        {
            try
            {
                foreach (GorillaPlayerScoreboardLine line in GorillaScoreboardTotalUpdater.allScoreboardLines)
                {
                    if (line.linePlayer == NetworkSystem.Instance.LocalPlayer)
                    {
                        Transform report = line.reportButton.gameObject.transform;
                        foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                        {
                            if (vrrig != GorillaTagger.Instance.offlineVRRig)
                            {
                                float D1 = Vector3.Distance(vrrig.rightHandTransform.position, report.position);
                                float D2 = Vector3.Distance(vrrig.leftHandTransform.position, report.position);

                                float threshold = 0.35f;

                                if (D1 < threshold || D2 < threshold)
                                {
                                    PhotonNetwork.Disconnect();
                                    Tutorial.FlushRPC();
                                    NotifiLib.SendNotification("You have disconnected by anti-report pussy");
                                }
                            }
                        }
                    }
                }
            }
            catch { } // Not connected
        }
        public static GameObject NewPointer;
        public static GameObject pointer;
        public static bool espcolor;
        public static void ESP()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (!vrrig.isOfflineVRRig && !vrrig.isMyPlayer && vrrig.mainSkin.material.name.Contains("fected"))
                {
                    vrrig.mainSkin.material.shader = Shader.Find("GUI/Text Shader");
                    if (espcolor == false)
                    {
                        vrrig.mainSkin.material.color = new Color(9f, 0f, 0f, 0.5f);
                    }
                    else
                    {
                        vrrig.playerColor.a = 0.5f;
                        vrrig.mainSkin.material.color = vrrig.playerColor;
                        vrrig.playerColor.a = 1f;
                    }
                }
                else if (!vrrig.isOfflineVRRig && !vrrig.isMyPlayer)
                {
                    vrrig.mainSkin.material.shader = Shader.Find("GUI/Text Shader");
                    vrrig.mainSkin.material.shader = Shader.Find("GUI/Text Shader");
                    if (espcolor == false)
                    {
                        vrrig.mainSkin.material.color = new Color(0f, 9f, 0f, 0.5f);
                    }
                    else
                    {
                        vrrig.playerColor.a = 0.5f;
                        vrrig.mainSkin.material.color = vrrig.playerColor;
                        vrrig.playerColor.a = 1f;
                    }
                }
            }

        }

        public static void BoxESP()
        {
            Tutorial.FlushRPC();
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig != GorillaTagger.Instance.offlineVRRig)
                {
                    UnityEngine.Color thecolor = vrrig.playerColor;
                    GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    box.transform.position = vrrig.transform.position;
                    UnityEngine.Object.Destroy(box.GetComponent<BoxCollider>());
                    box.transform.localScale = new Vector3(0.5f, 0.5f, 0f);
                    box.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                    box.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                    box.GetComponent<Renderer>().material.color = thecolor;
                    UnityEngine.Object.Destroy(box, Time.deltaTime);
                }
            }
        }

        public static void SKIDIBI()
        {
            Application.Quit();
        }

        public static void RigGun()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                Physics.Raycast(GorillaLocomotion.Player.Instance.rightControllerTransform.position, -GorillaLocomotion.Player.Instance.rightControllerTransform.up, out var hitInfo);
                pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                pointer.GetComponent<Renderer>().material.color = new Color32(255, 0, 0, 1);
                pointer.transform.position = hitInfo.point;
                GameObject.Destroy(pointer.GetComponent<BoxCollider>());
                GameObject.Destroy(pointer.GetComponent<Rigidbody>());
                GameObject.Destroy(pointer.GetComponent<Collider>());
                if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.5)
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = false;
                    GorillaTagger.Instance.offlineVRRig.transform.position = NewPointer.transform.position + new Vector3(0, 1, 0);
                    GorillaTagger.Instance.myVRRig.transform.position = NewPointer.transform.position + new Vector3(0, 1, 0);

                    GameObject l = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    UnityEngine.Object.Destroy(l.GetComponent<Rigidbody>());
                    UnityEngine.Object.Destroy(l.GetComponent<SphereCollider>());

                    l.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    l.transform.position = GorillaTagger.Instance.leftHandTransform.position;

                    GameObject r = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    UnityEngine.Object.Destroy(r.GetComponent<Rigidbody>());
                    UnityEngine.Object.Destroy(r.GetComponent<SphereCollider>());

                    r.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    r.transform.position = GorillaTagger.Instance.rightHandTransform.position;

                    l.GetComponent<Renderer>().material.color = Color.green;
                    r.GetComponent<Renderer>().material.color = Color.white;

                    UnityEngine.Object.Destroy(l, Time.deltaTime);
                    UnityEngine.Object.Destroy(r, Time.deltaTime);
                }
                else
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = true;
                }
            }
            if (pointer != null)
            {
                GameObject.Destroy(pointer, Time.deltaTime);
            }
        }

        public static void GrabRig()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                GorillaTagger.Instance.myVRRig.transform.position = GorillaTagger.Instance.rightHandTransform.position;

                GameObject l = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                UnityEngine.Object.Destroy(l.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(l.GetComponent<SphereCollider>());

                l.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                l.transform.position = GorillaTagger.Instance.leftHandTransform.position;

                GameObject r = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                UnityEngine.Object.Destroy(r.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(r.GetComponent<SphereCollider>());

                r.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                r.transform.position = GorillaTagger.Instance.rightHandTransform.position;

                l.GetComponent<Renderer>().material.color = Color.green;
                r.GetComponent<Renderer>().material.color = Color.green;

                UnityEngine.Object.Destroy(l, Time.deltaTime);
                UnityEngine.Object.Destroy(r, Time.deltaTime);
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }

        public static void AntiTag()
        {
            Tutorial.FlushRPC();
            if (GorillaTagger.Instance.offlineVRRig.mainSkin.material.name.Contains("fected") && PhotonNetwork.LocalPlayer.IsMasterClient)
            {
                foreach (GorillaTagManager tagman in GameObject.FindObjectsOfType<GorillaTagManager>())
                {
                    if (tagman.currentInfected.Contains(PhotonNetwork.LocalPlayer))
                    {
                        tagman.currentInfected.Remove(PhotonNetwork.LocalPlayer);
                        GorillaLocomotion.Player.Instance.disableMovement = false;
                    }
                }
            }
        }

        public static void TagOnJoin()
        {
            PlayerPrefs.SetString("tutorial", "false");
            Hashtable h = new Hashtable();
            h.Add("didTutorial", false);
            PhotonNetwork.LocalPlayer.SetCustomProperties(h, null, null);
            PlayerPrefs.Save();
        }

        public static void SpazMonke()
        {
            {
                GorillaTagger.Instance.offlineVRRig.head.rigTarget.eulerAngles = new Vector3((float)UnityEngine.Random.Range(0, 360), (float)UnityEngine.Random.Range(0, 360), (float)UnityEngine.Random.Range(0, 360));
                GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget.eulerAngles = new Vector3((float)UnityEngine.Random.Range(0, 360), (float)UnityEngine.Random.Range(0, 360), (float)UnityEngine.Random.Range(0, 360));
                GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.eulerAngles = new Vector3((float)UnityEngine.Random.Range(0, 360), (float)UnityEngine.Random.Range(0, 360), (float)UnityEngine.Random.Range(0, 360));
                GorillaTagger.Instance.offlineVRRig.head.rigTarget.eulerAngles = new Vector3((float)UnityEngine.Random.Range(0, 360), (float)UnityEngine.Random.Range(0, 180), (float)UnityEngine.Random.Range(0, 180));
                GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget.eulerAngles = new Vector3((float)UnityEngine.Random.Range(0, 360), (float)UnityEngine.Random.Range(0, 180), (float)UnityEngine.Random.Range(0, 180));
                GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.eulerAngles = new Vector3((float)UnityEngine.Random.Range(0, 360), (float)UnityEngine.Random.Range(0, 180), (float)UnityEngine.Random.Range(0, 180));
            }
        }

        public static void GripTagAuraBETA()
        {
            Tutorial.FlushRPC();
            if (ControllerInputPoller.instance.rightGrab)
            {
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    Vector3 they = vrrig.headMesh.transform.position;
                    Vector3 notthem = GorillaTagger.Instance.offlineVRRig.head.rigTarget.position;
                    float distance = Vector3.Distance(they, notthem);

                    if (GorillaTagger.Instance.offlineVRRig.mainSkin.material.name.Contains("fected") && !vrrig.mainSkin.material.name.Contains("fected") && GorillaLocomotion.Player.Instance.disableMovement == false)
                    {
                        if (ControllerInputPoller.instance.rightGrab == true) { GorillaLocomotion.Player.Instance.rightControllerTransform.position = they; } else { GorillaLocomotion.Player.Instance.leftControllerTransform.position = they; }
                    }
                }
            }
        }

        public static void UntagSelf()
        {
            Tutorial.FlushRPC();

            foreach (GorillaTagManager tagman in GameObject.FindObjectsOfType<GorillaTagManager>())
            {
                if (tagman.currentInfected.Contains(PhotonNetwork.LocalPlayer))
                {
                    tagman.currentInfected.Remove(PhotonNetwork.LocalPlayer);
                }
            }

        }

        public static bool rightTrigger = ControllerInputPoller.instance.rightControllerIndexFloat == 0.1;

        public static void UntagAll()
        {
            Tutorial.FlushRPC();
            if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.5)
            {
                foreach (GorillaTagManager tagman in GameObject.FindObjectsOfType<GorillaTagManager>())
                {
                    foreach (Photon.Realtime.Player v in PhotonNetwork.PlayerList)
                    {
                        if (tagman.currentInfected.Contains(v))
                        {
                            tagman.currentInfected.Remove(v);
                        }
                    }
                }
            }
        }

        public static void FastMaster()
        {
            Tutorial.FlushRPC();
            {
                PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
            }
        }

        public static void CrashAll()
        {
            Tutorial.FlushRPC();
            if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.5)
            {
                {
                    Hashtable hashtable = new Hashtable();
                    hashtable[(byte)0] = -1;
                    PhotonNetwork.NetworkingClient.OpRaiseEvent(207, hashtable, null, SendOptions.SendReliable);
                    //TPPTPTPPT
                }
            }
        }

        public static void UnacidSelf()
        {
            Tutorial.FlushRPC();
            ScienceExperimentManager.instance.photonView.RPC("PlayerHitByWaterBalloonRPC", RpcTarget.All, new object[]
            {
                PhotonNetwork.LocalPlayer.ActorNumber
            });
        }

        private static GameObject Rplat = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        private static GameObject Lplat = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        private static bool IsNotHoldingR;
        private static bool IsSpawnedR;
        private static bool IsNotHoldingL;
        private static bool IsSpawnedL;
        public static void Platforms()
        {

            if (ControllerInputPoller.instance.rightGrab)
            {
                if (!IsNotHoldingR && !IsSpawnedR)
                {
                    Rplat.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.transform.position;
                    IsSpawnedR = true;
                }
                IsNotHoldingR = false;
                Rplat.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                Rplat.AddComponent<BoxCollider>();
            }
            else if (!ControllerInputPoller.instance.rightGrab)
            {
                Rplat.transform.position = new Vector3(0, 100, 0);
                IsNotHoldingR = true;
                IsSpawnedR = false;
            }

            if (ControllerInputPoller.instance.leftGrab)
            {
                if (!IsNotHoldingL && !IsSpawnedL)
                {
                    Lplat.transform.position = GorillaLocomotion.Player.Instance.leftControllerTransform.transform.position;
                    IsSpawnedL = true;
                }
                IsNotHoldingL = false;
                Lplat.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                Lplat.AddComponent<BoxCollider>();
                Lplat.transform.position = GorillaLocomotion.Player.Instance.leftControllerTransform.transform.position;
            }
            else if (!ControllerInputPoller.instance.leftGrab)
            {
                Lplat.transform.position = new Vector3(0, 100, 0);
                IsNotHoldingL = true;
                IsSpawnedL = false;
            }
        }



        public static void NoClip()
        {
            if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.1f)
            {
                {
                    MeshCollider[] array = Resources.FindObjectsOfTypeAll<MeshCollider>();
                    foreach (MeshCollider meshCollider in array)
                    {
                        meshCollider.enabled = false;
                    }
                }
            }
            else
            {
                {
                    MeshCollider[] array = Resources.FindObjectsOfTypeAll<MeshCollider>();
                    foreach (MeshCollider meshCollider in array)
                    {
                        meshCollider.enabled = true;
                    }
                }
            }
        }

        

        public static bool GunM = rightGrab || Mouse.current.rightButton.isPressed;

        public static void TpGun()
        {

            if (ControllerInputPoller.instance.rightGrab)
            {


                Physics.Raycast(GorillaLocomotion.Player.Instance.rightControllerTransform.position, -GorillaLocomotion.Player.Instance.rightControllerTransform.up, out var hitInfo);
                pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                pointer.GetComponent<Renderer>().material.color = new Color32(102, 255, 0, 0);
                pointer.transform.position = hitInfo.point;
                GameObject.Destroy(pointer.GetComponent<BoxCollider>());
                GameObject.Destroy(pointer.GetComponent<Rigidbody>());
                GameObject.Destroy(pointer.GetComponent<Collider>());
                if (ControllerInputPoller.instance.rightControllerIndexFloat >= 0.1)
                {
                    GameObject.Destroy(pointer, Time.deltaTime);
                    GorillaLocomotion.Player.Instance.transform.position = pointer.transform.position;
                    GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = pointer.transform.position;
                }

                if (pointer != null)
                {
                    GameObject.Destroy(pointer, Time.deltaTime);
                }

            }

        }

        public static void Fly()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                GorillaLocomotion.Player.Instance.transform.position += (GorillaLocomotion.Player.Instance.headCollider.transform.forward * Time.deltaTime) * 15;
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
                GorillaTagger.Instance.handTapVolume = 80;
            }
        }
        public static bool rightGrip = ControllerInputPoller.instance.rightGrab;
        public static bool leftPrimary = ControllerInputPoller.instance.leftControllerPrimaryButton;

        public static void LeftFly()
        {
            if (ControllerInputPoller.instance.leftGrab)
            {
                GorillaLocomotion.Player.Instance.transform.position += (GorillaLocomotion.Player.Instance.headCollider.transform.forward * Time.deltaTime) * 15;
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
                GorillaTagger.Instance.handTapVolume = 80;
            }
        }

        public static void LeftTriggerFly()
        {
            if (ControllerInputPoller.instance.leftControllerIndexFloat > 0.5)
            {
                GorillaLocomotion.Player.Instance.transform.position += (GorillaLocomotion.Player.Instance.headCollider.transform.forward * Time.deltaTime) * 15;
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
                GorillaTagger.Instance.handTapVolume = 80;
            }
        }
        public static bool leftTrigger = ControllerInputPoller.instance.leftControllerIndexFloat > 0.5;
        public static bool rightSecB = ControllerInputPoller.instance.rightControllerSecondaryButton;
        public static bool rightGrab = ControllerInputPoller.instance.rightGrab;

        public static void TriggerFly()
        {
            if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.5)
            {
                GorillaLocomotion.Player.Instance.transform.position += (GorillaLocomotion.Player.Instance.headCollider.transform.forward * Time.deltaTime) * 15;
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
                GorillaTagger.Instance.handTapVolume = 80;
            }
        }

       

        public static void GhostMonke()
        {
            bool Primary = ControllerInputPoller.instance.rightControllerSecondaryButton;
            {
                if (Primary == true)
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = false;

                    GameObject l = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    UnityEngine.Object.Destroy(l.GetComponent<Rigidbody>());
                    UnityEngine.Object.Destroy(l.GetComponent<SphereCollider>());

                    l.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    l.transform.position = GorillaTagger.Instance.leftHandTransform.position;

                    GameObject r = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    UnityEngine.Object.Destroy(r.GetComponent<Rigidbody>());
                    UnityEngine.Object.Destroy(r.GetComponent<SphereCollider>());

                    r.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    r.transform.position = GorillaTagger.Instance.rightHandTransform.position;



                    UnityEngine.Object.Destroy(l, Time.deltaTime);
                    UnityEngine.Object.Destroy(r, Time.deltaTime);
                }
                else
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = true;
                }
            }
        }

    }
}
