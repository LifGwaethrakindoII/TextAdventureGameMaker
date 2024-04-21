using TMPro;
using UnityEngine;

namespace Voidless
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class PoolTextMeshProUGUI : PoolGameObject
    {
        private TextMeshProUGUI _textMesh;

        /// <summary>Gets textMesh property./// </summary>
        public TextMeshProUGUI textMesh
        {
            get
            {
                if(_textMesh == null) _textMesh = GetComponent<TextMeshProUGUI>();
                return _textMesh;
            }
        }

        /// <summary>Callback invoked after this Pool-Object has been recycled.</summary>
        public override void OnObjectRecycled()
        {
            base.OnObjectRecycled();
            textMesh.text = string.Empty;
        }
    }
}