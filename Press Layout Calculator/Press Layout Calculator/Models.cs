using System;
using System.Collections.Generic;
using System.Text;

namespace Press_Layout_Calculator
{
    class Models
    {
        public class Calculate
        {
            public string StockWidth { get; set; }
            public string StockHeight { get; set; }
            public string FileWidth { get; set; }
            public string FileHeight { get; set; }
            public string GuttersWidth { get; set; }
            public string GuttersHeight { get; set; }
            public string Reserved { get; set; }
            public bool RotationLocked { get; set; }
        }
        public class Results
        {
            public int NumberUp { get; set; }
            public int Horizontal { get; set; }
            public int Vertical { get; set; }
            public int Rotation { get; set; }
            public int SheetCount { get; set; }
        }
        public class Reserved
        {
            public float Top { get; set; }
            public float Right { get; set; }
            public float Bottom { get; set; }
            public float Left { get; set; }
        }
    }
}
