namespace propSol.Model
{
    public class RVE
        //Representative Volume Element.
        //Is the smallest portion of material that contains all the properties
    {
        public double[] tensileModulus = new double[2]; //Tensile modulus in 11 and 22
        public double[] shearModulus = new double[3]; // Shear modlus in 12,13,23
        public double volumeFraction; //Fiber volume fraction
        public double weightFraction; //Fiber weight fraction
        public double[] poissonRatio = new double[2]; // Poisson ratio
        public double[,] fiberOrientationRatio = new double[50,2]; //To obtain properties for the laminate according the amount of fibers in each direction


        public RVE(Fiber fiber, Resin resin, double fiberVolumeFraction)
        {
            //All the code goes here to create a new laminate using micromechanics 

            tensileModulus[1] = fiber.TensileModulus * fiberVolumeFraction + resin.TensileModulus * (1 - fiberVolumeFraction);
            tensileModulus[2] = 1 / ((fiberVolumeFraction / fiber.TensileModulus) + (1 - fiberVolumeFraction) / resin.TensileModulus);

            poissonRatio[1] = fiber.poissonRatio * fiberVolumeFraction + resin.poissonRatio * (1 - fiberVolumeFraction);

            //The method of the inverse ROM is used. Not accurate, but underestimates it.
            shearModulus[1] = resin.ShearModulus / ((1 - fiberVolumeFraction) + (fiberVolumeFraction * resin.ShearModulus / fiber.ShearModulus));
            shearModulus[3] = shearModulus[1];
            double eta23 = (3 - 4 * resin.poissonRatio + resin.ShearModulus / fiber.ShearModulus) / (4 * (1 - resin.poissonRatio));
            shearModulus[2] = resin.ShearModulus * (fiberVolumeFraction + eta23 * (1 - fiberVolumeFraction)) / (eta23 * (1 - fiberVolumeFraction) + fiberVolumeFraction * resin.ShearModulus / fiber.ShearModulus);


            //Add strength properties obtained through micro mechanics.
            //Add thermanl part


            //Up Variables. Down math.


            //Code on top of this, only return below

            
        }
    }
}
