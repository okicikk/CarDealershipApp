namespace CarDealershipApp.Constants
{
    public static class Constants
    {
        public const decimal CarPriceMinValue = 200.00m;
        public const decimal CarPriceMaxValue = 3_000_000.00m;

        public const double CarMinWeight = 380;
        public const double CarMaxWeight = 3500;

        public const int CarDescriptionMinLenght = 5;
        public const int CarDescriptionMaxLenght = 500;

        public const int CarMileageMinValue = 0;
        public const int CarMileageMaxValue = 1_000_000;

        public const int BrandNameMinLength = 2;
        public const int BrandNameMaxLength = 15;

        public const int ModelNameMinLength = 1;
        public const int ModelNameMaxLength = 20;

        public const int CategoryNameMinLength = 3;
        public const int CategoryNameMaxLength = 15;

        public const int FeatureNameMinLength = 2;
        public const int FeatureNameMaxLength = 50;

        public const string DefaultBrandImage = "https://coffective.com/wp-content/uploads/2018/06/default-featured-image.png.jpg";

    }
}
