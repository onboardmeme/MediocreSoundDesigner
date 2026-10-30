using System.Text.Json;
using System.Text.Json.Serialization;
using Color = System.Drawing.Color;

namespace MediocreSoundDesigner;
/// <summary>
/// Converts colors to and from hex so they can be loaded in to the settings JSON.
/// </summary>
public class ColorJsonConverter : JsonConverter<Color> {
    /// <summary>
    /// Reads a hex color string and converts it to a color
    /// </summary>
    /// <param name="reader">The JSON reader positioned</param>
    /// <param name="typeToConvert">The Color type to convert to</param>
    /// <param name="options">The options in use</param>
    /// <returns></returns>
  public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => ColorTranslator.FromHtml(reader.GetString());
    /// <summary>
    /// Writes a color to the JSON as a hex string
    /// </summary>
    /// <param name="writer"> The JSON writer</param>
    /// <param name="value"> The color value</param>
    /// <param name="options">The options in use</param>
  public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options) => writer.WriteStringValue("#" + value.R.ToString("X2") + value.G.ToString("X2") + value.B.ToString("X2").ToLower());
}
/// <summary>
/// Program-wide settings
/// </summary>
public static class Settings {
/// <summary>
/// The colors used throughout the program
/// </summary>
  public class Colors {
    /// <summary>
    /// Window Background color
    /// </summary>
    public Color WinBg { get; set; }
    /// <summary>
    /// Windows Foreground color
    /// </summary>
    public Color WinFg { get; set; }
    /// <summary>
    /// Colors enabled track samples
    /// </summary>
    public Color TrackSamples_Enabled { get; set; }
    /// <summary>
    /// Colors disabled track samples
    /// </summary>
    public Color TrackSamples_Disabled { get; set; }
    /// <summary>
    /// Unselected track background
    /// </summary>
    public Color TrackBg_Unselected { get; set; }
    /// <summary>
    /// Selected track background
    /// </summary>
    public Color TrackBg_Selected { get; set; }
    /// <summary>
    /// Color of the playline
    /// </summary>
    public Color Playline { get; set; }

    /// <summary>
    /// Default color set
    /// </summary>
    public Colors() {
      WinBg = System.Drawing.Color.Black;
      WinFg = System.Drawing.Color.White;
      TrackSamples_Enabled = System.Drawing.Color.Cyan;
      TrackSamples_Disabled = System.Drawing.Color.LightGray;
      TrackBg_Unselected = System.Drawing.Color.DimGray;
      TrackBg_Selected = System.Drawing.Color.Gray;
      Playline = System.Drawing.Color.White;
    }
  }

  static Settings() {
    LoadFromFile();
  }
/// <summary>
/// The program's current color settings.
/// </summary>
  public static Colors Color { get; private set; }

  private const string FILENAME = "settings.json";
    /// <summary>
    /// Saves track to file system
    /// </summary>
  public static void SaveToFile() {
    JsonSerializerOptions options = new() { Converters = { new ColorJsonConverter() } };
    File.WriteAllText(FILENAME, JsonSerializer.Serialize(Color, options));
  }
/// <summary>
/// Loads track from file system
/// </summary>
  public static void LoadFromFile() {
    if (File.Exists(FILENAME)) {
      JsonSerializerOptions options = new() { Converters = { new ColorJsonConverter() } };
      Color = JsonSerializer.Deserialize<Colors>(File.ReadAllText(FILENAME), options);
    }
    else {
      Color = new();
    }
  }
/// <summary>
/// Reverts the color to default.
/// </summary>
  public static void RevertToDefault() {
    Color = new();
  }
}
