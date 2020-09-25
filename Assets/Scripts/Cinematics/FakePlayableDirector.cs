using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Cinematics
{
    public class FakePlayableDirector : MonoBehaviour
    {
        public event Action<float> onFinish;
        // Start is called before the first frame update
        void Start()
        {
            Invoke("OnFinish", 3f);
        }

        // Update is called once per frame
        void OnFinish()
        {
            onFinish(3.2f);
        }
    }
}

