using System;
using System.Collections.Generic;
using System.Text;

namespace RegretionBasedDll
{
    public class CorrelatedFeatures
    {
        string feature1, feature2;
        double correlation, threshold;
        double features_x_max, features_x_min;
        Line lin_reg;
        Circle minCircle;
        bool isHybrid;

        public CorrelatedFeatures(string f1, string f2, double fs_x_max, double fs_x_min, double c, double thres, Line lin_reg, Circle mC, bool isHybrid)
        {
            this.feature1 = f1;
            this.feature2 = f2;
            this.features_x_max = fs_x_max;
            this.features_x_min = fs_x_min;
            this.correlation = c;
            this.threshold = thres;
            this.lin_reg = lin_reg;
            this.minCircle = mC;
            this.isHybrid = isHybrid;
        }

        public string GetFeature1() { return this.feature1; }
        public string GetFeature2() { return this.feature2; }
        public double GetCorrelation() { return this.correlation; }
        public double GetThreshold() { return this.threshold; }
        public Line GetLin_Reg() { return this.lin_reg; }
        public Circle GetMinCircle() { return this.minCircle; }
        public bool IsHybrid() { return this.isHybrid; }

        public double GetMaxXFeatures() { return this.features_x_max; }
        public double GetMinXFeatures() { return this.features_x_min; }
    }
}
