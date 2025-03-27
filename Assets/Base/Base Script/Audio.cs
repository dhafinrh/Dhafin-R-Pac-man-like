using UnityEngine;

[System.Serializable]
    public class Audio
    {
        public string name;
        public AudioClip audioAsset;
        [Range(0f, 1f)] public float volume;
    }
