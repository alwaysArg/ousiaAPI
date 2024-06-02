namespace ousiaAPI.Model
{
    public class PlyTest : Ply
    {

        public static Ply TestCBX400()
        {
            return new Ply
            {
                Name = "Carbon Bi-Axial 400 gsm",
                volumeFraction = 0.5,
                tensileModulus = [23000, 23000],
                shearModulus = [40000, 3500, 3500],
            };


        }

        public static Ply EUD400()
        {
            return new Ply
            {
                Name = "E-Glass Unidirectional 400 gsm at 45 degrees",
                volumeFraction = 0.5,
                tensileModulus = [37864, 11224],
                shearModulus = [3317, 3500, 3500],
                angle = 45,
                poissonRatio = [0.3,0.3],
                plyWeight = 400,


            };

        }
        public static Ply EUD400Neg()
        {
            return new Ply
            {
                Name = "E-Glass Unidirectional 400 gsm at 45 degrees",
                volumeFraction = 0.5,
                tensileModulus = [37864, 11224],
                shearModulus = [3317, 3500, 3500],
                angle = -45,
                poissonRatio = [0.3, 0.3],
                plyWeight = 400,


            };

        }

        public static Ply CSM225()
        {
            return new Ply
            {
                Name = "CSM at 0",
                volumeFraction = 0.50,
                tensileModulus = [21214, 21214],
                shearModulus = [7539, 3500, 3500],
                angle = 0,
                poissonRatio = [0.407, 0.407],
                plyWeight = 225,


            };
        }
    }
}

