using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    internal class BuildingHighlighter : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Color highlightColor = Color.yellow;
        private Color originalColor;

        private void Awake()
        {
            if (spriteRenderer != null)
                originalColor = spriteRenderer.color;
        }

        public void SetHighlight(bool highlight)
        {
            if (spriteRenderer != null)
                spriteRenderer.color = highlight ? highlightColor : originalColor;
        }
    }
}
