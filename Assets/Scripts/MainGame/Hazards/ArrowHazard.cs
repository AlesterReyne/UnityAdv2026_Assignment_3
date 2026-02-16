using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainGame.Hazards
{
    public class ArrowHazard : MonoBehaviour
    {
        [SerializeField] private GameObject arrowPrefab;
        [SerializeField] float shootInterval;
        private List<ArrowObject> _pooledArrows;
        private ArrowObject _arrow;
        public int amountToPool;

        void Start()
        {
            _pooledArrows = new List<ArrowObject>();

            for (int i = 0; i < amountToPool; i++)
            {
                _arrow = Instantiate(arrowPrefab, transform).GetComponent<ArrowObject>();
                _arrow.gameObject.SetActive(false);
                _pooledArrows.Add(_arrow);
            }

            StartCoroutine(CreateArrow());
        }

        private IEnumerator CreateArrow()
        {
            yield return new WaitForSeconds(shootInterval);
            ArrowObject arrow = GetPooledObject();
            if (arrow)
            {
                arrow.transform.position = transform.position;
                arrow.transform.rotation = transform.rotation;
                arrow.gameObject.SetActive(true);
            }

            StartCoroutine(CreateArrow());
        }

        public ArrowObject GetPooledObject()
        {
            for (int i = 0; i < amountToPool; i++)
            {
                if (!_pooledArrows[i].gameObject.activeInHierarchy)
                {
                    return _pooledArrows[i];
                }
            }

            return null;
        }
    }
}