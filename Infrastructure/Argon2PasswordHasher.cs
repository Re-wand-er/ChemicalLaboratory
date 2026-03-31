using System;
using System.Security.Cryptography;
using System.Text;
using ChemicalLaboratory.Application.Interfaces;
using Konscious.Security.Cryptography;

public class Argon2PasswordHasher : IPasswordHasher
{
    // Хеширование пароля
    public string HashPassword(string password)
    {
        var salt = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt); // Генерация случайной соли
        }

        using (var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password)))
        {
            argon2.Salt = salt;
            argon2.DegreeOfParallelism = 8; // Количество потоков
            argon2.MemorySize = 128 * 1024; // Память в КБ (128 МБ)
            argon2.Iterations = 4; // Количество итераций

            var hash = argon2.GetBytes(32); // Получение 32-байтового хеша

            // Объединяем соль и хеш для хранения
            return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
        }
    }

    // Проверка пароля
    public bool VerifyHash(string password, string storedHash)
    {
        var parts = storedHash.Split(':');
        var salt = Convert.FromBase64String(parts[0]);
        var hash = Convert.FromBase64String(parts[1]);

        using (var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password)))
        {
            argon2.Salt = salt;
            argon2.DegreeOfParallelism = 8;
            argon2.MemorySize = 128 * 1024;
            argon2.Iterations = 4;

            var newHash = argon2.GetBytes(32);
            // Сравниваем новый хеш со старым
            return HashCryptographicEquals(newHash, hash);
        }
    }

    // Cравнение массивов байт
    private bool HashCryptographicEquals(byte[] a, byte[] b)
    {
        if (a.Length != b.Length) return false;
        var result = 0;
        for (int i = 0; i < a.Length; i++) result |= a[i] ^ b[i];
        return result == 0;
    }
}
