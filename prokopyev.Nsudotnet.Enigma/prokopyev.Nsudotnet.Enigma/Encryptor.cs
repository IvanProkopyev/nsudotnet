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
            byte[] Key = null;
            byte[] IV = null;
            using (SymmetricAlgorithm alg = Codetype.getType(mode)) {
                ICryptoTransform encryptor = alg.CreateEncryptor();
                Key = alg.Key;
                IV = alg.IV;
                using (FileStream fsInput = new FileStream(input, FileMode.Open, FileAccess.Read)) {
                    using (FileStream filestreamout = new FileStream(output, FileMode.Create, FileAccess.Write)) {
                        using (CryptoStream csEncrypt = new CryptoStream(filestreamout, encryptor, CryptoStreamMode.Write)) {
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
            string keyfile = input + ".key.txt";
            string fileKey = Path.Combine(path, keyfile);
            using (FileStream fout = new FileStream(fileKey, FileMode.Create, FileAccess.Write)) {
                using (StreamWriter sw = new StreamWriter(fout)) {
                    string key = Convert.ToBase64String(Key);
                    string iV = Convert.ToBase64String(IV);
                    sw.WriteLine(key);
                    sw.WriteLine(iV);
                }
                Console.WriteLine("key-file created");
            }
        }
    }
}


