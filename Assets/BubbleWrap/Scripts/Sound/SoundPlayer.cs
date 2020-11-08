using System.Collections;
using UnityEngine;
using UnityEngine.Events;


    namespace Sound
    {

        public class Soundplayer : MonoBehaviour
        {
            #region Fields

            public static Soundplayer instance = null;

            /// <summary>
            /// bgm_audiosource has a more background feel with a spread of 10, volume of 100 and has the highest priority
            /// Can only play 1 BGM
            /// </summary>
            [SerializeField] public AudioSource bgm_Audiosource = null;
            /// <summary>
            /// sfx_audiosource has a foreground feel
            /// </summary>
            [SerializeField] public AudioSource sfx_Audiosource = null;

            [SerializeField] public SoundPlayerConfig configFile = null;

            public bool isDebugging = false;
            #endregion 

            #region UNITY_METHODS
            private void Awake()
            {
                Initialize();
            }

            private void OnDestroy()
            {
                if (instance == this)
                {
                    instance = null;
                }
            }


            #endregion

            #region private methods
          
            /// <summary>
            /// Initializes the soundplayer singleton
            /// </summary>
            private void Initialize()
            {
                if (instance == null)
                {
                    instance = this;
                }
                else
                {
                    Destroy(this.gameObject);
                }
                DontDestroyOnLoad(this);               
            }

            /// <summary>
            /// Creates instance of the soundplayer if not existing
            /// </summary>
            private static void CreateInstance()
            {
                GameObject soundPlayergameobject = new GameObject("Sound Player");
                Soundplayer soundPlayer = soundPlayergameobject.AddComponent<Soundplayer>();

                soundPlayer.sfx_Audiosource = soundPlayergameobject.AddComponent<AudioSource>();
                //Set SFX audio settings
                soundPlayer.bgm_Audiosource = soundPlayergameobject.AddComponent<AudioSource>();
                //Set BGM audio settings
                soundPlayer.configFile = Resources.Load<SoundPlayerConfig>("GameSoundConfig");
                soundPlayer.configFile.UpdateAudioLibrary();
                SetSourceVolume(SoundSourceType.SFX, soundPlayer.configFile.sfx_volume);
                SetSourceVolume(SoundSourceType.BGM, soundPlayer.configFile.bgm_volume);
            }

            private void Log(string msg)
            {
                if (!isDebugging) return;
                Debug.Log("Sound player: " + msg);
            }

            private void LogWarning(string msg)
            {
                if (!isDebugging) return;
                Debug.LogWarning("Sound Player: " + msg);
            }

            /// <summary>
            /// Play audio with a callback
            /// </summary>
            /// <param name="clip">audio clip tye</param>
            /// <param name="delay">delay</param>
            /// <param name="action">Action</param>
            /// <returns></returns>
            private static IEnumerator EnumeratorPlayAudioWithCallback(AudioLibrary clip, ulong delay = 0,UnityAction action = null)
            {
                if (instance == null)
                {
                    CreateInstance();
                }

                instance.sfx_Audiosource.PlayOneShot(instance.configFile.getAudioClip(clip));

                yield return new WaitForSeconds(instance.configFile.getAudioClip(clip).length);
                action?.Invoke();
                yield return null;
            }

            private static IEnumerator EnumeratorPlayAudioWithCallback(AudioClip clip, ulong delay = 0, UnityAction action = null)
            {
                if (instance == null)
                {
                    CreateInstance();
                }

                instance.sfx_Audiosource.PlayOneShot(clip);

                yield return new WaitForSeconds(clip.length);
                action?.Invoke();
                yield return null;
            }

            #endregion

            #region public methods (API)

            /// <summary>
            /// Play sound by referencing the audiolibrary. this would be played as SFX by default
            /// </summary>
            /// <param name="clip">Audio clip in audio library you want to play</param>
            public static void PlayAudio(AudioLibrary clip)
            {
                if(instance == null)
                {
                    CreateInstance();
                }
                instance.sfx_Audiosource.PlayOneShot(instance.configFile.getAudioClip(clip));
            }

            /// <summary>
            /// Play sound via audio library, and set which audio source will play
            /// </summary>
            /// <param name="clip">audio clip</param>
            /// <param name="type">SOURCE SFX or BGM</param>
            public static void PlayAudio(AudioLibrary clip, SoundSourceType type)
            {
                if(instance == null)
                {
                    CreateInstance();
                }

                if(type == SoundSourceType.BGM)
                { 
                     if(instance.bgm_Audiosource.isPlaying)
                    {
                        instance.bgm_Audiosource.Stop();                      
                    }
                    instance.bgm_Audiosource.clip = instance.configFile.getAudioClip(clip);
                    instance.bgm_Audiosource.loop = true;
                    instance.bgm_Audiosource.Play();
                }
                else
                {
                    instance.sfx_Audiosource.PlayOneShot(instance.configFile.getAudioClip(clip));
                }
            }

            /// <summary>
            /// play sound via library, and set which audio source will play the sound while setting delay and play type settings
            /// </summary>
            /// <param name="clip">audio clip</param>
            /// <param name="type">SOURCE SFX or BGM</param>
            /// <param name="playtype"> Loop or One shot </param>
            /// <param name="delay">Delay 100 = 1 second</param>
            public static void PlayAudio(AudioLibrary clip, SoundSourceType type, PlayType playtype, ulong delay = 0)
            {

                if (instance == null)
                {
                    CreateInstance();
                }

                if (type == SoundSourceType.BGM)
                {
                    if (instance.bgm_Audiosource.isPlaying)
                    {
                        instance.bgm_Audiosource.Stop();
                    }
                    instance.bgm_Audiosource.clip = instance.configFile.getAudioClip(clip);
                    instance.bgm_Audiosource.loop = playtype == PlayType.Loop;
                    instance.bgm_Audiosource.Play(delay);
                }
                else
                {
                    instance.sfx_Audiosource.clip = instance.configFile.getAudioClip(clip);
                    instance.sfx_Audiosource.loop = playtype == PlayType.Loop;
                    instance.sfx_Audiosource.Play(delay);
                }

            }


            /// <summary>
            /// play sound via library, and set which audio source will play the sound while setting delay
            /// play type default to 1 shot
            /// </summary>
            /// <param name="clip">audio clip</param>
            /// <param name="type">SOURCE SFX or BGM</param>
            /// <param name="delay">Delay 100 = 1 second</param>
            public static void PlayAudio(AudioLibrary clip, SoundSourceType type, ulong delay)
            {
                if (instance == null)
                {
                    CreateInstance();
                }

                if (type == SoundSourceType.BGM)
                {
                    if (instance.bgm_Audiosource.isPlaying)
                    {
                        instance.bgm_Audiosource.Stop();
                    }
                    instance.bgm_Audiosource.clip = instance.configFile.getAudioClip(clip);
                    instance.bgm_Audiosource.loop = true;
                    instance.bgm_Audiosource.Play(delay);
                }
                else
                {
                    instance.sfx_Audiosource.clip = instance.configFile.getAudioClip(clip);
                    instance.sfx_Audiosource.Play(delay);
                }
            }


            /// <summary>
            /// Play audio with a callback
            /// </summary>
            /// <param name="clip">audio clip tye</param>
            /// <param name="delay">delay</param>
            /// <param name="action">Action</param>
            public static void PlayAudioWithCallBack(AudioLibrary clip,UnityAction action,ulong delay = 0)
            {
               instance.StartCoroutine(EnumeratorPlayAudioWithCallback(clip, delay, action));
            }

            /// <summary>
            /// Play audioclip with callback
            /// </summary>
            /// <param name="clip"> audio clip to play</param>
            /// <param name="action">action callback</param>
            public static void PlayAudioClip(AudioClip clip, UnityAction action = null)
            {
                instance.StartCoroutine(EnumeratorPlayAudioWithCallback(clip, default, action));
            }

          



            public static void StopAudio(AudioLibrary clip,SoundSourceType type)
            {
                if(instance == null) { CreateInstance(); }

                if(type == SoundSourceType.SFX )
                {
                    if (instance.sfx_Audiosource.isPlaying) instance.sfx_Audiosource.Stop();
                    else { instance.LogWarning("Source not playing any audio to stop"); return; }
                }
                else
                {
                    if (instance.bgm_Audiosource.isPlaying) instance.bgm_Audiosource.Stop();
                    else { instance.LogWarning("Source not playing any audio to stop"); return; }
                }
            }
            public static void StopAllAudio()
            {
                if (instance == null)
                {
                    CreateInstance();
                }
                instance.sfx_Audiosource.Stop();
                instance.bgm_Audiosource.Stop();
            }
            public static void SetSourceVolume(SoundSourceType type, float volume )
            {
               
                if(type == SoundSourceType.BGM)
                {
                    if (volume == 0) instance.LogWarning("Warning: you set the volume of BGM source to 0");
                    instance.bgm_Audiosource.volume = volume;
                }
                else
                {
                    if (volume == 0) instance.LogWarning("Warning: you set the volume of SFX source to 0");
                    instance.sfx_Audiosource.volume = volume;
                }
            }
            #endregion           

        }



        public enum SoundSourceType
        {
            SFX = 0,
            BGM = 1,
        }

        public enum PlayType
        {
            Oneshot = 0,
            Loop = 1,
        }
    }
