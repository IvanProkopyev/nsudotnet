using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace prokopyev.Nsudotnet.Enigma{
    class Encryptor{
        internal void Encrypt(String mode, String input, String output) {
            //  encrypt c:\Users\ivqn\Desktop\1.jpg  aes c:\Users\ivqn\Desktop\1.bin
     //       try {
                byte[] Key = null;
                byte[] IV = null;
                ICryptoTransform encryptor = null;
                SymmetricAlgorithm alg = null;
                switch (mode) {
                    case "aes":
                    using (Aes aes = Aes.Create()) {
                        Key = aes.Key;
                        IV = aes.IV;
                        encryptor = aes.CreateEncryptor();
                    }
                        break;

                    case "des":
                        using (alg = new DESCryptoServiceProvider()) {
                            Key = alg.Key;
                            IV = alg.IV;
                            encryptor = alg.CreateEncryptor();
                        }
                        break;

                    case "rc2":
                        using (alg = new RC2CryptoServiceProvider()) {
                            Key = alg.Key;
                            IV = alg.IV;
                            encryptor = alg.CreateEncryptor();
                        }
                        break;

                    case "rijndael":
                    using (Rijndael r = Rijndael.Create()) {
                        Key = r.Key;
                        IV = r.IV;
                        encryptor = r.CreateEncryptor();
                    }
                        break;

                    default:
                        Console.WriteLine("incorrect alghorytm name. use (rijndael, rc2, aes, des)");
                        return;
                }
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
                string path = Path.GetDirectoryName(input);
                string keyfile = input + ".key.txt";
                string fileKey = Path.Combine(path, keyfile);
                using (FileStream fout = new FileStream(fileKey, FileMode.Create, FileAccess.Write)) {
                    using (StreamWriter sw = new StreamWriter(fout)) {
                        String key = Convert.ToBase64String(Key);
                        String iV = Convert.ToBase64String(IV);
                        sw.WriteLine(key);
                        sw.WriteLine(iV);
                    }
                    Console.WriteLine("key-file created");
                }
                encryptor.Dispose();
                alg.Dispose(); 
           // }
         //   catch (ObjectDisposedException e) {
           //     Console.WriteLine("Caught: {0}", e.Message);
          //  }
        }
    }       
}

   
