using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ForecastAndAnalysing
{
    class forecast_LineralTrendCalculation
    {

        public void ForecastSetup_CalculateRegressionParameters(DataRow[] foundRows, DateTime dtm, int iDateColumnIndex, ref double dA, ref double dB)
        {

            // wyliczanie trendu polega na wyciagniecu daty poczatkowej, i wyznaczeniu ze zgromadzonych dat parametrow

            double dAvgDates = 0;
            double dAvgTyp = 0;
            int kl = 0;

            double dSumLicznik = 0;
            double dSumMianownik = 0;

            int calculatedColumn = 2;

            // dane potrzebne do wyliczenia sredniej
            foreach (DataRow drw in foundRows)
            {
                if (dtm <= (DateTime)drw[iDateColumnIndex])
                {

                    dAvgDates += double.Parse(drw[0].ToString());
                    dAvgTyp += double.Parse(drw[calculatedColumn].ToString());
                    kl++;
                }
            }

            dAvgDates = dAvgDates / kl;
            dAvgTyp = dAvgTyp / kl;

            foreach (DataRow drw in foundRows)
            {
                if (dtm <= (DateTime)drw[iDateColumnIndex])
                {
                    dSumLicznik +=
                        (
                            double.Parse(drw[0].ToString())
                                -
                            dAvgDates
                        ) *
                        (
                            double.Parse(drw[calculatedColumn].ToString())
                                -
                            dAvgTyp
                        );
                    dSumMianownik += Math.Pow(
                            double.Parse(drw[0].ToString())
                                -
                            dAvgDates
                            , 2
                        );
                }
            }

            if (dSumMianownik != 0)
                dB = dSumLicznik / dSumMianownik;
            else
                dB = 0;

            dA = dAvgTyp - dB * dAvgDates;
        }

    }
}
