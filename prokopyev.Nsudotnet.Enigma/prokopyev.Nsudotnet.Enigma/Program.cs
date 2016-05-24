using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace prokopyev.Nsudotnet.Enigma{
    class Program{

        public static void Main(string[] args){
            String mode = args[0];
            String input = args[1]; 
            String type = args[2];
            String output = null;
            String keyFile = null;

            if (args.Length != 4 && args.Length != 5) {
                Console.WriteLine("not correct args");
                return;
            }
            if (args.Length == 4) {
                output = args[3];
            }
            if (args.Length == 5) {
                keyFile = args[3];
                output = args[4];
                if (!File.Exists(keyFile)) {
                    Console.WriteLine("where is key-file?");
                    return;
                }
            }
            try {
             //   if (!File.Exists(input)) {
             //       Console.WriteLine("where is IO -files? pidor");
             //       return;
             //   }
                if (mode != "decrypt" && mode != "encrypt") {
                    Console.WriteLine("incorrect mode!(encrypt or decrypt)");
                    return;
                }
                if (mode == "decrypt"){                              
                    Decryptor decryptor = new Decryptor();                   
                    decryptor.Decrypt(type, input, output, keyFile);    
                    Console.WriteLine("file restored");
                    
                }
                
                //encrypt c:\Users\ivqn\Desktop\text1.txt  aes c:\Users\ivqn\Desktop\text.txt 
                if (mode == "encrypt") {
                    Encryptor encryptor = new Encryptor();
                    encryptor.Encrypt(type, input, output);
                }
                
            }catch (Exception e){
                Console.WriteLine("Error: {0}", e.Message);
            }
        }             
    }
}
