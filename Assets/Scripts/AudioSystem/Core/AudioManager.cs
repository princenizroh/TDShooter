using UnityEngine;
using UnityEngine.Audio;
using TDShooter.Core;

namespace TDShooter.AudioSystem
{
    public class AudioManager : SingletonBehaviour<AudioManager>
    {
        [Header("Libraries")]
        [SerializeField] private SO_MusicLibrary _musicLibrary;

        [Header("Music")]
        [SerializeField] private AudioSource _musicSource;

        protected override void OnSingletonAwake()
        {
            SetupMusicSources();
        }

        public void PlayMusicById(string id, bool loop = true)
        {
            MusicTrack track = _musicLibrary.GetTrack(id);
            if (track == null || track.Clip == null)
            {
                return;
            }

            _musicSource.clip = track.Clip;
            _musicSource.volume = track.Volume;
            _musicSource.loop = loop;
            _musicSource.Play();
        }


        private void SetupMusicSources()
        {
            if (_musicSource == null)
            {
                _musicSource = gameObject.AddComponent<AudioSource>();
            }

            _musicSource.playOnAwake = false;
            _musicSource.loop = true;
        }

    }
}
