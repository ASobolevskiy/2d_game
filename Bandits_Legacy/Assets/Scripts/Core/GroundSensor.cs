using System;
using UnityEngine;
using Utils;

namespace Core
{
    public sealed class GroundSensor : MonoBehaviour
    {
        private GroundSensorState _state;

        private int _numOfCollisions;
        private float _disableTimer;

        private void OnTriggerEnter2D(Collider2D other)
        {
            _numOfCollisions++;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _numOfCollisions--;
        }

        private void Update()
        {
            if (_disableTimer <= 0)
                return;
            _disableTimer -= Time.deltaTime;
        }

        public GroundSensorState GetState()
        {
            return _disableTimer != 0 
                ? GroundSensorState.Aerial
                : _numOfCollisions <= 0
                    ? GroundSensorState.Aerial
                    : GroundSensorState.Grounded;
        }

        public void DisableSensor(float duration)
        {
            _disableTimer = duration;
        }
    }
}