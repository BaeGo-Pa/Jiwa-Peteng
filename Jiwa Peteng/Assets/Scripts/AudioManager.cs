using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    /*
        public Sound[] sounds;

        private void Awake()
        {
            if(instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }

            foreach(Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.loop = s.loop;
            }
        }

        void Start()
        {

        }


        public void Play(string sound)
        {
            Sound s = ArrayList.Find(sounds, item => item.name == sound);
            if(s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }

            s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
            s.souce.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));


            if(PauseMenu.GameIsPaused)
            {
                s.souce.pitch *= 0.5f;
            }

            s.source.Play();
        }*/
}
