using System.Collections;
using UnityEngine;

namespace MainGame.Hazards
{
    public class ArrowObject : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float damage;
        [SerializeField] private float timeToDestroy;

        private void OnEnable()
        {
            gameObject.transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y + 1, transform.parent.position.z);
            StartCoroutine(SelfDisable());
        }

        void FixedUpdate()
        {
            transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        }

        private IEnumerator SelfDisable()
        {
            yield return new WaitForSeconds(timeToDestroy);
            gameObject.SetActive(false);
        }
    }
}