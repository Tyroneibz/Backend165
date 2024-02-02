using MongoDB.Driver;
using Backend165.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Backend165.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("SkiServiceDB");
            _users = database.GetCollection<User>("Users");
        }

        // Alle Benutzer abrufen
        public async Task<List<User>> GetAllAsync()
        {
            return await _users.Find(user => true).ToListAsync();
        }

        // Einen Benutzer nach Username abrufen
        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _users.Find(user => user.Username == username).FirstOrDefaultAsync();
        }

        // Einen neuen Benutzer erstellen
        public async Task CreateAsync(User user)
        {
            // Hier sollten Sie das Passwort hashen, bevor Sie es speichern
            user.Password = HashPassword(user.Password);
            await _users.InsertOneAsync(user);
        }

        // Benutzerdaten aktualisieren
        public async Task UpdateAsync(string id, User updatedUser)
        {
            // Hier sollten Sie das Passwort hashen, falls es aktualisiert wird
            if (!string.IsNullOrEmpty(updatedUser.Password))
            {
                updatedUser.Password = HashPassword(updatedUser.Password);
            }

            await _users.ReplaceOneAsync(user => user.Id == id, updatedUser);
        }

        // Einen Benutzer löschen
        public async Task DeleteAsync(string id)
        {
            await _users.DeleteOneAsync(user => user.Id == id);
        }

        // Beispiel für eine Methode zum Hashen des Passworts
        private string HashPassword(string password)
        {
            // Hier sollten Sie einen sicheren Algorithmus zur Passworthashierung verwenden
            // Beispiel: Verwenden Sie eine Passwort-Hashing-Bibliothek wie BCrypt oder PBKDF2
            // Implementieren Sie diese Methode nicht selbst, da die sichere Passworthashierung komplex ist.
            // Beispiel mit BCrypt (empfohlen):
            // string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            // return hashedPassword;

            return password; // Dies ist nur ein Dummy-Beispiel und unsicher. Bitte verwenden Sie eine echte Hashing-Bibliothek.
        }

        // In der UserService-Klasse

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await _users.Find(user => user.Username == username && user.Password == password).FirstOrDefaultAsync();
            return user;
        }

    }
}

