using System.Drawing.Drawing2D;

namespace MediocreSoundDesigner;

/// <summary>Draws consistent toolbar symbols without relying on font glyphs.</summary>
internal static class ToolbarIcons
{
    internal enum Action { Open, Save, Library, SaveTrack, Settings, Record, Append, Noise, Beats, Instruments, Flip, Reverse, Volume, Pitch, Silence, Delete }

    internal static void Apply(Button button, Action action)
    {
        button.Text = "";
        button.Paint += (_, e) => Draw(e.Graphics, button, action);
    }

    private static void Draw(Graphics graphics, Button button, Action action)
    {
        var state = graphics.Save();
        try
        {
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            float size = Math.Min(24, Math.Min(button.ClientSize.Width, button.ClientSize.Height) - 8);
            graphics.TranslateTransform((button.ClientSize.Width - size) / 2, (button.ClientSize.Height - size) / 2);
            graphics.ScaleTransform(size / 24, size / 24);
            Color color = button.Enabled ? button.ForeColor : SystemColors.GrayText;
            using var pen = new Pen(color, 1.7f) { StartCap = LineCap.Round, EndCap = LineCap.Round, LineJoin = LineJoin.Round };
            using var brush = new SolidBrush(color);
            void Line(float x1, float y1, float x2, float y2) => graphics.DrawLine(pen, x1, y1, x2, y2);
            void Box(float x, float y, float w, float h) => graphics.DrawRectangle(pen, x, y, w, h);
            void Poly(params PointF[] points) => graphics.DrawLines(pen, points);
            void Arrow(float x1, float x2, float y)
            {
                Line(x1, y, x2, y);
                float direction = x2 > x1 ? -1 : 1;
                Poly(new(x2 + direction * 4, y - 3), new(x2, y), new(x2 + direction * 4, y + 3));
            }
            void Wave(float left, float right, float y)
            {
                for (float x = left; x < right; x += 3)
                    Line(x, y - (x % 2 == 0 ? 4 : 7), x, y + (x % 2 == 0 ? 4 : 7));
            }
            switch (action)
            {
                case Action.Open:
                    Poly(new(2, 18), new(2, 5), new(9, 5), new(12, 8), new(21, 8), new(21, 10));
                    graphics.DrawPolygon(pen, new PointF[] { new(2, 19), new(5, 10), new(23, 10), new(19, 19) });
                    break;
                case Action.Save:
                    graphics.DrawPolygon(pen, new PointF[] { new(3, 2), new(18, 2), new(22, 6), new(22, 22), new(3, 22) });
                    Box(7, 2, 10, 7); Box(7, 14, 11, 8); Line(14, 4, 14, 7);
                    break;
                case Action.Library:
                    Box(3, 4, 4, 16); Box(9, 4, 4, 16);
                    Poly(new(16, 5), new(19, 4), new(23, 19), new(19, 20), new(16, 5));
                    Line(1, 22, 23, 22);
                    break;
                case Action.SaveTrack:
                    Wave(3, 12, 8); Line(18, 3, 18, 15);
                    Poly(new(14, 11), new(18, 15), new(22, 11));
                    Poly(new(3, 17), new(3, 21), new(22, 21), new(22, 17));
                    break;
                case Action.Settings:
                    graphics.DrawEllipse(pen, 5, 5, 14, 14); graphics.DrawEllipse(pen, 9, 9, 6, 6);
                    for (int i = 0; i < 8; i++)
                    {
                        double angle = i * Math.PI / 4;
                        Line(12 + (float)Math.Cos(angle) * 7, 12 + (float)Math.Sin(angle) * 7,
                            12 + (float)Math.Cos(angle) * 10, 12 + (float)Math.Sin(angle) * 10);
                    }
                    break;
                case Action.Record:
                    using (var red = new SolidBrush(button.Enabled ? Color.Firebrick : color)) graphics.FillEllipse(red, 4, 4, 16, 16);
                    break;
                case Action.Append:
                    graphics.DrawEllipse(pen, 5, 2, 7, 12);
                    graphics.DrawArc(pen, 2, 6, 13, 12, 0, 180);
                    Line(8.5f, 18, 8.5f, 22); Line(5, 22, 12, 22);
                    Line(19, 13, 19, 21); Line(15, 17, 23, 17);
                    break;
                case Action.Noise:
                    Poly(new(1, 12), new(3, 8), new(5, 17), new(7, 4), new(9, 20), new(11, 9), new(13, 15), new(15, 3), new(17, 18), new(19, 7), new(21, 13), new(23, 10));
                    break;
                case Action.Beats:
                    graphics.DrawEllipse(pen, 3, 6, 18, 6);
                    Line(3, 9, 3, 19); Line(21, 9, 21, 19); graphics.DrawArc(pen, 3, 16, 18, 6, 0, 180);
                    Line(6, 12, 6, 19); Line(18, 12, 18, 19);
                    Line(6, 1, 17, 7); Line(18, 1, 8, 7);
                    break;
                case Action.Instruments:
                    Box(2, 4, 20, 17); Line(7, 5, 7, 21); Line(12, 5, 12, 21); Line(17, 5, 17, 21);
                    graphics.FillRectangle(brush, 5, 4, 4, 10); graphics.FillRectangle(brush, 10, 4, 4, 10); graphics.FillRectangle(brush, 15, 4, 4, 10);
                    break;
                case Action.Flip:
                    Line(2, 12, 22, 12);
                    Poly(new(3, 12), new(7, 3), new(11, 12));
                    Poly(new(13, 12), new(17, 21), new(21, 12));
                    break;
                case Action.Reverse:
                    Arrow(21, 3, 8); Arrow(3, 21, 17);
                    break;
                case Action.Volume:
                    graphics.DrawPolygon(pen, new PointF[] { new(2, 9), new(6, 9), new(12, 4), new(12, 20), new(6, 15), new(2, 15) });
                    graphics.DrawArc(pen, 10, 7, 9, 10, -65, 130); graphics.DrawArc(pen, 10, 3, 15, 18, -65, 130);
                    break;
                case Action.Pitch:
                    graphics.FillEllipse(brush, 2, 16, 7, 5); Line(8, 18, 8, 4); Line(8, 4, 13, 6);
                    Line(19, 3, 19, 21);
                    Poly(new(16, 6), new(19, 3), new(22, 6));
                    Poly(new(16, 18), new(19, 21), new(22, 18));
                    break;
                case Action.Silence:
                    Wave(2, 6, 12); Wave(19, 23, 12); Line(8, 12, 16, 12);
                    Line(9, 5, 15, 19); Line(15, 5, 9, 19);
                    break;
                case Action.Delete:
                    Line(3, 6, 21, 6); Box(8, 2, 8, 4);
                    Poly(new(5, 6), new(6, 22), new(18, 22), new(19, 6));
                    Line(10, 10, 10, 18); Line(14, 10, 14, 18);
                    break;
            }
        }
        finally { graphics.Restore(state); }
    }
}
