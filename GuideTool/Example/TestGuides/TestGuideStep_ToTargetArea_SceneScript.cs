using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hont
{
    public class TestGuideStep_ToTargetArea_SceneScript : MonoBehaviour
    {
        public TestGuideStep_ToTargetArea scriptableObject;
        public string compareTag = "Player";


        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(compareTag)) return;

            scriptableObject.TriggeredEnterTargetArea();
        }
    }
}
