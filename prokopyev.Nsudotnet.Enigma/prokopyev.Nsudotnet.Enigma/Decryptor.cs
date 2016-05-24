using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
namespace prokopyev.Nsudotnet.Enigma {
    class Decryptor {

        internal void Decrypt(String type, String from, String to, String keyFile) {
            TextReader tr = new StreamReader(keyFile);
            byte[] Key = Convert.FromBase64String(tr.ReadLine());
            byte[] IV = Convert.FromBase64String(tr.ReadLine());
            tr.Dispose();
            ICryptoTransform decryptor = null;
            
            switch (type) {
                case "aes":
                    using (Aes aes = Aes.Create()) {
                        aes.Key = Key;
                        aes.IV = IV;
                        decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                    }
                    break;

                case "des":
                    using (DESCryptoServiceProvider des = new DESCryptoServiceProvider()) {
                        des.Key = Key;
                        des.IV = IV;
                        decryptor = des.CreateDecryptor(des.Key, des.IV);
                    }
                    break;
            
                case "rc2":
                    using (RC2CryptoServiceProvider rc2 = new RC2CryptoServiceProvider()) {
                        rc2.Key = Key;
                        rc2.IV = IV;
                        decryptor = rc2.CreateDecryptor(rc2.Key, rc2.IV);
                    }
                    break;


                case "rijndael":
                    using (Rijndael r = Rijndael.Create()) {
                        r.Key = Key;
                        r.IV = IV;
                        decryptor = r.CreateDecryptor(r.Key, r.IV);
                    }
                    break;
                default:
                    Console.WriteLine("incorrect alghorytm name. use (rijndael, rc2, aes, des)");
                    return;
            }

            using (FileStream destination = new FileStream(from, FileMode.Open)) {
                using (CryptoStream cryptoStream = new CryptoStream(destination, decryptor, CryptoStreamMode.Read)) {
                    using (FileStream fout = new FileStream(to, FileMode.Create)) {
                        int data;
                        while ((data = cryptoStream.ReadByte()) != -1) {
                            fout.WriteByte((byte)data);
                        }
                    }
                }
            }
            decryptor.Dispose();
        }
    }
}

