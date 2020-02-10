using RPG.Core;
using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] Transform target;

        private void Update()
        {
            if (target == null) return;
            if (target != null && !GetInRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
            }
        }

        private bool GetInRange()
        {
           // Debug.Log(target.name, target);
            return Vector3.Distance(target.position, transform.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }
        public void Cancel()
        {
            target = null;
        }
    }
}
