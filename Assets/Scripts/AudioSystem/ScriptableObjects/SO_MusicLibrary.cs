using System;
using System.Collections.Generic;
using UnityEngine;

namespace TDShooter.AudioSystem
{
    [CreateAssetMenu(menuName = "TDShooter/Audio/Music Library")]
    public class SO_MusicLibrary : ScriptableObject
    {
        public List<MusicTrack> Tracks = new List<MusicTrack>();

        public MusicTrack GetTrack(string id)
        {
            for (int i = 0; i < Tracks.Count; i++)
            {
                if (string.Equals(Tracks[i].Id, id, StringComparison.OrdinalIgnoreCase))
                {
                    return Tracks[i];
                }
            }

            return null;
        }
    }

    [Serializable]
    public class MusicTrack
    {
        public string Id;
        public AudioClip Clip;
        public float Volume = 1f;
    }
}
