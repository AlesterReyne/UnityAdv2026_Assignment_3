using System.Collections;
using UnityEngine;

namespace MainGame.Hazards
{
    public class ArrowHazard : MonoBehaviour
    {
        [SerializeField] private GameObject arrowPrefab;
        [SerializeField] float shootInterval;

        void Start()
        {
            StartCoroutine(CreateArrow());
        }

        private IEnumerator CreateArrow()
        {
            yield return new WaitForSeconds(shootInterval);
            GameObject arrow = ObjectPool.SharedInstance.GetPooledObject();
            if (arrow != null)
            {
                arrow.transform.position = transform.position;
                arrow.transform.rotation = transform.rotation;
                arrow.SetActive(true);
            }

            StartCoroutine(CreateArrow());
        }
    }
}