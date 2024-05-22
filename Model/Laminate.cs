namespace propSol.Model
{
    public class Laminate
    {
        //Laminate is composed with layer 0 on male mould side. Laminate in i=0, is bottom lamiante.
        public Ply[] stackOfPlies = new Ply[100];

        public double[,] matrixA = new double[3, 3];
        public double[,] matrixB = new double[3, 3];
        public double[,] matrixD = new double[3, 3];

        public double totalLaminateThickness = 0;

        public double[] distanceNeutralAxis;

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



        public void laminateMacroMechanics(Ply[] stackOfPlies)
            //Assumptions
            //1. Thin laminate. Shear is constant through the laminate
            //2. Normal strain ezz is 0.
        {
            this.stackOfPlies = stackOfPlies;

            

            //Calculate matrix A
            for ( int i = 0; i < stackOfPlies.Length; i++)
            {
                for ( int j = 0; j < stackOfPlies[i].matrixQslash.GetLength(0); j++)
                {
                    for ( int k = 0; k < stackOfPlies[i].matrixQslash.GetLength(1); k++)
                    {
                        matrixA[j,k] = stackOfPlies[i].plyThickness * stackOfPlies[i].matrixQslash[j,k];
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
                        matrixB[j, k] = stackOfPlies[i].plyThickness * stackOfPlies[i].matrixQslash[j, k] * this.distanceNeutralAxis[i];
                    }
                }
            }

            for (int i = 0; i < stackOfPlies.Length; i++)
            {
                for (int j = 0; j < stackOfPlies[i].matrixQslash.GetLength(0); j++)
                {
                    for (int k = 0; k < stackOfPlies[i].matrixQslash.GetLength(1); k++)
                    {
                        matrixB[j, k] = stackOfPlies[i].plyThickness * stackOfPlies[i].matrixQslash[j, k] * this.distanceNeutralAxis[i];
                    }
                }
            }


        }

        

        
    }
}
