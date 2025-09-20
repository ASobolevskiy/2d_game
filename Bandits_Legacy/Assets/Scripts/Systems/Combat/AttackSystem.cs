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
            _attackers = contexts.game.GetGroup(GameMatcher.AllOf(matches).NoneOf(GameMatcher.Dead));
        }

        public void Execute()
        {
            foreach (var entity in _attackers.GetEntities())
            {
                if(!entity.hasWeapon || !entity.isAttackRequested || entity.isAttackDelaying)
                    continue;
                //TODO find collisions and hit them
                entity.isAttackRequested = false;
                entity.isAttackDelaying = true;
                entity.ReplaceAttackDelayTimer(entity.weapon.Delay);
            }
        }
    }
}