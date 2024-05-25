using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace propSol.Model
{
    public class Ply
    {
        //Variables
       

        //Methods
        //To create a ply can be done in three ways.
        // By michromechanics. Using this, different plyes will be created to obtain then final one.
        // By definign an existing ply.
        // By different norms which follow the formulas in these.
        // Both ways the end results has to eb sthe same. The idea behind micromechanics
        public double[] tensileModulus = new double[2]; //Tensile modulus in 11 and 22
        public double[] shearModulus = new double[3]; // Shear modlus in 12,13,23
        public double volumeFraction { get; set; }//Fiber volume fraction
        public double weightFraction { get; set; } //Fiber weight fraction
        public double[] poissonRatio = new double[2]; // Poisson ratio
        public double[,] fiberOrientationRatio = new double[50, 2]; //To obtain properties for the laminate according the amount of fibers in each direction
        public double angle { get; set; }

        //Variables neeed to do the Ply mechanics
        public double[,] matrixQ = new double[3,3]; //Reduced stiffness matrix
        public double[,] matrixS = new double[3,3]; //Compliance matrix
        public double mCos, nSin;
        public double[,] matrixQslash = new double[3, 3]; //Reduced stiffness matrix
        public double[,] matrixSslash = new double[3, 3];

        
        //Ply weight makes reference to the gsm for the ply.
        public double   plyWeight {  get; set; }
        public double   plyThickness { get; set; }



        public string name = "New Ply";

        public enum plyType
        {
            [Description("Unidirectional")] Unidirectional = 1,
            [Description("Woven")] Woven = 2,
            [Description("Bi-Axial")] Biaxial = 3,
            [Description("Quadriaxial")] Quadraxial = 4,
            [Description("Custom")] Custom = 5,
        }


        //Parametless constructor, to obtain when doing any type of ply.
        public Ply()
        {

        }


        public Ply(string name) :this()
        {
            this.name = name;
        }


        public void PlyMechanics()
        {
    
            //Define Delta and Stiffness matrix
            double delta = 1 - Math.Pow(poissonRatio[0], 2) * tensileModulus[1] / tensileModulus[0];
            matrixQ[0, 0] = tensileModulus[0] / delta;
            matrixQ[0, 1] = poissonRatio[0] * tensileModulus[1] / delta;
            matrixQ[1, 1] = tensileModulus[1] / delta;
            matrixQ[1, 0] = matrixQ[0, 1];
            matrixQ[2, 2] = shearModulus[0];

            //Define Transformation coordinates matrix
            mCos = Math.Cos(angle * Math.PI / 180);
            nSin = Math.Sin(angle * Math.PI / 180);

            //Define global matrices of stiffness and compliance

            matrixQslash = new double[,]
            {
                //Q11
                {matrixQ[0,0]*Math.Pow(mCos,4) + 2 * (matrixQ[0,1] + 2 * matrixQ[2,2])*Math.Pow(nSin,2)*Math.Pow(mCos,2) + matrixQ[1,1] * Math.Pow(nSin,4),
                //Q12
                (matrixQ[0,0] + matrixQ[1,1] - 4 * matrixQ[2,2]) * Math.Pow(nSin,2) * Math.Pow(mCos,2) + matrixQ[0,1]*(Math.Pow(nSin,4) + Math.Pow(mCos,4)),
                //Q13
                (matrixQ[0,0] - matrixQ[0,1] - 2 * matrixQ[2,2])* Math.Pow(nSin,1) * Math.Pow(mCos,3) + (matrixQ[0,1] - matrixQ[1,1] + 2 * matrixQ[2,2])* Math.Pow(nSin,3) * Math.Pow(mCos,1)   },
                //Q21
                {(matrixQ[0,0] + matrixQ[1,1] - 4 * matrixQ[2,2]) * Math.Pow(nSin,2) * Math.Pow(mCos,2) + matrixQ[0,1]*(Math.Pow(nSin,4) + Math.Pow(mCos,4)),
                //Q22
                matrixQ[0,0] * Math.Pow(nSin,4) + 2* (matrixQ[0,1] + 2 * matrixQ[2,2]) * Math.Pow(nSin,2) * Math.Pow(mCos,2) + matrixQ[1,1] * Math.Pow(mCos,4),
                //Q23
                (matrixQ[0,0] - matrixQ[0,1] - 2 * matrixQ[2,2])* Math.Pow(nSin,3) * Math.Pow(mCos,1) + (matrixQ[0,1] - matrixQ[1,1] + 2 * matrixQ[2,2])* Math.Pow(nSin,1) * Math.Pow(mCos,3) },
                //Q31
                {(matrixQ[0,0] - matrixQ[0,1] - 2 * matrixQ[2,2])* Math.Pow(nSin,1) * Math.Pow(mCos,3) + (matrixQ[0,1] - matrixQ[1,1] + 2 * matrixQ[2,2])* Math.Pow(nSin,3) * Math.Pow(mCos,1),
                //Q32
                 (matrixQ[0,0] - matrixQ[0,1] - 2 * matrixQ[2,2])* Math.Pow(nSin,3) * Math.Pow(mCos,1) + (matrixQ[0,1] - matrixQ[1,1] + 2 * matrixQ[2,2])* Math.Pow(nSin,1) * Math.Pow(mCos,3),
                //Q66
                (matrixQ[0,0] + matrixQ[1,1] - 2 * matrixQ[0,1] - 2 * matrixQ[2,2]) * Math.Pow(nSin,2) * Math.Pow(mCos,2) + matrixQ[2,2] * ( Math.Pow(nSin,4) + Math.Pow(mCos,4))  }
            };

        }

        public void getCombinedPly(Ply[] listOfPlies)
        {
      

            double totalListWeight = 0;

            for(int i = 0; i < listOfPlies.Length; i++)
            {
                totalListWeight += listOfPlies[i].plyWeight;
            }


            this.plyWeight = totalListWeight;

            //Three nested for. First, for goign through the list. j and k, to go through the matrix Qslash.
            for(int i = 0; i < listOfPlies.Length ; i++)
            {
                for(int j = 0; j < listOfPlies[i].matrixQslash.GetLength(0) ; j++)
                    for(int k = 0; k < listOfPlies[i].matrixQslash.GetLength(1); k++)
                {
                    this.matrixQslash[j, k] += listOfPlies[i].matrixQslash[j, k] * listOfPlies[i].plyWeight / totalListWeight;
                }
            }

        }



    }
}
