namespace ousiaAPI.Model
{
    public class Laminate
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        //Laminate is composed with layer 0 on male mould side. Laminate in i=0, is bottom lamiante.
        public Ply[] stackOfPlies = new Ply[100];

        public double[,] matrixA = new double[3, 3];
        public double[,] matrixB = new double[3, 3];
        public double[,] matrixD = new double[3, 3];
        public double[,] matrixABD = new double[6,6];
        public double[,] inverseMatrixABD =  new double[6,6];

        public double totalLaminateThickness = 0;

        public double[] distanceNeutralAxis;

        public Boolean isSymetric;

        //Variables for the equivalent laminate obtained
        //Lamiants have all equivalent variables both for bending and inplane stresses
        
        public double[] tensileModulusInPlane = new double[2]; //Tensile modulus in 11 and 22
        public double[] tensileModulusBending = new double[2]; //Tensile modulus in 11 and 22
        public double[] shearModulusInPlane = new double[3]; // Shear modlus in 12,13,23
        public double[] shearModulusBending = new double[3]; // Shear modlus in 12,13,23
        public double volumeFraction { get; set; }//Fiber volume fraction
        public double weightFraction { get; set; } //Fiber weight fraction
        public double[] poissonRatioInPlane = new double[2]; // Poisson ratio
        public double[] poissonRatioBending = new double[2]; // Poisson ratio

        public enum failureType
        {

        }

        public Laminate(Ply[] stackOfPlies)
        {

            this.stackOfPlies = stackOfPlies;
            for (int i = 0; i < stackOfPlies.Length; i++)
            {
                totalLaminateThickness += stackOfPlies[i].plyThickness;
            }

            distanceNeutralAxis = new double[stackOfPlies.Length];
            double auxForDistance1 = 0;

            for (int i = 0; i < stackOfPlies.Length; ++i)
            {
                distanceNeutralAxis[i] = Math.Abs(totalLaminateThickness/2 - auxForDistance1 - stackOfPlies[i].plyThickness/2);
                auxForDistance1 += stackOfPlies[i].plyThickness;
            }

        }

    }

    public class LoadedLaminate : Laminate
    {
        public Load load { get;set;}
        public double[] strains { get; set; } = new double[6];
        public double[] stresses { get; set; } = new double[6];
        public LoadedLaminate(Ply[] stackOfPlies, Load loades) : base(stackOfPlies)
        {
            load = loades;
        }

    }
}
