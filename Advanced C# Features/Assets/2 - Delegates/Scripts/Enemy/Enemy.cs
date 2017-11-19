using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Delegates
{
    public class Enemy : MonoBehaviour
    {
        public enum Behaviour
        {
            IDLE = 0,
            SEEK = 1
        }

        // Defining a delegate
        delegate void BehaviourFunc();

        public Behaviour behaviourIndex = Behaviour.SEEK;
        public Transform target;

        // Create a delegates list
        private List<BehaviourFunc> behaviourFuncs = new List<BehaviourFunc>();
        private NavMeshAgent agent;

        void Awake ()
        {
            agent = GetComponent<NavMeshAgent>();

            // Setup behaviours
            behaviourFuncs.Add(Idle);
            behaviourFuncs.Add(Seek);
        }

        void Idle ()
        {
            // Stop the NavAgent
            agent.Stop();
        }

        void Seek ()
        {
            // Resume the NavAgent
            agent.Resume();

            if (target != null)
            {
                agent.SetDestination(target.position);
            }
        }

        public void SetTarget (Transform target)
        {
            this.target = target;
        }

        public bool IsCloseToTarget (float distance)
        {
            if (target != null)
            {
                float distToTarget = Vector3.Distance(transform.position, target.position);

                if (distToTarget <= distance)
                {
                    return true;
                }
            }

            return false;
        }

        // Update is called once per frame
        protected virtual void Update ()
        {
            // Call current behvaiour function
            behaviourFuncs[(int)behaviourIndex]();
        }
    }
}
