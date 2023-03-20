using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "Sprite Animation", menuName = "Config/Sprite Animation", order = 2)]
    public class SpriteAnimation : ScriptableObject
    {
        public string Name;
        public List<Sprite> Sprites;
    }
}