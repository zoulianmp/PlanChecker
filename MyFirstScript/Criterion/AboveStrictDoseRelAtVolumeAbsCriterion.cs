﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanChecker
{
    [Serializable]
    public class AboveStrictDoseRelAtVolumeAbsCriterion : Criterion
    {
        public AboveStrictDoseRelAtVolumeAbsCriterion()
        {
        }
        public AboveStrictDoseRelAtVolumeAbsCriterion(AboveStrictDoseRelAtVolumeAbsCriterion c) : base(c) { }

        public AboveStrictDoseRelAtVolumeAbsCriterion(double inDose, double inVolume, int inPriority, int inPartPlan)
        {
            dose = inDose;
            volume = inVolume;
            priotity = inPriority;
            name = "D(%) >" + dose.ToString() + " at V=" + inVolume.ToString() + " cc";
            partPlan = inPartPlan;
        }
        public override Criterion Copy()
        { return new AboveStrictDoseRelAtVolumeAbsCriterion(this); }
        public override bool Analyze(VMS.TPS.Common.Model.API.PlanSetup planSetup, VMS.TPS.Common.Model.API.Structure eclipseStructure)
        {
            readValue = planSetup.GetDoseAtVolume(eclipseStructure, volume, VMS.TPS.Common.Model.Types.VolumePresentation.AbsoluteCm3, VMS.TPS.Common.Model.Types.DoseValuePresentation.Relative).Dose;
            passed = (readValue > dose);
            return passed;
        }
    }
}
