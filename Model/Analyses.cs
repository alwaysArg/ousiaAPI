using System.Security.Cryptography.X509Certificates;

namespace propSol.Model
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
            Laminate outputLaminate = new Laminate(stackOfPlies);


            return outputLaminate;
        }
    }
}
