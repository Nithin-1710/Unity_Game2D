using UnityEngine;
[System.Serializable]
public struct MusicTrack
{
    public string trackname;
    public AudioClip clip;
}
public class MusicLibrary : MonoBehaviour
{
    public MusicTrack[] tracks;
    public AudioClip GetClipFromName(string trackName)
    {
        foreach(var track in tracks)
        {
            if (track.trackname == trackName)
            {
                return track.clip;
            }
        }
        return null;
    }
}
