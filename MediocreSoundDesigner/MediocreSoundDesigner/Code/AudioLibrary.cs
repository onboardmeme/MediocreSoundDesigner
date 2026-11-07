using NAudio.Wave;

namespace MediocreSoundDesigner;

/// <summary>Stores reusable clips independently of the current project.</summary>
public class AudioLibrary
{
    /// <summary>Folder containing the user's saved clips.</summary>
    public string FolderPath { get; }

    /// <summary>Uses a per-user library, or a supplied folder.</summary>
    public AudioLibrary(string folderPath = null)
    {
        FolderPath = folderPath ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "MediocreSoundDesigner", "AudioLibrary");
    }

    /// <summary>Lists saved clips alphabetically.</summary>
    public string[] GetClips()
    {
        if (!Directory.Exists(FolderPath)) return Array.Empty<string>();
        return Directory.GetFiles(FolderPath, "*.wav").OrderBy(Path.GetFileName, StringComparer.OrdinalIgnoreCase).ToArray();
    }

    /// <summary>Deletes a saved WAV clip from this library only.</summary>
    public void DeleteClip(string path)
    {
        string fullPath = Path.GetFullPath(path);
        if (!string.Equals(Path.GetDirectoryName(fullPath), Path.TrimEndingDirectorySeparator(Path.GetFullPath(FolderPath)), StringComparison.OrdinalIgnoreCase)
            || !string.Equals(Path.GetExtension(fullPath), ".wav", StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException("Select a saved audio clip from this library.");
        File.Delete(fullPath);
    }

    /// <summary>Saves edited samples and volume, without the track's timeline offset.</summary>
    public void SaveClip(string name, Track track)
    {
        name = name.Trim();
        if (name.Length == 0 || name.Length > 80 || name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0
            || name.EndsWith('.') || name is "." or "..")
            throw new ArgumentException("Enter a clip name of 1–80 characters without filename symbols or a trailing period.");
        float[] samples = track.CloneSamples();
        if (samples.Length == 0) throw new ArgumentException("The selected track has no audio to save.");
        for (int i = 0; i < samples.Length; i++) samples[i] *= track.Volume;
        Directory.CreateDirectory(FolderPath);
        string destination = Path.Combine(FolderPath, name + ".wav");
        if (File.Exists(destination)) throw new ArgumentException("That name already exists. Choose a different clip name.");
        string temporary = Path.Combine(FolderPath, Guid.NewGuid() + ".tmp");
        try
        {
            using (var writer = new WaveFileWriter(temporary, WaveFormat.CreateIeeeFloatWaveFormat(44100, 1)))
                writer.WriteSamples(samples, 0, samples.Length);
            File.Move(temporary, destination);
        }
        finally
        {
            if (File.Exists(temporary)) File.Delete(temporary);
        }
    }
}
