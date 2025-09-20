using Entitas;
using Utils;

namespace Systems.Utility
{
    public sealed class ReadGroundSensorSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _withGroundSensors;
        
        public ReadGroundSensorSystem(Contexts contexts)
        {
            _withGroundSensors = contexts.game.GetGroup(GameMatcher.AllOf(new[]
            {
                GameMatcher.GroundSensor
            }));
        }

        public void Execute()
        {
            foreach (var sensors in _withGroundSensors.GetEntities())
            {
                sensors.ReplaceGrounded(sensors.groundSensor.Value.GetState() == GroundSensorState.Grounded);
            }
        }
    }
}