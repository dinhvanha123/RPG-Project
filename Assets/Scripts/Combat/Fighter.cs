using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] Transform target;
        [SerializeField] float timeBetweenAttack = 1f;
        [SerializeField] float weaponDamage = 5f;
        float timeSinceLastAttack = 0f;

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;
            if (target != null && !GetInRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            if (timeSinceLastAttack > timeBetweenAttack)
            {
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0;
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
        public void Hit()
        {
            Debug.Log("hit");
            target.GetComponent<Health>().TakeDamage(weaponDamage);
        }
    }
}
