using NAudio.Wave;

namespace MediocreSoundDesigner; 
/// <summary>
/// Manages the tracks
/// </summary>
public class TrackManager {
  private Track pseudoCombinedTrack;
  private Panel panContainer;
  /// <summary>
  /// The list of all of the tracks
  /// </summary>
  public List<Track> Tracks { get; private set; }
    /// <summary>
    /// The selected track
    /// </summary>
	public Track SelectedTrack { get; private set; }
  /// <summary>
  /// Number representing how much the track is zoomed out on the timescale.
  /// The higher the number, the more zoomed out the track is.
  /// </summary>
  public int DrawTimescale { get; set; }
  /// <summary>
  /// Whether the track state is playing, paused, or stopped
  /// The state is stopped if it has not been played yet.
  /// </summary>
  public PlaybackState State => pseudoCombinedTrack?.State ?? PlaybackState.Stopped;
  /// <summary>
  /// Creates the track manager with no selected track, nothing selected, and nothing playing yet
  /// </summary>
  /// <param name="panContainer"></param>
  public TrackManager(Panel panContainer) {
		Tracks = new();
    SelectedTrack = null;
    pseudoCombinedTrack = null;
    this.panContainer = panContainer;
  }
    /// <summary>
    /// Adds a track
    /// </summary>
    /// <param name="track">The track to add</param>
	public void AddTrack(Track track) {
		Tracks.Add(track);
	}

    /// <summary>
    /// Removes a track
    /// </summary>
    /// <param name="track">The track to remove</param>
    
	public void RemTrack(Track track) {
		Tracks.Remove(track);
        if (SelectedTrack == track) SelectedTrack = null; //unselects the removed track
    }
    /// <summary>
    /// Selects a track
    /// </summary>
    /// <param name="track">The track selected</param>
	public void SelectTrack(Track track) {
		SelectedTrack = track;
    Tracks.ForEach(track => {
      track.PanDrawSamples.BorderStyle = BorderStyle.None;
      track.PanDrawSamples.BackColor = Settings.Color.TrackBg_Unselected;
    });
    track.PanDrawSamples.BorderStyle = BorderStyle.Fixed3D;
    track.PanDrawSamples.BackColor = Settings.Color.TrackBg_Selected;
  }
  /// <summary>
  /// Redraws all of the tracks by marking each track's panel as needing a redo
  /// </summary>
  public void RedrawAll() {
    Tracks.ForEach(track => track.PanDrawSamples.Invalidate());
  }
  /// <summary>
  /// Applies the track settings to the selected tracks
  /// </summary>
  public void ApplySettings() {
    foreach (Track track in Tracks) {
      if (SelectedTrack == track) {
        track.PanDrawSamples.BackColor = Settings.Color.TrackBg_Selected;
      }
      else {
        track.PanDrawSamples.BackColor = Settings.Color.TrackBg_Unselected;
      }
    }
    RedrawAll();
  }
    /// <summary>
    /// Saving the track to a file
    /// </summary>
    /// <param name="filename">The output filename</param>
    public void SaveToFile(string filename)
    {
        Track mix = BuildMix();
        mix.SaveToFile(filename);
        mix.Dispose();
    }
    /// <summary>
    /// Playing all of the tracks at once
    /// </summary>
    public void PlayAll()
    {
        if (State == PlaybackState.Paused)
        {
            pseudoCombinedTrack.Resume();
            return;
        }
        CombineTracks();
        pseudoCombinedTrack.Play(startSample * 4L);
    }
    /// <summary>
    /// Combining all of the tracks
    /// </summary>
    /// <summary>Rebuilds the current mix and starts playback from the beginning.</summary>
    public void RestartAll()
    {
        startSample = 0;
        CombineTracks();
        pseudoCombinedTrack.Play();
    }

    private void CombineTracks()
    {
        pseudoCombinedTrack?.Dispose();
        pseudoCombinedTrack = BuildMix();
    }
    private Track BuildMix()
    {
        List<float> combinedSamples = new();
        foreach (Track track in Tracks)
        {
            if (track.IsDisabled) continue;
            float[] trackSamples = track.GetAdjustedSamples();
            for (int i = 0; i < trackSamples.Length; i++)
            {
                if (combinedSamples.Count <= i) combinedSamples.Add(0);
                combinedSamples[i] += trackSamples[i];
            }
        }
        Track mix = new(combinedSamples.Select(s => Math.Clamp(s, -1f, 1f)).ToArray());
        mix.LinkPanels(null, panContainer);
        return mix;
    }
    /// <summary>
    /// Pausing all of the tracks
    /// </summary>
    public void PauseAll() {
    pseudoCombinedTrack?.Pause();
  }
    /// <summary>
    /// Getting the playhead position in terms of how far it is to the right, in pixels
    /// </summary>
    /// <returns>The pixel number to the right the playhead is</returns>
    public int GetPlayheadPos()
    {
        if (State == PlaybackState.Stopped) return startSample / DrawTimescale;
        return pseudoCombinedTrack.GetPlayheadPos() / DrawTimescale;
    }
    private int startSample;
    /// <summary>
    /// Moves the playback position
    /// </summary>
    /// <param name="pixel"></param>
    public void SeekTo(int pixel)
    {
        startSample = pixel * DrawTimescale;
        if (State != PlaybackState.Stopped)   // playing or paused: jump right now
            pseudoCombinedTrack.Seek(startSample * 4L, State == PlaybackState.Playing);
    }
}
