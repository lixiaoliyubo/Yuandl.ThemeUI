// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using Point = System.Windows.Point;
using Size = System.Windows.Size;

// ReSharper disable once CheckNamespace
namespace Yuandl.ThemeUI.Controls;

public class Arc : System.Windows.Shapes.Shape
{
    /// <summary>Identifies the <see cref="StartAngle"/> dependency property.</summary>
    public static readonly DependencyProperty StartAngleProperty = DependencyProperty.Register(
        nameof(StartAngle),
        typeof(double),
        typeof(Arc),
        new PropertyMetadata(0.0d, PropertyChangedCallback)
    );

    /// <summary>Identifies the <see cref="EndAngle"/> dependency property.</summary>
    public static readonly DependencyProperty EndAngleProperty = DependencyProperty.Register(
        nameof(EndAngle),
        typeof(double),
        typeof(Arc),
        new PropertyMetadata(0.0d, PropertyChangedCallback)
    );

    /// <summary>
    /// Gets or sets the initial angle from which the arc will be drawn.
    /// </summary>
    public double StartAngle
    {
        get => (double)GetValue(StartAngleProperty);
        set => SetValue(StartAngleProperty, value);
    }

    /// <summary>
    /// Gets or sets the final angle from which the arc will be drawn.
    /// </summary>
    public double EndAngle
    {
        get => (double)GetValue(EndAngleProperty);
        set => SetValue(EndAngleProperty, value);
    }

    /// <summary>
    /// Gets a value indicating whether one of the two larger arc sweeps is chosen; otherwise, if is <see langword="false"/>, one of the smaller arc sweeps is chosen.
    /// </summary>
    public bool IsLargeArc { get; internal set; } = false;

    /// <inheritdoc />
    protected override Geometry DefiningGeometry => GetDefiningGeometry();

    /// <summary>
    /// Initializes static members of the <see cref="Arc"/> class.
    /// </summary>
    /// <remarks>
    /// Overrides default properties.
    /// </remarks>
    static Arc()
    {
        StrokeStartLineCapProperty.OverrideMetadata(
            typeof(Arc),
            new FrameworkPropertyMetadata(PenLineCap.Round)
        );

        StrokeEndLineCapProperty.OverrideMetadata(
            typeof(Arc),
            new FrameworkPropertyMetadata(PenLineCap.Round)
        );
    }

    /// <summary>
    /// Get the geometry that defines this shape.
    /// <para><see href="https://stackoverflow.com/a/36756365/13224348">Based on Mark Feldman implementation.</see></para>
    /// </summary>
    protected Geometry GetDefiningGeometry()
    {
        var geometryStream = new StreamGeometry();
        var arcSize = new Size(
            Math.Max(0, (RenderSize.Width - StrokeThickness) / 2),
            Math.Max(0, (RenderSize.Height - StrokeThickness) / 2)
        );

        using (StreamGeometryContext context = geometryStream.Open())
        {
            context.BeginFigure(PointAtAngle(Math.Min(StartAngle, EndAngle)), false, false);

            context.ArcTo(
                PointAtAngle(Math.Max(StartAngle, EndAngle)),
                arcSize,
                0,
                IsLargeArc,
                SweepDirection.Counterclockwise,
                true,
                false
            );
        }

        geometryStream.Transform = new TranslateTransform(StrokeThickness / 2, StrokeThickness / 2);

        return geometryStream;
    }

    /// <summary>
    /// Draws a point on the coordinates of the given angle.
    /// <para><see href="https://stackoverflow.com/a/36756365/13224348">Based on Mark Feldman implementation.</see></para>
    /// </summary>
    /// <param name="angle">The angle at which to create the point.</param>
    protected Point PointAtAngle(double angle)
    {
        var radAngle = angle * (Math.PI / 180);
        var xRadius = (RenderSize.Width - StrokeThickness) / 2;
        var yRadius = (RenderSize.Height - StrokeThickness) / 2;

        return new Point(xRadius + (xRadius * Math.Cos(radAngle)), yRadius - (yRadius * Math.Sin(radAngle)));
    }

    /// <summary>
    /// Event triggered when one of the key parameters is changed. Forces the geometry to be redrawn.
    /// </summary>
    protected static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Arc control)
        {
            return;
        }

        control.IsLargeArc = Math.Abs(control.EndAngle - control.StartAngle) > 180;

        // Force complete new layout pass
        control.InvalidateVisual();
    }
}