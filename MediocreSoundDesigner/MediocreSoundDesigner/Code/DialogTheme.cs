namespace MediocreSoundDesigner;

/// <summary>Shared appearance and spacing for the editor's popup windows.</summary>
internal static class DialogTheme
{
    internal static void Build(Form form, string title, string description,
        IEnumerable<(string Label, Control Input)> rows, params Button[] actions)
    {
        var fields = rows.ToArray();
        form.SuspendLayout();
        form.AutoScaleMode = AutoScaleMode.Dpi;
        form.Font = new Font("Segoe UI", 10F);
        form.Text = title;
        form.BackColor = Settings.Color.WinBg;
        form.ForeColor = Settings.Color.WinFg;
        form.FormBorderStyle = FormBorderStyle.FixedDialog;
        form.StartPosition = FormStartPosition.CenterParent;
        form.MaximizeBox = false;
        form.MinimizeBox = false;
        form.ShowInTaskbar = false;
        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill, Padding = new Padding(24), ColumnCount = 1,
            RowCount = 4, BackColor = form.BackColor
        };
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));
        root.Controls.Add(new Label { Text = title, Dock = DockStyle.Fill, Font = new Font("Segoe UI", 15F, FontStyle.Bold) }, 0, 0);
        root.Controls.Add(new Label { Text = description, Dock = DockStyle.Fill, Font = form.Font }, 0, 1);
        var body = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = fields.Length,
            BackColor = Color.FromArgb(196, 200, 203), ForeColor = Color.Black, Padding = new Padding(14) };
        body.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65));
        body.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
        int contentHeight = 28;
        foreach (var (label, input) in fields)
        {
            int index = body.RowStyles.Count;
            bool large = input is ListBox;
            int height = large ? 190 : 46;
            contentHeight += height;
            body.RowStyles.Add(new RowStyle(SizeType.Absolute, height));
            input.Font = form.Font;
            input.Margin = new Padding(0, 4, 0, 8);
            input.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            input.AutoSize = false;
            if (input is ListBox list)
            {
                list.IntegralHeight = false;
                list.Dock = DockStyle.Fill;
                list.BorderStyle = BorderStyle.FixedSingle;
                list.BackColor = Color.FromArgb(229, 232, 234);
                list.ForeColor = Color.Black;
                list.ItemHeight = 26;
                list.DrawMode = DrawMode.OwnerDrawFixed;
                list.DrawItem += (_, e) =>
                {
                    if (e.Index < 0) return;
                    bool selected = (e.State & DrawItemState.Selected) != 0;
                    using var background = new SolidBrush(selected ? Color.FromArgb(51, 91, 119)
                        : e.Index % 2 == 0 ? list.BackColor : Color.FromArgb(218, 223, 226));
                    e.Graphics.FillRectangle(background, e.Bounds);
                    var textBounds = new Rectangle(e.Bounds.X + 10, e.Bounds.Y, e.Bounds.Width - 10, e.Bounds.Height);
                    TextRenderer.DrawText(e.Graphics, list.Items[e.Index].ToString(), list.Font, textBounds,
                        selected ? Color.White : Color.Black, TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
                    e.DrawFocusRectangle();
                };
            }
            else if (input is Button swatch)
            {
                swatch.Anchor = AnchorStyles.Right;
                swatch.Size = new Size(48, 28);
                swatch.FlatStyle = FlatStyle.Flat;
                swatch.FlatAppearance.BorderSize = 1;
                swatch.FlatAppearance.BorderColor = Color.Gray;
                swatch.AccessibleName = label;
                if (swatch.Name == "btnRecord")
                {
                    swatch.Size = new Size(36, 36);
                    swatch.BackgroundImageLayout = ImageLayout.Zoom;
                    swatch.BackColor = Color.FromArgb(229, 232, 234);
                }
            }
            else if (input is TextBoxBase or NumericUpDown or ComboBox)
            {
                input.BackColor = Color.FromArgb(229, 232, 234);
                input.ForeColor = Color.Black;
                input.Height = 30;
                if (input is ComboBox combo) combo.FlatStyle = FlatStyle.Flat;
            }
            if (label.Length == 0)
            {
                body.Controls.Add(input, 0, index);
                body.SetColumnSpan(input, 2);
            }
            else
            {
                body.Controls.Add(new Label { Text = label, Dock = DockStyle.Fill, ForeColor = Color.Black, TextAlign = ContentAlignment.MiddleLeft, Font = form.Font }, 0, index);
                body.Controls.Add(input, 1, index);
            }
            if (input is Label) input.ForeColor = Color.Black;
        }
        root.Controls.Add(body, 0, 2);
        var footer = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, Padding = new Padding(0, 12, 0, 0), WrapContents = false };
        foreach (Button button in actions.Reverse())
        {
            button.Font = form.Font;
            button.AutoSize = false;
            button.Size = new Size(Math.Max(88, TextRenderer.MeasureText(button.Text, form.Font).Width + 24), 32);
            button.Margin = new Padding(8, 0, 0, 0);
            button.BackColor = Color.FromArgb(196, 200, 203);
            button.ForeColor = Color.Black;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 224, 227);
            button.UseVisualStyleBackColor = false;
            footer.Controls.Add(button);
        }
        root.Controls.Add(footer, 0, 3);
        // Reparent the existing inputs and buttons so their event handlers remain intact.
        form.Controls.Clear();
        form.Controls.Add(root);
        form.ClientSize = new Size(580, 24 * 2 + 36 + 48 + contentHeight + 52);
        var cancel = actions.FirstOrDefault(b => b.Text is "Cancel" or "Close");
        if (cancel != null) { cancel.DialogResult = DialogResult.Cancel; form.CancelButton = cancel; }
        form.ResumeLayout(true);
    }

    internal static void StyleMenu(ContextMenuStrip menu)
    {
        menu.Font = new Font("Segoe UI", 10F);
        menu.ForeColor = Color.Black;
        foreach (ToolStripItem item in menu.Items) item.ForeColor = Color.Black;
        menu.BackColor = Settings.Color.TrackBg_Unselected;
        menu.Renderer = new ToolStripProfessionalRenderer(new MenuColors());
    }

    private sealed class MenuColors : ProfessionalColorTable
    {
        public override Color ToolStripDropDownBackground => Settings.Color.TrackBg_Unselected;
        public override Color MenuItemSelected => Settings.Color.TrackBg_Selected;
        public override Color MenuItemBorder => Settings.Color.TrackSamples_Enabled;
        public override Color MenuBorder => Settings.Color.TrackBg_Selected;
        public override Color ImageMarginGradientBegin => Settings.Color.TrackBg_Unselected;
        public override Color ImageMarginGradientMiddle => Settings.Color.TrackBg_Unselected;
        public override Color ImageMarginGradientEnd => Settings.Color.TrackBg_Unselected;
    }
}
