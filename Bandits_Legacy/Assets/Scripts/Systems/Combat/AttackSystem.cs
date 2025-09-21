using Entitas;

namespace Systems.Combat
{
    public sealed class AttackSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _attackers;
        
        public AttackSystem(Contexts contexts)
        {
            var matches = new[]
            {
                GameMatcher.Weapon,
                GameMatcher.AttackRequested
            };
            var excludes = new[]
            {
                GameMatcher.Attacking,
                GameMatcher.Dead
            };
            _attackers = contexts.game.GetGroup(GameMatcher.AllOf(matches).NoneOf(excludes));
        }

        public void Execute()
        {
            foreach (var entity in _attackers.GetEntities())
            {
                if(!entity.hasWeapon || !entity.isAttackRequested || entity.isAttackDelaying)
                    continue;
                entity.isAttackRequested = false;
                entity.AddAttacking(true);
                entity.isAttackDelaying = true;
                entity.ReplaceAttackDelayTimer(entity.weapon.Delay);
            }
        }
    }
}