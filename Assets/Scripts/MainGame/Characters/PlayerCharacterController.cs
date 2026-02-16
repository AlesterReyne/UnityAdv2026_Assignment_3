using Data;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace MainGame.Characters
{
    public class PlayerCharacterController : MonoBehaviour
    {
        public event UnityAction<int> OnTakeDamageEventAction;
        [SerializeField] private UnityEvent<int> onTakeDamageEvent;

        [Header("Navigation")] [SerializeField]
        private NavMeshAgent navMeshAgent;

        [SerializeField] private Transform waypoint;
        [SerializeField] private Transform[] pathWaypoints;
        [SerializeField] private Animator animator;
        [SerializeField] private Camera mainCamera;


        private bool _isMoving = true;
        private int _currentWaypointIndex = 0;
        private readonly bool _hasBloodyBoots = true;
        private readonly float _maxDistanceOfRaycast = 100f;

        private int _hp;
        private int _startingHp;

        public int Hp
        {
            get => _hp;
            set => _hp = value;
        }

        public int CurrentWaypointIndex
        {
            get => _currentWaypointIndex;
            set => _currentWaypointIndex = value;
        }

        public void ToggleMoving(bool shouldMove)
        {
            _isMoving = shouldMove;
            if (navMeshAgent) navMeshAgent.enabled = shouldMove;
        }

        public void SetDestination(Transform targetTransformWaypoint)
        {
            if (navMeshAgent)
                navMeshAgent.SetDestination(targetTransformWaypoint.position);
        }

        public void SetDestination(int waypointIndex)
        {
            SetDestination(pathWaypoints[waypointIndex]);
        }

        public void TakeDamage(int damageAmount)
        {
            _hp -= damageAmount;
            float hpPercentLeft = (float)_hp / _startingHp;
            animator.SetLayerWeight(1, (1 - hpPercentLeft));
            onTakeDamageEvent.Invoke(_hp);
            OnTakeDamageEventAction?.Invoke(_hp);
        }

        private void Start()
        {
            if (!mainCamera)
            {
                Debug.Log($"Please set camera to {gameObject.name}");
            }

            _hp = 100;
            _startingHp = _hp;
            SetMudAreaCost();
            ToggleMoving(true);
            SetDestination(pathWaypoints[0]);
        }

        private void SetMudAreaCost()
        {
            if (_hasBloodyBoots)
            {
                navMeshAgent.SetAreaCost(3, 1);
            }
        }

        [ContextMenu("Take Damage Test")]
        private void TakeDamageTesting()
        {
            TakeDamage(10);
        }


        private void Update()
        {
            if (_isMoving && !navMeshAgent.isStopped && navMeshAgent.remainingDistance <= 0.1f)
            {
                _currentWaypointIndex++;
                if (_currentWaypointIndex >= pathWaypoints.Length)
                    _currentWaypointIndex = 0;
                SetDestination(pathWaypoints[_currentWaypointIndex]);
            }

            if (animator)
                animator.SetFloat(AnimatorNames.Speed, navMeshAgent.velocity.magnitude);

            if (mainCamera)
            {
                Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.value);
                if (Physics.Raycast(ray, out RaycastHit hit, _maxDistanceOfRaycast))
                {
                    //We want to know what the mouse is hovering now
                    Debug.Log($"Hit: {hit.collider.name}");
                }
            }
        }
    }
}