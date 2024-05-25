namespace propSol.Model
{
    public class RVE
        //Representative Volume Element.
        //Is the smallest portion of material that contains all the properties
    {
        public double[] tensileModulus = new double[2]; //Tensile modulus in 11 and 22
        public double[] shearModulus = new double[3]; // Shear modlus in 12,13,23
        public double fiberVolumeFraction; //Fiber volume fraction
        public double resinVolumeFraction;
        public double fiberWeightFraction; //Fiber weight fraction
        public double resinWeightFraction;
        public double[] poissonRatio = new double[2]; // Poisson ratio
        public double[,] fiberOrientationRatio = new double[50,2]; //To obtain properties for the laminate according the amount of fibers in each direction
        public Fiber fiber;
        public Resin resin;

        public RVE(Fiber fiber, Resin resin, double fiberVolumeFraction)
        {
            this.fiber = fiber;
            this.resin = resin;
            this.fiberVolumeFraction = fiberVolumeFraction;

           

            
        }
    }
}
