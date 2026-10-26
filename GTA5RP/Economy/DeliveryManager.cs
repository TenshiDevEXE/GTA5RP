using System;
using GTA;
using GTA.Math;
using GTA5RP.Core.Contracts;
using GTA5RP.Economy;

namespace GTA5RP.Economy
{
    public class DeliveryManager
    {
        public Vector3 Target = new Vector3(0, 0, 0);
        private bool _hasTarget = false;

        public void SetTarget(Vector3 t) { Target = t; _hasTarget = true; }

        public bool IsNearTarget(Vector3 pos, float radius = 5f)
        {
            if (!_hasTarget) return false;
            return pos.DistanceTo(Target) <= radius;
        }
    }
}
