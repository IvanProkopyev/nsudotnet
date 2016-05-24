using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace prokopyev.Nsudotnet.Enigma {
    class Codetype {
        public static SymmetricAlgorithm getType(string mode) {
            switch (mode) {
                case "aes":
                     return new AesManaged();
                case "rc2":
                    return new RC2CryptoServiceProvider();
                case "des":
                    return new DESCryptoServiceProvider();
                case "rijndael":
                    return new RijndaelManaged();
                default:
                    return null;
            }
        }
    }
}
