using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace VehicleDashConcept.UI.Utilities
{
    public static class ImageUtilities
    {

        public static ScaleTransform BoundedImageScale(ScaleTransform st, double minScale, double maxScale, int scaleDelta, double scaleFactor = 0.1)
        {
            if (st != null)
            {
                double zoom = scaleDelta > 0 ? scaleFactor : -(scaleFactor);

                if (st.ScaleX < maxScale && st.ScaleY < maxScale && st.ScaleX > minScale && st.ScaleY > minScale)
                {
                    st.ScaleX += zoom;
                    st.ScaleY += zoom;
                }
                else if ((st.ScaleX >= maxScale || st.ScaleY >= maxScale) && zoom < 0)
                {
                    st.ScaleX += zoom;
                    st.ScaleY += zoom;
                }
                else if ((st.ScaleX <= minScale && st.ScaleY <= minScale) && zoom >= 0)
                {
                    st.ScaleX += zoom;
                    st.ScaleY += zoom;
                }
            }

            return st;
        }

        public static TranslateTransform BoundedImageTranslate(TranslateTransform tt, double xMin, double xMax, double yMin, double yMax, double xDelta, double yDelta)
        {
            if (xDelta < xMax && xDelta > xMin)
            {
                tt.X = xDelta;
            }
            else if ((xDelta >= xMax) && (xDelta < 0))
            {
                tt.X = xDelta;
            }
            else if ((xDelta <= xMin) && (xDelta > 0))
            {
                tt.X = xDelta;
            }

            if (yDelta < yMax && yDelta > yMin)
            {
                tt.Y = yDelta;
            }
            else if ((yDelta >= yMax) && (yDelta < 0))
            {
                tt.Y = yDelta;
            }
            else if ((yDelta <= yMin) && (yDelta > 0))
            {
                tt.Y = yDelta;
            }

            return tt;
        }

    }
}
