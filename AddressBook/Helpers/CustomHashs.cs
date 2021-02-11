using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace AddressBook.Helpers
{
    public class CustomHashs
    {
        public byte[] CalculateSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] hashvalue;
            UTF8Encoding objUtf8 = new UTF8Encoding();
            hashvalue = sha256.ComputeHash(objUtf8.GetBytes(str));

            return hashvalue;
        }
    }
}