using System.Collections;
using UnityEngine;

namespace Assets.Sources.BaseLogic.Item
{
    public class ParentChangerComponent
    {
        private const float MoveDuration = 0.3f;

        private readonly Rigidbody _rigidbody;
        private readonly ItemObject _itemObject;
        private readonly Collider _collider;

        public ParentChangerComponent(Rigidbody rigidbody, ItemObject itemObject, Collider collider)
        {
            _rigidbody = rigidbody;
            _itemObject = itemObject;
            _collider = collider;

            Fixed = false;
        }

        public bool Fixed { get; private set; }

        public void Set(Transform parent, bool isInstantly = false)
        {
            Fixed = true;
            _rigidbody.isKinematic = true;
            _itemObject.transform.parent = parent;
            _collider.enabled = false;

            if(isInstantly)
            {
                _itemObject.transform.position = parent.position;
                _itemObject.transform.rotation = parent.rotation;
            }
            else
            {
                _itemObject.StartCoroutine(Mover(parent));
            }
        }

        public void Reset()
        {
            Fixed = false;
            _rigidbody.isKinematic = false;
            _rigidbody.transform.parent = null;
            _collider.enabled = true;
        }

        private IEnumerator Mover(Transform targetParent)
        {
            float progress = 0;
            float passedTime = 0;

            Vector3 startPosition = _itemObject.transform.position;
            Quaternion startRotation = _itemObject.transform.rotation;

            while(progress < 1)
            {
                progress = passedTime / MoveDuration;
                passedTime += Time.deltaTime;

                _itemObject.transform.position = Vector3.Lerp(startPosition, targetParent.position, progress);
                _itemObject.transform.rotation = Quaternion.Lerp(startRotation, targetParent.rotation, progress);

                yield return null;
            }
        }
    }
}
