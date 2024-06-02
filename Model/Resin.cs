namespace ousiaAPI.Model
{

    enum ResinMaterial
    {
        Epoxy,
        Polyester,
        Vinilester
    }
    public class Resin()
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        private ResinMaterial _material { get; set; } //
        public double TensileModulus { get; set; } //Young Modulus, commonly used letter: E [MPa]

        public double density { get; set; } // [kg/m³]

        public double ShearModulus { get; set; } //Young Modulus, commonly used letter: E [MPa]

        public double poissonRatio { get; set; } //Young Modulus, commonly used letter: E [MPa]

    }
}
