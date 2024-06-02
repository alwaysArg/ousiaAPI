namespace ousiaAPI.Model
{

    enum Type
    {
        Epoxy,
        Polyester,
        Vinilester
    }
    public class Resin(string name)
    {
        private Material _material { get; set; } //
        public double TensileModulus { get; set; } //Young Modulus, commonly used letter: E [MPa]

        public double density { get; set; } // [kg/m³]

        public double ShearModulus { get; set; } //Young Modulus, commonly used letter: E [MPa]

        public double poissonRatio { get; set; } //Young Modulus, commonly used letter: E [MPa]

    }
}
