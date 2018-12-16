using CVGS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVGS.Utility
{
    public class SD
    {
        public const string SuperAdminUser = "Super Admin";
        public const string EmployeeUser = "Employee";
        public const string MemberUser = "Member";

        public const string DefaultGameImage = "default_product.jpg";
        public const string ImageFolder = @"images\GameImages";

        public const string DefaultGameExe = "default_exe.jpg";
        public const string ExeFolder = @"GameExes";

        public const string GamePic = "GamePic";
        public const string GameExe = "GameExe";

        public const int page = 1;
        public const int pageSize = 6;

        public const string hashKey = "fd131212!321321!@#";

        //Will find a better way to store this later
        public const string sendGridKey = "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";

        public static float CalculateRating(List<Ratings> ratings)
        {
            var gCount = ratings.Count();
            float gTotalRatings = 0;
            foreach (var item in ratings)
            {
                gTotalRatings += item.Rating;
            }
            double gFinalRating = gTotalRatings / gCount;
            float gRoundedFinalRating = (float)Math.Round(gFinalRating, 2);
            return gRoundedFinalRating;
            
        }
        public static double[] CalculateTaxes(string province, double cartTotal)
        {
           
            double taxRate = 0;
            double taxAmount = 0;
            double cartTotalPlusTax = 0;

            switch (province)
            {
                case "ON":
                    taxRate = .13;
                    break;
                case "AL":
                    taxRate = .05;
                    break;
                case "BC":
                    taxRate = .12;
                    break;
                case "NL":
                    taxRate = .15;
                    break;
                case "NWT":
                    taxRate = .05;
                    break;
                case "NS":
                    taxRate = .15;
                    break;
                case "NU":
                    taxRate = .05;
                    break;
                case "PEI":
                    taxRate = .15;
                    break;
                case "QB":
                    taxRate = .14975;
                    break;
                case "SA":
                    taxRate = .11;
                    break;
                case "YU":
                    taxRate = .05;
                    break;
            }
            taxAmount = cartTotal * taxRate;
            cartTotalPlusTax = cartTotal + taxAmount;

            double[] TaxValueAndTotal = { taxAmount, cartTotalPlusTax };
            return TaxValueAndTotal;
        }
    }
}
