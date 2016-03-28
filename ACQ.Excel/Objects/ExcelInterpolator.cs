﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ExcelDna.Integration;
using ExcelDna.Logging;

namespace ACQ.Excel.Objects
{
    public class ExcelInterpolator
    {
        private static readonly string m_tag = "#Interpolator";
        private static readonly string m_defaultInterpolator = "Linear";

        [ExcelFunction(Description = "Create Interpolator Object", Category = AddInInfo.Category)]
        public static object acq_interpolator_create(double[] x, double[] y, object method, object bounds)
        {
            if (ExcelDnaUtil.IsInFunctionWizard())
                return ExcelError.ExcelErrorRef;
            else
            {
                ACQ.Math.Interpolation.InterpolationInterface interpolator = null;
                try
                {
                    string interpolation_method = ExcelHelper.Check(method, m_defaultInterpolator);
                    bool interpolation_bounds = ExcelHelper.CheckValue(bounds, true);

                    interpolator = ACQ.Math.Interpolation.InterpolationFactory.GetInterpolator(interpolation_method, x, y, interpolation_bounds);
                }
                catch (Exception ex)
                {
                    LogDisplay.WriteLine("Error: " + ex.ToString());
                }

                if (interpolator == null)
                    return ExcelError.ExcelErrorNull;
                else
                {
                    return ACQ.Excel.Handles.GlobalCache.CreateHandle(m_tag, new object[] { x, y, method, bounds, "acq_interpolator_create" },
                        (objectType, paramaters) =>
                        {
                            return interpolator;

                        });
                }

            }
        }

        [ExcelFunction(Description = "Evaluate Interpolation", Category = AddInInfo.Category)]
        public static object acq_interpolator_eval(string handle, double x)
        {
            ACQ.Math.Interpolation.InterpolationInterface interpolator;

            if (ACQ.Excel.Handles.GlobalCache.TryGetObject<ACQ.Math.Interpolation.InterpolationInterface>(handle, out interpolator))
            {
                if (interpolator != null)
                {
                    return ExcelHelper.CheckNan(interpolator.Eval(x));
                }
            }
            return ExcelError.ExcelErrorRef;
        }
    }
}
