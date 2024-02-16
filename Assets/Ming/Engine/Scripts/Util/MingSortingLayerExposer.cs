using UnityEngine;

namespace Ming
{
    [ExecuteInEditMode]
    public sealed class MingSortingLayerExposer : MingBehaviour
    {
        [SerializeField]
        private string SortingLayerName = "Default";

        [SerializeField]
        private int SortingOrder = 0;

        public void OnValidate()
        {
            apply();
        }

        public void OnEnable()
        {
            apply();
        }

        private void apply()
        {
            var meshRenderer = gameObject.GetComponent<MeshRenderer>();
            meshRenderer.sortingLayerName = SortingLayerName;
            meshRenderer.sortingOrder = SortingOrder;
        }
    }
}
