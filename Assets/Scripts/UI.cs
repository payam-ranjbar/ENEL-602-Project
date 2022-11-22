using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
        [SerializeField] private Image[] decodeImages;
        [SerializeField] private Color onColor;

        public void Decode(int index)
        {
                if (index < decodeImages.Length)
                {
                        decodeImages[index].color = onColor;
                }
        }
}