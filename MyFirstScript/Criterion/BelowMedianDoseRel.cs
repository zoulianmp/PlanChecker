﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanChecker
{
    [Serializable]
    public class BelowMedianDoseRel : Criterion
    {
        public BelowMedianDoseRel()
        {
        }
        public BelowMedianDoseRel(BelowMedianDoseRel c) : base(c) { }

        public BelowMedianDoseRel(double inDose, double inVolume, int inPriority, int inPartPlan)
        {
            dose = inDose;
            partPlan = inPartPlan;
            volume = inVolume;
            priotity = inPriority;
            name = "Median D(%) ≤" + dose.ToString();
        }
        public override Criterion Copy()
        { return new BelowMedianDoseRel(this); }
        public override bool Analyze(VMS.TPS.Common.Model.API.PlanSetup planSetup, VMS.TPS.Common.Model.API.Structure eclipseStructure)
        {

            VMS.TPS.Common.Model.API.DVHData dvh = planSetup.GetDVHCumulativeData(eclipseStructure, VMS.TPS.Common.Model.Types.DoseValuePresentation.Relative, VMS.TPS.Common.Model.Types.VolumePresentation.AbsoluteCm3, 10000);
            if (dvh != null)
            {
                readValue = dvh.MedianDose.Dose;
                passed = (readValue <= dose);
            }
            else
            {
                readValue = double.NaN;
                passed = false;
            }
            return passed;
        }
        /* //for PlanSums
         public override bool Analyze(VMS.TPS.Common.Model.API.PlanSum inplanSum, VMS.TPS.Common.Model.API.Structure eclipseStructure)
         {
             readValue = inplanSum.GetDVHCumulativeData(eclipseStructure, VMS.TPS.Common.Model.Types.DoseValuePresentation.Relative, VMS.TPS.Common.Model.Types.VolumePresentation.AbsoluteCm3, .001).MedianDose.Dose;
             return (readValue < dose);
         }
         */
    }
}
