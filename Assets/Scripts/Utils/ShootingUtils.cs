using TestAssignment.Characters;
using UnityEngine;

namespace TestAssignment.Utils
{
    public static class ShootingUtils
    {
        public static bool TargetIsVisible(BaseCharacterComponent actor, BaseCharacterComponent target)
        {
            if (target == null || actor == null)
                return false;

            var ray = new Ray(actor.transform.position + Vector3.up, target.transform.position - actor.transform.position + Vector3.up);
            if (Physics.Raycast(ray, out var hit) && hit.collider.TryGetComponent<BaseCharacterComponent>(out var t) && t == target)
            {
                return true;
            }

            return false;
        }
    }
}