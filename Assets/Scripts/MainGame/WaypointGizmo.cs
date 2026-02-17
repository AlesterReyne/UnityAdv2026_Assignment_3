using UnityEngine;

namespace MainGame
{
    public class WaypointGizmo : MonoBehaviour
    {
        private readonly int _radius = 1;

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(transform.position, _radius);
        }
    }
}