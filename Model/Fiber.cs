namespace propSol.Model
{
    public enum Material
    {
        Carbon,
        Glass,
        Aramid,
        
    }
    public class Fiber(string name)

    {
        
        private Material _material { get; set; } //
        public double TensileModulus { get; set; } //Young Modulus, commonly used letter: E [MPa]

        public double density { get; set; } // [kg/m³]

        public double ShearModulus { get; set; } //Young Modulus, commonly used letter: E [MPa]

        public double poissonRatio { get; set; } //Young Modulus, commonly used letter: E [MPa]

       


    }
}
