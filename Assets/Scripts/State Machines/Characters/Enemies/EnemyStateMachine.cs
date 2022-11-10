using Animation;
using BzKovSoft.ObjectSlicer;
using CameraExtensions;
using Main.Level;
using System;
using Triggers;
using UnityEngine;
using Weapons;
using Weapons.Enemy;
using Zenject;

namespace States.Characters.Enemy
{
    public enum AttackType { Melee, Range }

    public class EnemyStateMachine : CharacterStateMachine, ISlowMotionActivator
    {
        [Inject] public EnemyAnimationController AnimationController { get; private set; }

        [SerializeField] private BzSliceableBase _slicer;
        public BzSliceableBase Slicer => _slicer;

        [Header("Movement")]
        [SerializeField] private bool _movable;
        public bool Movable => _movable;
        [SerializeField, NaughtyAttributes.ShowIf(nameof(_movable))] private float _moveSpeed = 2f;
        public float MoveSpeed => _moveSpeed;
        [SerializeField, NaughtyAttributes.ShowIf(nameof(_movable))] private float _rotationSpeed = 90f;
        public float RotationSpeed => _rotationSpeed;
        [SerializeField] private bool _canVerticalRotate;
        public bool CanVerticalRotate => _canVerticalRotate;

        [Header("Damage")]
        [SerializeField] private AttackType _attackType;
        public AttackType AttackType => _attackType;
        [SerializeField] private float _agroRange;
        public float AgroRange => _agroRange;
        [SerializeField] private float _minAttackRange;
        public float MinAttackRange => _minAttackRange;
        [SerializeField] private float _attackRange;
        public float AttackRange => _attackRange;
        [SerializeField] private SphereCollider _agroTrigger;
        public SphereCollider AgroTirgger => _agroTrigger;
        [SerializeField] private CollisionHandler _collisionHandler;
        public CollisionHandler CollisionHandler => _collisionHandler;
        [SerializeField] private CollisionHandler _agroCollisionHandler;
        public CollisionHandler AgroCollisionHandler => _agroCollisionHandler;

        [SerializeField] private LayerMask _targetLayer;
        public LayerMask TargetLayer => _targetLayer;
        [SerializeField] private int _collisionDamage;
        public int CollisionDamage => _collisionDamage;
        [SerializeField] private int _attackDamage;
        public int AttackDamage => _attackDamage;
        [SerializeField] private float _attackDelay = 1f;
        public float AttackDelay => _attackDelay;
        [SerializeField, NaughtyAttributes.ShowIf(nameof(_attackType), AttackType.Range)] private EnemyWeapon _weapon;
        public EnemyWeapon Weapon => _weapon;
        
        [Header("Flying")]
        [SerializeField] private bool _flying;
        public bool Flying => _flying;
        [SerializeField, NaughtyAttributes.ShowIf(nameof(_flying))] private float _flyAmplitude = 1f;
        public float FlyingAmplitude => _flyAmplitude;
        [SerializeField, NaughtyAttributes.ShowIf(nameof(_flying))] private float _flyPeriod = 1f;
        public float FlyingPeriod => _flyPeriod;
        public Vector3 StartPosition { get; private set; }

        [Header("Dead")]
        [SerializeField] private int _ragePoints = 20;
        public int RagePoints => _ragePoints;
        [SerializeField] private int _coinsReward = 100;
        public bool IsDead { get; private set; }


        [Inject] private LevelRewardHandler _levelRewardHandler;

        protected new EnemyStateFactory States { get; private set; }

        public Action OnDisabled { get; set; }

        protected override void InitializeState()
        {
            States = _factory.Create<EnemyStateFactory>(this);
            CurrentState = States.Neutral();
            CurrentState.Enter();
        }

        protected override void Start()
        {
            base.Start();
            StartPosition = transform.position;
        }

        public override void Dead()
        {
            base.Dead();
            IsDead = true;
            _levelRewardHandler.AddCoinsToReward(_coinsReward);
            OnDisabled?.Invoke();
        }
    }
}