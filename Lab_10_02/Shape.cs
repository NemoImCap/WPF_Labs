using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Lab_05_01
{
    /// <summary>
    /// Класс фигуры
    /// </summary>
    [Serializable]
    public class Shape
    {

        private int _strokeThickness = 1;
        private double inradius = 10;
        private double outradius = 50;
        private double _shapePositionX = 0;
        private double _shapePositionY = 0;

        /// <summary>
        /// Позиция фигуры по X
        /// </summary>
        [XmlAttribute]
        public double ShapePositionX { get { return _shapePositionX; } set { _shapePositionX = value; } }
        /// <summary>
        /// Позиция фигуры по Y
        /// </summary>
        [XmlAttribute]
        public double ShapePositionY { get { return _shapePositionY; } set { _shapePositionY = value; } }

        /// <summary>
        /// Толщина линий
        /// </summary>
        public int StrokeThickness { get { return _strokeThickness; } set { _strokeThickness = value; } }

        /// <summary>
        /// Цвет фона
        /// </summary>
        public Color BackgroundColor { get { return Color.FromArgb(AColor, RColor, GColor, BColor); } }
        /// <summary>
        /// Цвет линии
        /// </summary>
        public Color StrokeColor { get { return Color.FromArgb(SAColor, SRColor, SGColor, SBColor); } }

        /// <summary>
        /// Внутренний радиус
        /// </summary>
        public double InnerRadius { get { return inradius; } set { inradius = value; } }
        /// <summary>
        /// Внешний радиус
        /// </summary>
        public double OuterRadius { get { return outradius; } set { outradius = value; } }

        private Color _DefaultColor = Brushes.Blue.Color;
        private Color _DefaultColorStr = Brushes.Black.Color;

        /// <summary>
        /// Красный цвет фона
        /// </summary>
        public byte RColor { get { return _DefaultColor.R; } set { _DefaultColor.R = value; } }
        /// <summary>
        /// Зеленый цвет фона
        /// </summary>
        public byte GColor { get { return _DefaultColor.G; } set { _DefaultColor.G = value; } }
        /// <summary>
        /// Синий цвет фона
        /// </summary>
        public byte BColor { get { return _DefaultColor.B; } set { _DefaultColor.B = value; } }
        /// <summary>
        /// Альфа цвет фона
        /// </summary>
        public byte AColor { get { return _DefaultColor.A; } set { _DefaultColor.A = value; } }

        /// <summary>
        /// Красный цвет линии
        /// </summary>
        public byte SRColor { get { return _DefaultColorStr.R; } set { _DefaultColorStr.R = value; } }
        /// <summary>
        /// Зеленый цвет линии
        /// </summary>
        public byte SGColor { get { return _DefaultColorStr.G; } set { _DefaultColorStr.G = value; } }
        /// <summary>
        /// Синий цвет линии
        /// </summary>
        public byte SBColor { get { return _DefaultColorStr.B; } set { _DefaultColorStr.B = value; } }
        /// <summary>
        /// Альфа цвет линии
        /// </summary>
        public byte SAColor { get { return _DefaultColorStr.A; } set { _DefaultColorStr.A = value; } }

        /// <summary>
        /// Инициализация фигуры и добавление на Canvas
        /// </summary>
        /// <param name="paint">Canvas</param>
        public void InitShape(Canvas paint)
        {
            Polygon polygon = new Polygon
            {
                Stroke = new SolidColorBrush(StrokeColor),
                Fill = new SolidColorBrush(BackgroundColor),
                StrokeThickness = StrokeThickness,
                Stretch = Stretch.Uniform,
                //Point.Parse("20,200 95,185 110,110 125,185 200,200 125,215 110,290 95,215");

                Points = new PointCollection() {
                    new Point(ShapePositionX - OuterRadius, ShapePositionY),
                    new Point(ShapePositionX - InnerRadius, ShapePositionY - InnerRadius),
                    new Point(ShapePositionX, ShapePositionY - OuterRadius),
                    new Point(ShapePositionX + InnerRadius, ShapePositionY - InnerRadius),
                    new Point(ShapePositionX + OuterRadius, ShapePositionY),
                    new Point(ShapePositionX + InnerRadius, ShapePositionY + InnerRadius),
                    new Point(ShapePositionX, ShapePositionY + OuterRadius),
                    new Point(ShapePositionX - InnerRadius, ShapePositionY + InnerRadius)
                    }
            };

            paint.Children.Add(polygon);
            Canvas.SetLeft(polygon, ShapePositionX - OuterRadius);
            Canvas.SetTop(polygon, ShapePositionY - OuterRadius);
        }
    }

}
