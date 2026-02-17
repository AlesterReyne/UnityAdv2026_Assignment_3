using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
                AddArrow();
            }

            StartCoroutine(GetArrow());
        }

        private IEnumerator GetArrow()
        {
            yield return new WaitForSeconds(shootInterval);
            ArrowObject arrow = GetPooledObject();
            if (arrow)
            {
                arrow.gameObject.SetActive(true);
            }

            StartCoroutine(GetArrow());
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

            AddArrow();
            return _pooledArrows[_pooledArrows.Count - 1];
        }

        private void AddArrow()
        {
            _arrow = Instantiate(arrowPrefab, transform).GetComponent<ArrowObject>();
            _arrow.gameObject.SetActive(false);
            _pooledArrows.Add(_arrow);
        }
    }
}