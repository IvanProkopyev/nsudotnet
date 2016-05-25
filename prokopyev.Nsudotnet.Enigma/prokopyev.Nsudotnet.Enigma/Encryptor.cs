using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace prokopyev.Nsudotnet.Enigma {
    class Encryptor {
        internal void Encrypt(String mode, String input, String output) {
            byte[] algKey = null;
            byte[] algIV = null;
            using (SymmetricAlgorithm alg = Codetype.GetType(mode)) {
                ICryptoTransform encryptor = alg.CreateEncryptor();
                algKey = alg.Key;
                algIV = alg.IV;
                using (FileStream fsInput = new FileStream(input, FileMode.Open, FileAccess.Read)) {
                    using (FileStream filestreamOut = new FileStream(output, FileMode.Create, FileAccess.Write)) {
                        using (CryptoStream csEncrypt = new CryptoStream(filestreamOut, encryptor, CryptoStreamMode.Write)) {
                            int data;
                            while ((data = fsInput.ReadByte()) != -1) {
                                csEncrypt.WriteByte((byte)data);
                            }
                            Console.WriteLine("binary file created");
                        }
                    }
                }
            }

            string path = Path.GetDirectoryName(input);
            string keyFile = input + ".key.txt";
            string fileKey = Path.Combine(path, keyFile);
            using (FileStream fout = new FileStream(fileKey, FileMode.Create, FileAccess.Write)) {
                using (StreamWriter sw = new StreamWriter(fout)) {
                    string key = Convert.ToBase64String(algKey);
                    string iV = Convert.ToBase64String(algIV);
                    sw.WriteLine(key);
                    sw.WriteLine(iV);
                }
                Console.WriteLine("key-file created");
            }
        }
    }
}


