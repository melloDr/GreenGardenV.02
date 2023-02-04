using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace GreeenGarden.Business.Utilities
{
    public class CustomerUtil
    {
        public static bool IsValidPhone(string Phone)
        {
            try
            {
                if (string.IsNullOrEmpty(Phone))
                    return false;
                var r = new Regex(@"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$");
                return r.IsMatch(Phone);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool IsValidEmail(string strIn)
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        public static bool IsValidUserName(string Data)
        {
            try
            {
                if (string.IsNullOrEmpty(Data))
                    return false;
                var r = new Regex(@"^(?:[a-zA-Z0-9]|[-](?=[^-]*$)){2,20}$");
                return r.IsMatch(Data);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool IsValidFullName(string Data)
        {
            try
            {
                if (string.IsNullOrEmpty(Data))
                    return false;
                var r = new Regex(@"^[a-zA-Z]{2,20}$");
                return r.IsMatch(Data);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool checkPasswordValid(string Data)
        {
            try
            {
                if (string.IsNullOrEmpty(Data))
                    return false;
                var r = new Regex("^(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&*()]).{8,20}$");
                return r.IsMatch(Data);

            }
            catch (Exception)
            {
                throw;
            }

        }

        public static string GetMD5(string input)
        {
            using (MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider())
            {
                byte[] b = System.Text.Encoding.UTF8.GetBytes(input);
                b = MD5.ComputeHash(b);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (byte x in b)

                    sb.Append(x.ToString("x2"));

                return sb.ToString();
            }
        }

    }
}
