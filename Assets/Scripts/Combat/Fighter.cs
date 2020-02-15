using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] Health target;
        [SerializeField] float timeBetweenAttack = 1f;
        [SerializeField] float weaponDamage = 5f;
        float timeSinceLastAttack = Mathf.Infinity;
        NavMeshAgent navMeshAgent;
        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;
            if (target.IsDeath()) return;
            if (target != null && !GetInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
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
                AttackTrigger();
                transform.LookAt(target.transform);
                timeSinceLastAttack = 0;
            }
        }

        private void AttackTrigger()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) { return false; }
            Health health = combatTarget.GetComponent<Health>();
            return health != null && !health.IsDeath();
        }
        private bool GetInRange()
        {
           // Debug.Log(target.name, target);
            return Vector3.Distance(target.transform.position, transform.position) < weaponRange;
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }
        public void Cancel()
        {
            StopAttack();
            target = null;
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

        public void Hit()
        {
            if (target == null) return;
            target.TakeDamage(weaponDamage);
        }
    }
}
