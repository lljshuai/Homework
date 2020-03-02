using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace work01
{
    class Factory
    {
        public static Shape CreateFunction(int n)
        {
            if (n == 1)
            { 
                return new Rectangle();
            }
            else if (n == 2)
            {
            return new Triangle();
            }
            else if (n == 3)
            {
                return new Square();
            }
            else
            {
                return null;
            }
        }
    }
}
