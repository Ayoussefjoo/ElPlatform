using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElPlatform.BAL.ViewModels
{
    public class AccessTokenResult
    {
        public AccessTokenResult(string token, DateTime expiryDate)
        {
            Token = token;
            ExpiryDate = expiryDate;
        }

        public AccessTokenResult()
        {

        }

        public string Token { get; set; }

        public DateTime ExpiryDate { get; set; }

    }
}
