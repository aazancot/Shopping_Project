using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Analytics.AnalyticsByCategory.CarouselCategories
{
    public interface ICarouselControl
    {
        double Rotate(double startXInScreenCoordinates, double endXInScreenCoordinates);
        void RotateIncrement(int increment);
        void RotateRight();
        void RotateLeft();
        bool ShowRotation { get; set; }
    }
}
