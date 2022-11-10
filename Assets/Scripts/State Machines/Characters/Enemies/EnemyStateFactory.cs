using Game.Systems;
using UnityEngine;

namespace States.Characters.Enemy
{
    public class EnemyStateFactory : CharacterStateFactory
    {
        protected new EnemyStateMachine Machine => base.Machine as EnemyStateMachine;

        public EnemyStateFactory(EnemyStateMachine machine, State.ZenFactory stateFactory) : base(machine, stateFactory) { }

        public override State Idle()
        {
            return StateFactory.Create<EnemyIdleState>(Machine, this);
        }

        public override State Move()
        {
            return StateFactory.Create<EnemyMoveState>(Machine, this);
        }

        public override State Hurt()
        {
            return StateFactory.Create<EnemyHurtState>(Machine, this);
        }

        public override State Neutral()
        {
            return StateFactory.Create<EnemyNeutralState>(Machine, this);
        }

        public State Dead(bool sliced)
        {
            return StateFactory.Create<EnemyDeadState>(Machine, this, sliced);
        }

        public State Battle(HealthSystem target)
        {
            return StateFactory.Create<EnemyBattleState>(Machine, this, target);
        }

        public State Chase(HealthSystem target)
        {
            return StateFactory.Create<EnemyChaseState>(Machine, this, target);
        }

        public State Attack(HealthSystem target)
        {
            return StateFactory.Create<EnemyAttackState>(Machine, this, target);
        }
    }
}