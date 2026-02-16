using UnityEngine;

namespace MainGame
{
    public class WaypointGizmo : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(transform.position,1);
        }
    }
}
