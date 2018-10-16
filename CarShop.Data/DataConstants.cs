namespace CarShop.Data
{
    public static class DataConstants
    {
        public const int CommentTitleMinLength = 3;
        public const int CommentTitleMaxLength = 50;
        public const int CommentContentsMinLength = 300;

        public const int DealerNameMaxLength = 100;
        public const int DealerDescriptionMaxLength = 1000;

        public const int ExtraNameMaxLength = 50;
        public const int ExtraDescriptionMaxLength = 1000;

        public const int BrandNameMaxLength = 100;
        public const int BrandDescriptionMaxLength = 1000;

        public const double CarExtraMinDiscount = 0;
        public const double CarExtraMaxDiscount = 100;
        public const double CarExtraMinPrice = 0.01;
        public const double CarExtraMaxPrice = double.MaxValue;
        public const double CarExtraMinQuantity = 0;
        public const double CarExtraMaxQuantity = int.MaxValue;

        public const int CarDescriptionMaxLength = 1000;
        public const int CarMinLength = 1;
        public const int CarMaxLength = int.MaxValue;
        public const int CarTitleMaxLength = 300;

        public const int UserNameMinLength = 2;
        public const int UserNameMaxLength = 100;
        public const int UserUsernameMaxLength = 50;
    }
}
