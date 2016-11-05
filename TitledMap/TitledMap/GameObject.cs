using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TitledMap
{
    class GameObject
    {
        public int _id;
        public Rectangle _bounds;
        public Point pointStart;
        public Point pointEnd;
        public int type;

        public GameObject(int id, Rectangle bounds, int type)
        {
            this._id = id;
            this._bounds = bounds;
            this.type = type;
        }
        public GameObject(int id, Rectangle bounds, int type, Point pointStart, Point pointEnd)
        {
            this._id = id;
            this._bounds = bounds;
            this.type = type;
            this.pointStart = pointStart;
            this.pointEnd = pointEnd;
        }
        public string export()
        {
            return _id.ToString() + " " + type.ToString() + " " + _bounds.X.ToString() + " " + _bounds.Y.ToString() + " " + _bounds.Width.ToString() + " " + _bounds.Height.ToString()
                + " " + pointStart.X + " " + pointStart.Y + " " + pointEnd.X + " " + pointEnd.Y;
        }
    }
}
