using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azaliq.GCommon
{
    public static class ValidationConstants
    {
        public static class Product
        {
            public const int NameMaxLength = 100;
            public const int DescriptionMaxLength = 1000;
            public const int ImageUrlMaxLength = 255;
        }

        public static class ProductTag
        {
            public const int NameMaxLength = 50;
        }

        public static class StoreLocation
        {
            public const int NameMaxLength = 100;
            public const int GoogleMapsUrlLength = 500;
            public const int AddressMaxLength = 300;
            public const int PhoneMaxLength = 50;
        }

        public static class Category
        {
            public const int NameMaxLength = 50;
        }

        public static class Store
        {
            public const int NameMaxLength = 50;
            public const string PhoneNumberRegex = @"^\+?(\d[\d\s\-().]{7,}\d)$";
            public const int GoogleMapsUrlLength = 1000;
            public const int AddressMaxLength = 300;
        }

    }
}
