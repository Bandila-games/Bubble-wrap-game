using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sound {
    [CreateAssetMenu(fileName = "GameSoundConfig", menuName = "Config/Sound/SoundConfig")]
    public class SoundPlayerConfig : ScriptableObject
    {
        [SerializeField] public float bgm_volume = 1;
        [SerializeField] public float sfx_volume = 1;
        [SerializeField][ArrayElementTitle("type")] public List<SoundObject> audioClips = new List<SoundObject>();
        [SerializeField] private Dictionary<AudioLibrary, AudioClip> audioLibrary = new Dictionary<AudioLibrary, AudioClip>();

        [SerializeField] private SoundPlayerConfig backup;



        /// <summary>
        /// Updates dictionary
        /// </summary>
        [ContextMenu("Populate Dictionary")]
        public void UpdateAudioLibrary()
        {
            if (audioLibrary.Count == audioClips.Count)
            {
               // Debug.Log("[Sound player config]: AUDIO LIBRARY IS UPDATED");
                return;
            }                
              

           audioLibrary = new Dictionary<AudioLibrary, AudioClip>();

            foreach (SoundObject o in audioClips)
            {
                audioLibrary.Add(o.type, o.clip);
            }
            Debug.Log("[Sound Player config]: Done updating Audio library, library count: " + audioLibrary.Count);
        }

        /// <summary>
        /// Get audio clip from library via Enum
        /// </summary>
        /// <param name="type">audio clip name</param>
        /// <returns>audio clip to play</returns>
        public AudioClip getAudioClip(AudioLibrary type)
        {
            if (audioLibrary.ContainsKey(type))
            {
                return audioLibrary[type];
            }
            else
            {
                Debug.LogWarning("[Sound Player Config]:" + type.ToString() + "Cannot be found in Audio Library");
                return null;
            }
        }
        

        /// <summary>
        /// Makes a backup to the attached sound player config
        /// </summary>
        [ContextMenu("BackUp")]
        public void makeCopy()
        {
            if (this.audioClips.Count > backup.audioClips.Count)
            {
                //if this config file has more audio, its the updated one. 
                Debug.Log("this config file is up to date");
                return;
            }

            this.audioClips = backup.audioClips;
            Debug.Log(backup.audioClips.Count);
        }
       


    }
}