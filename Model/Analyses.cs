using ousiaAPI.Model;
using System.Security.Cryptography.X509Certificates;

namespace ousiaAPI.Model
{
    public class Analyses
    {

        
        public RVE microMechanics(Fiber fiber, Resin resin, double fiberVolumeFraction)
        {
            RVE outputRVE = new RVE(fiber, resin, fiberVolumeFraction);

            //All the code goes here to create a new laminate using micromechanics 

            outputRVE.tensileModulus[1] = fiber.TensileModulus * fiberVolumeFraction + resin.TensileModulus * (1 - fiberVolumeFraction);
            outputRVE.tensileModulus[2] = 1 / ((fiberVolumeFraction / fiber.TensileModulus) + (1 - fiberVolumeFraction) / resin.TensileModulus);

            outputRVE.poissonRatio[1] = fiber.poissonRatio * fiberVolumeFraction + resin.poissonRatio * (1 - fiberVolumeFraction);

            //The method of the inverse ROM is used. Not accurate, but underestimates it.
            outputRVE.shearModulus[1] = resin.ShearModulus / ((1 - fiberVolumeFraction) + (fiberVolumeFraction * resin.ShearModulus / fiber.ShearModulus));
            outputRVE.shearModulus[3] = outputRVE.shearModulus[1];
            double eta23 = (3 - 4 * resin.poissonRatio + resin.ShearModulus / fiber.ShearModulus) / (4 * (1 - resin.poissonRatio));
            outputRVE.shearModulus[2] = resin.ShearModulus * (fiberVolumeFraction + eta23 * (1 - fiberVolumeFraction)) / (eta23 * (1 - fiberVolumeFraction) + fiberVolumeFraction * resin.ShearModulus / fiber.ShearModulus);


            //Add strength properties obtained through micro mechanics.
            //Add thermanl part


            //Up Variables. Down math.


            //Code on top of this, only return below

            return outputRVE;
        }
        
        public Laminate macroMechanics(Ply[] stackOfPlies)
        {
            //Assumptions
            //1. Thin laminate. Shear is constant through the laminate
            //2. Normal strain ezz is 0.
            Laminate outputLaminate = new Laminate(stackOfPlies);

            //Calculate matrix A
            for (int i = 0; i < stackOfPlies.Length; i++)
            {
                for (int j = 0; j < stackOfPlies[i].matrixQslash.GetLength(0); j++)
                {
                    for (int k = 0; k < stackOfPlies[i].matrixQslash.GetLength(1); k++)
                    {
                        outputLaminate.matrixA[j, k] = stackOfPlies[i].plyThickness * stackOfPlies[i].matrixQslash[j, k];
                    }
                }
            }

            //Calculate matrix B
            for (int i = 0; i < stackOfPlies.Length; i++)
            {
                for (int j = 0; j < stackOfPlies[i].matrixQslash.GetLength(0); j++)
                {
                    for (int k = 0; k < stackOfPlies[i].matrixQslash.GetLength(1); k++)
                    {
                        outputLaminate.matrixB[j, k] = stackOfPlies[i].plyThickness * stackOfPlies[i].matrixQslash[j, k] * outputLaminate.distanceNeutralAxis[i];
                    }
                }
            }


            //Calculate matrix D
            for (int i = 0; i < stackOfPlies.Length; i++)
            {
                for (int j = 0; j < stackOfPlies[i].matrixQslash.GetLength(0); j++)
                {
                    for (int k = 0; k < stackOfPlies[i].matrixQslash.GetLength(1); k++)
                    {
                        outputLaminate.matrixB[j, k] = stackOfPlies[i].matrixQslash[j, k] * (stackOfPlies[i].plyThickness * Math.Pow(outputLaminate.distanceNeutralAxis[i],2) + Math.Pow(stackOfPlies[i].plyThickness,3)/12) ;
                    }
                }
            }

            //Obtain ABD matrix

            outputLaminate.matrixABD = new double[,]
            {
                { outputLaminate.matrixA[0,0],outputLaminate.matrixA[0,1],outputLaminate.matrixA[0,2],outputLaminate.matrixB[0,0],outputLaminate.matrixB[0,1],outputLaminate.matrixB[0,2] },
                { outputLaminate.matrixA[1,0],outputLaminate.matrixA[1,1],outputLaminate.matrixA[1,2],outputLaminate.matrixB[1,0],outputLaminate.matrixB[1,1],outputLaminate.matrixB[1,2] },
                { outputLaminate.matrixA[2,0],outputLaminate.matrixA[2,1],outputLaminate.matrixA[2,2],outputLaminate.matrixB[2,0],outputLaminate.matrixB[2,1],outputLaminate.matrixB[2,2] },
                { outputLaminate.matrixB[0,0],outputLaminate.matrixB[0,1],outputLaminate.matrixB[0,2],outputLaminate.matrixD[0,0],outputLaminate.matrixD[0,1],outputLaminate.matrixD[0,2] },
                { outputLaminate.matrixB[1,0],outputLaminate.matrixB[1,1],outputLaminate.matrixB[1,2],outputLaminate.matrixD[1,0],outputLaminate.matrixD[1,1],outputLaminate.matrixD[1,2] },
                { outputLaminate.matrixB[2,0],outputLaminate.matrixB[2,1],outputLaminate.matrixB[2,2],outputLaminate.matrixD[2,0],outputLaminate.matrixD[2,1],outputLaminate.matrixD[2,2] }
            };

            //Obtain the inverse of ABD

            outputLaminate.inverseMatrixABD = Utils.inverseMatrix(outputLaminate.matrixABD);

            outputLaminate.tensileModulusInPlane[0] = 1/(Math.Pow(outputLaminate.totalLaminateThickness,3) * outputLaminate.inverseMatrixABD[3,3]);
            outputLaminate.tensileModulusInPlane[1] = 1 / (Math.Pow(outputLaminate.totalLaminateThickness, 3) * outputLaminate.inverseMatrixABD[4, 4]);
            outputLaminate.shearModulusInPlane[0] = 1 / (Math.Pow(outputLaminate.totalLaminateThickness, 3) * outputLaminate.inverseMatrixABD[5, 5]);
            outputLaminate.poissonRatioInPlane[0] = outputLaminate.matrixABD[0, 1] / outputLaminate.matrixABD[1, 1];

            outputLaminate.tensileModulusBending[0] = 1 / (outputLaminate.totalLaminateThickness * outputLaminate.inverseMatrixABD[0, 0]);
            outputLaminate.tensileModulusBending[1] = 1 / (outputLaminate.totalLaminateThickness * outputLaminate.inverseMatrixABD[1, 1]);
            outputLaminate.shearModulusBending[0] = 1 / (outputLaminate.totalLaminateThickness * outputLaminate.inverseMatrixABD[2, 2]);
            outputLaminate.poissonRatioBending[0] = outputLaminate.matrixABD[3, 4] / outputLaminate.matrixABD[4, 4];


            return outputLaminate;
        }
    }
}
