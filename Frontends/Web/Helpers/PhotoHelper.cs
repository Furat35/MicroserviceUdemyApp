﻿using Microsoft.Extensions.Options;
using Web.Models;

namespace Web.Helpers
{
    public class PhotoHelper
    {
        private readonly ServiceApiSettings _serviceApiSettings;
        public PhotoHelper(IOptions<ServiceApiSettings> serviceApiSettings)
        {
            _serviceApiSettings = serviceApiSettings.Value;
        }

        public string GetPhotoStockUrl(string photoUrl)
            => $"{_serviceApiSettings.PhotoStockUri}/photos/{photoUrl}";
    }
}
