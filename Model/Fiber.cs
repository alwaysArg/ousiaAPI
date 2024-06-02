namespace ousiaAPI.Model
{
    public enum FiberMaterial
    {
        Carbon,
        Glass,
        Aramid,
        
    }
    public class Fiber

    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        private FiberMaterial _material { get; set; } //
        public double TensileModulus { get; set; } //Young Modulus, commonly used letter: E [MPa]

        public double density { get; set; } // [kg/m³]

        public double ShearModulus { get; set; } //Young Modulus, commonly used letter: E [MPa]

        public double poissonRatio { get; set; } //Young Modulus, commonly used letter: E [MPa]

       


    }
}
