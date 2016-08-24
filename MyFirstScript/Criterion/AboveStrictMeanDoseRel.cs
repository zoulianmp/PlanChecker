﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanChecker
{ [Serializable]
    public class AboveStrictMeanDoseRel : Criterion
    {
       public AboveStrictMeanDoseRel()
        {
        }
       public AboveStrictMeanDoseRel(AboveStrictMeanDoseRel c) : base(c) { }

       public AboveStrictMeanDoseRel(double inDose, double inVolume, int inPriority, int inPartPlan)
        {
            dose = inDose;
            volume = inVolume;
            priotity = inPriority;
            partPlan = inPartPlan;
            name = "Mean D(%) >" + dose.ToString();
        }
       public override Criterion Copy()
       { return new AboveStrictMeanDoseRel(this); }
        public override bool Analyze(VMS.TPS.Common.Model.API.PlanSetup planSetup, VMS.TPS.Common.Model.API.Structure eclipseStructure)
        {
            VMS.TPS.Common.Model.API.DVHData dvh = planSetup.GetDVHCumulativeData(eclipseStructure, VMS.TPS.Common.Model.Types.DoseValuePresentation.Relative, VMS.TPS.Common.Model.Types.VolumePresentation.AbsoluteCm3, 10000);
            if (dvh != null)
            {
                readValue = dvh.MeanDose.Dose;
                passed = (readValue > dose);
            }
            else
            {
                readValue = double.NaN;
                passed = false;
            }
            return passed;
        }
      /*  //for PlanSums
        public override bool Analyze(VMS.TPS.Common.Model.API.PlanSum inplanSum, VMS.TPS.Common.Model.API.Structure eclipseStructure)
        {
            readValue = inplanSum.GetDVHCumulativeData(eclipseStructure, VMS.TPS.Common.Model.Types.DoseValuePresentation.Relative, VMS.TPS.Common.Model.Types.VolumePresentation.AbsoluteCm3, binWidth).MeanDose.Dose;
            return (readValue > dose);
        }
       */
    }
}