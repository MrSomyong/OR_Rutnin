using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace DAL
{
    public class Encode
    {
        public string GetSimpleEncode(string plaintext)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(plaintext));
        }
        public string GetSimpleEncode(string plaintext, int maxlength)
        {
            string retVal = Convert.ToBase64String(Encoding.UTF8.GetBytes(plaintext));
            return retVal.Substring(0, (maxlength < retVal.Length ? maxlength : retVal.Length));
        }
        private string _plaintext = string.Empty;
        public string GetMD5Encode(string plaintext)
        {
            this._plaintext = plaintext;
            string retVal = string.Empty;
            retVal = EncodePassword(this._plaintext);
            retVal = RemoveSpecialChar(retVal);
            retVal += BuildSerial(retVal);
            retVal = EncodePassword(retVal);
            retVal = RemoveSpecialChar(retVal);
            return retVal;
        }

        private string GenerateCodes()
        {
            System.Security.Cryptography.HashAlgorithm Hash;
            byte[] HashBytes;
            UnicodeEncoding UNIEncoding = new UnicodeEncoding();
            string temp, _CustRef, _SerialKey;
            int _CustRefLength = 32;
            HashAlgorithms _HashAlgorithm= HashAlgorithms.MD5;

            switch (_HashAlgorithm)
            {
                case HashAlgorithms.MD5:
                    Hash = new MD5CryptoServiceProvider();
                    break;
                case HashAlgorithms.SHA1:
                    Hash = new SHA1CryptoServiceProvider();
                    break;
                case HashAlgorithms.SHA256:
                    Hash = new SHA256Managed();
                    break;
                case HashAlgorithms.SHA384:
                    Hash = new SHA384Managed();
                    break;
                case HashAlgorithms.SHA512:
                    Hash = new SHA512Managed();
                    break;
                default:
                    Hash = new MD5CryptoServiceProvider();
                    break;
            }
            //_DiskSerial = (_DiskSerial == "NONE" ? InstallDateTime() : _DiskSerial);
            //temp = _AppName + _Password + _DiskSerial;
            temp = this._plaintext;
            HashBytes = Hash.ComputeHash(UNIEncoding.GetBytes(temp));

            _CustRef = Convert.ToBase64String(HashBytes);

            _CustRef = RemoveSpecialChar(_CustRef);

            if (_CustRef.Length > _CustRefLength)
                _CustRef = _CustRef.Substring(0, _CustRefLength);

            _CustRef = _CustRef.ToUpper();

            Array.Clear(HashBytes, 0, HashBytes.Length);

            //temp = _CustRef + _AppName + _Password;
            HashBytes = Hash.ComputeHash(UNIEncoding.GetBytes(temp));

            _SerialKey = Convert.ToBase64String(HashBytes);
            _SerialKey = RemoveSpecialChar(_SerialKey);

            if (_SerialKey.Length > _CustRefLength)
                _SerialKey = _SerialKey.Substring(0, _CustRefLength);

            //_SerialKey = "1234567890";
            if (_SerialKey.Length < _CustRefLength)
                _SerialKey = BuildSerial(_SerialKey);

            //_SerialKey = _SerialKey.ToUpper();

            return _SerialKey;
        }

        public string EncodePassword(string originalPassword)
        {
            //Declarations
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;

            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(originalPassword);
            encodedBytes = md5.ComputeHash(originalBytes);

            //Convert encoded bytes back to a 'readable' string
            return BitConverter.ToString(encodedBytes);
        }

        /// <summary>
        /// Remove Special Char
        /// </summary>
        /// <param name="CustRef">String Value</param>
        /// <returns></returns>
        protected string RemoveSpecialChar(string CustRef)
        {
            string specialChar = "!@#$%^&*+/=-";
            string temp = "", t = "";
            int a = 0;
            for (int i = 0; i < CustRef.Length; i++)
            {
                t = CustRef.Substring(i, 1);
                a = specialChar.IndexOf(t);
                if (a < 0)
                    temp += t;
            }
            return temp;
        }
        /// <summary>
        /// Process for incease length of serial if less than 20 char
        /// </summary>
        /// <param name="_SerialKey"></param>
        /// <returns></returns>
        protected string BuildSerial(string _SerialKey)
        {
            string temp = string.Empty;
            string text = Convert.ToBase64String(new UnicodeEncoding().GetBytes("tiffaedi.com"));

            temp = _SerialKey + text;//.Substring(0, 32 - _SerialKey.Length);
            return temp;
        }
    }
}