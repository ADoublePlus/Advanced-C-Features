using System.Collections;
using UnityEngine;

namespace Delegates
{
    public class Orc : Enemy
    {
        public GameObject attackBox;
        public float attackRange = 2f;
        public float meleeSpeed = 20f;
        public float meleeDelay = .3f;

        private bool isAttacking = false;

        // Update is called once per frame
        protected override void Update ()
        {
            // Call super's update
            base.Update();

            // IF not attacking AND target is with attackRange
            if (!isAttacking && IsCloseToTarget(attackRange))
            {
                StartCoroutine(Attack());
            }
        }

        IEnumerator Attack ()
        {
            isAttacking = true;
            attackBox.SetActive(true);
            behaviourIndex = Behaviour.IDLE;
            yield return new WaitForSeconds(meleeDelay);
            behaviourIndex = Behaviour.SEEK;
            attackBox.SetActive(false);
            isAttacking = false;
        }
    }
} 