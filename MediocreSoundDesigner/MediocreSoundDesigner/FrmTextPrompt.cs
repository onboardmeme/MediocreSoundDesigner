namespace MediocreSoundDesigner;

/// <summary>A themed dialog for naming clips and entering export information.</summary>
public sealed class FrmTextPrompt : Form
{
    private readonly TextBox input = new();
    /// <summary>The entered text.</summary>
    public string Value => input.Text.Trim();

    /// <summary>Creates a text entry prompt.</summary>
    public FrmTextPrompt(string title, string description, string initialValue = "")
    {
        input.Text = initialValue;
        var cancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel };
        var save = new Button { Text = "Save", DialogResult = DialogResult.OK };
        DialogTheme.Build(this, title, description, new[] { ("Name", (Control)input) }, cancel, save);
        AcceptButton = save;
        Shown += (_, _) => { input.Focus(); input.SelectAll(); };
    }
}
