using System;
using UnityEngine;

namespace Animation
{
    public class EnemyAnimationController : AnimationController
    {
        private int _idleParamID;
        private int _flyParamID;
        private int _chaseParamID;
        private int _attackParamID;
        private int _deadParamID;

        public Action OnStartAttack { get; set; }
        public Action OnAttacked { get; set; }

        protected override void InitializeParams()
        {
            _idleParamID = Animator.StringToHash("idle");
            _flyParamID = Animator.StringToHash("fly");
            _chaseParamID = Animator.StringToHash("chase");
            _attackParamID = Animator.StringToHash("attack");
            _deadParamID = Animator.StringToHash("dead");
        }

        public void Idle()
        {
            _animator.SetTrigger(_idleParamID);
        }

        public void Fly()
        {
            _animator.SetTrigger(_flyParamID);
        }

        public void Attack()
        {
            _animator.SetTrigger(_attackParamID);
        }

        public void Chase()
        {
            _animator.SetTrigger(_chaseParamID);
        }

        public void Dead()
        {
            _animator.SetTrigger(_deadParamID);
        }

        public void StartAttack()
        {
            OnStartAttack?.Invoke();
        }

        public void OnAttack()
        {
            OnAttacked?.Invoke();
        }
    }
}