using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Ayri.DataProtectionService;

public class DataProtectionService {


    /// <summary>
    /// Este parámetro no aumenta la efectividad del algoritmo, sino que protege a las aplicaciones de la misma
    /// máquina unas de otras, para evitar que una aplicación desencripte datos de otra.
    /// </summary>
    private byte[] entropy;

    /// <summary>
    /// Este parámetro se usa para generar los códigos HASH.<BR/>
    /// OJO: Se puede usar este comando RandomNumberGenerator.GetBytes(128 / 8); para generar un salt fuerte,
    /// pero cada vez que se invoca la aplicación, el salt cambiaría y los hash serían diferentes.
    /// </summary>
    private byte[] salt;

    /// <summary>
    /// Este parámetro define bajo que ámbito se pueden desencriptar los datos. Si se han encriptado con el usuario actual,
    /// sólo el usuario actual los puede desencriptar.
    /// </summary>
    private DataProtectionScope protectionScope;


    /// <summary>
    /// Instancia el servicio con el salt proporcionado y los valores por defecto.<BR/>
    /// La entropía se queda vacía y el ámbito de protección es el usuario activo.
    /// </summary>
    public DataProtectionService(byte[] salt) {
        this.salt = salt;
        this.entropy = [];
        this.protectionScope = DataProtectionScope.CurrentUser;
    }


    /// <summary>
    /// Instancia el servicio con el salt y la entropía proporcionados.<BR/>
    /// El ámbito de protección es el usuario activo.
    /// </summary>
    public DataProtectionService(byte[] salt, string textEntropy) {
        this.salt = salt;
        this.entropy = Encoding.UTF8.GetBytes(textEntropy);
        this.protectionScope = DataProtectionScope.CurrentUser;
    }


    /// <summary>
    /// Instancia el servicio con el salt, la entropía y el ámbito de protección proporcionados.
    /// </summary>
    public DataProtectionService(byte[] salt, string textEntropy, DataProtectionScope protectionScope) {
        this.salt = salt;
        this.entropy = Encoding.UTF8.GetBytes(textEntropy);
        this.protectionScope = protectionScope;
    }


    /// <summary>
    /// Encripta el texto proporcionado en un array de bytes.
    /// </summary>
    public byte[] Protect(string textData) {
        byte[] data = Encoding.UTF8.GetBytes(textData);
        return ProtectedData.Protect(data, entropy, protectionScope);
    }


    /// <summary>
    /// Encripta el texto proporcionado en una cadena de texto de base 64<BR/>
    /// Esta cadena de texto puede ser usada en bases de datos o archivos JSON.
    /// </summary>
    public string ProtectToBase64(string textData) {
        byte[] data = Encoding.UTF8.GetBytes(textData);
        byte[] protectedData = ProtectedData.Protect(data, entropy, protectionScope);
        return Convert.ToBase64String(protectedData);
    }


    /// <summary>
    /// Desencripta el array de bytes proporcionado en una cadena de texto.
    /// </summary>
    public string UnProtect(byte[] protectedData) {
        byte[] unprotectedData = ProtectedData.Unprotect(protectedData, entropy, protectionScope);
        return Encoding.UTF8.GetString(unprotectedData);
    }


    /// <summary>
    /// Desencripta una cadena de texto de base 64 (encriptada anteriormente de esta manera) en una cadena de texto.
    /// </summary>
    public string UnProtectFromBase64(string protectedBase64String) {
        byte[] protectedData = Convert.FromBase64String(protectedBase64String);
        byte[] unprotectedData = ProtectedData.Unprotect(protectedData, entropy, protectionScope);
        return Encoding.UTF8.GetString(unprotectedData);
    }


    /// <summary>
    /// Genera un código hash de 256 bits para el password proporcionado.
    /// </summary>
    public string GenerateHash(string password) {
        byte[] hash = KeyDerivation.Pbkdf2(password!, salt, KeyDerivationPrf.HMACSHA256, 100000, 256 / 8);
        return Convert.ToBase64String(hash);
    }


}
