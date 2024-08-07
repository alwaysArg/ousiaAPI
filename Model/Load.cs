﻿namespace ousiaAPI.Model
{
    public class Load
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public double[] mechanicalLoads = new double[6]; //Vector that is going to contain Nx, Ny, Nz, Mx, My, Mz.

        public double[] thermalLoads = new double[2];

        public double[] moistureLoads = new double[2];
    }
}
