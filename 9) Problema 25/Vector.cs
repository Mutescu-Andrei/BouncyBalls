using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9__Problema_25
{
    class Vector
    {
        private double x, y, z;

        public double Z
        {
            get { return z; }
            set { z = value; }
        }

        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        public double X
        {
            get { return x; }
            set { x = value; }
        }
    
        //const
        
        public Vector()
        {
            x = 0.0;
            y = 0.0;
            z = 0.0;
        }
      //const
        public Vector(double x1, double y1, double z1)
        {
            x = x1;
            y = y1;
            z = z1;
        }
       //const
        public Vector(Vector v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
        }
   
      

        public double Dim()
        {
            return Math.Sqrt(x * x + y * y + z * z);
        }

        public bool Egal(Vector v2)
        {
            if (x == v2.X && y == v2.Y && z == v2.Z) //daca sunt egale cu altul
                return true;
            else
                return false;
        }
        public double ProdusScalar(Vector v2)
        {
            return (x * v2.X + y * v2.Y + z * v2.Z);
        }

        public Vector ProdusVectorial(Vector v2)
        {
            return new Vector(y * v2.Z - z * v2.Y, z * v2.X - x * v2.Z, x * v2.Y - y * v2.X);
        }
        public double Unghi(Vector v2)
        {
            double val = 0;
            double numarator = this.ProdusScalar(v2);
            double numitor = this.Dim() * v2.Dim();
            double unghi0;
           
            if (numitor != 0)
                val = numarator / numitor;
            else
                return 0;


            //punem cosinusul in [-1,1]
            if (val > 1) val = 1;
            if (val < -1) val = -1;
            unghi0 = Math.Acos(val);
            return (unghi0 * 180 / Math.PI);
        }
        public Vector Unitate()
        {
            double dim = Math.Sqrt(x * x + y * y + z * z);
            return new Vector(x / dim, y / dim, z / dim);
        }

        public Vector Proiectie(Vector v2)
        {
            double norma, produsScalar, scalar2;
            norma = Dim() * Dim();
            produsScalar = ProdusScalar(v2);
            if (norma != 0)
                scalar2 = produsScalar / norma;
            else
                return new Vector();
            return new Vector(this.scalar(scalar2));
        }

        public Vector Perpendiculara(Vector v2)
        {
            //pentru a lua cea pe care e perpendicualara(scadem paralela din vectorul original)

            return new Vector(v2 - this.Proiectie(v2)); // perpendiculara = vector - proiectie
        }

        public Vector scalar(double scalar)
        {
            return new Vector(scalar * x, scalar * y, scalar * z); //scalar
        }

        public static Vector operator *(double k, Vector v1)
        {
            return new Vector(k * v1.x, k * v1.y, k * v1.z);
        }

        public static Vector operator *(Vector v1, double k)
        {
            return new Vector(k * v1.x, k * v1.y, k * v1.z);
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z); 
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }
        //ca sa avem un singur vector
        public static Vector operator -(Vector v1)
        {
            return new Vector(-v1.x, -v1.y, -v1.z);
        }
    }
}
