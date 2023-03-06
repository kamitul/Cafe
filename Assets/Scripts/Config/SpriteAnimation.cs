using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(menuName = "Sprite Animation")]
    public class SpriteAnimation : ScriptableObject
    {
        public string Name;
        public List<Sprite> Sprites;
    }
}