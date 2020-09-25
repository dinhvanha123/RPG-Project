using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
namespace RPG.Cinematics
{
    public class CinematicsTrigger : MonoBehaviour
    {
        bool alreadyTrigger = false;
        public void OnTriggerEnter(Collider other)
        {
            if (!alreadyTrigger && other.gameObject.tag == "Player")
            {
                GetComponent<PlayableDirector>().Play();
                alreadyTrigger = true;

            }
        }
    }
}
